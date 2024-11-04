using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using LibVLCSharp.Shared;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Saitek.DirectOutput;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SpotifyAPI.Web.Enums;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static FIPToolKit.Models.FIPSpotifyPlayer;
using TagLib;

namespace FIPToolKit.Models
{
    internal class FIPMusicLibrary : IDisposable
    {
        public List<FIPMusicArtist> Artists { get; set; } = new List<FIPMusicArtist>();

        public void Dispose()
        {
            foreach (var artist in Artists)
            {
                artist.Dispose();
            }
        }
    }

    internal class FIPMusicArtist : IDisposable
    {
        public string ArtistName { get; set; }
        public List<FIPMusicAlbum> Albums { get; set; } = new List<FIPMusicAlbum>();
        public Image Artwork
        {
            get
            {
                if (Albums.Count > 0 && Albums.First().Songs.Count > 0)
                {
                    return Albums.First().Songs.First().Artwork;
                }
                return null;
            }
        }

        public void Dispose()
        {
            foreach(var album in Albums)
            {
                album.Dispose();
            }
        }
    }

    internal class FIPMusicAlbum : IDisposable
    {
        public string AlbumName { get; set; }
        public List<FIPMusicSong> Songs { get; set; } = new List<FIPMusicSong>();
        public Image Artwork
        {
            get
            {
                if (Songs.Count > 0)
                {
                    return Songs.First().Artwork;
                }
                return null;
            }
        }

        public void Dispose()
        {
            foreach( var song in Songs)
            {
                song.Dispose();
            }
        }
    }

    internal class FIPMetaData : IDisposable
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public uint Track { get; set; }
        public Image Artwork { get; set; }

        public void Dispose()
        {
            if (Artwork != null)
            {
                Artwork.Dispose();
                Artwork = null;
            }
        }
    }

    internal class FIPMusicSong : IDisposable
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title {  get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public TimeSpan Duration { get; set; }
        public uint Year { get; set; }
        public Image Artwork { get; set; }
        public uint Track { get; set; }
        public FIPMetaData MetaData { get; set; } = new FIPMetaData();

        public bool IsPlaylist
        {
            get
            {
                return Path.GetExtension(Filename ?? string.Empty).Equals(".m3u", StringComparison.OrdinalIgnoreCase);
            }
        }

        public void Dispose()
        {
            if (Artwork != null)
            {
                Artwork.Dispose();
                Artwork = null;
            }
            if (MetaData != null)
            {
                MetaData.Dispose();
            }
        }
    }

    public enum MusicPlayerPage
    {
        Player,
        Library
    }

    public enum MusicLibraryPage
    {
        Artists,
        Albums,
        Songs
    }

    public enum MusicRepeatState
    {
        Track = 1,
        Context = 2,
        Off = 4
    }

    public class FIPMusicPlayer : FIPPage
    {
        private LibVLC libVLC = null;
        private MediaPlayer player = null;
        private Media media = null;
        private bool opening = false;
        
        private FIPMusicLibrary Library { get; set; }
        private FIPMusicSong CurrentSong { get; set; }
        private FIPMusicArtist CurrentArtist { get; set; }
        private FIPMusicAlbum CurrentAlbum { get; set; }
        private MusicPlayerPage CurrentPage { get; set; }
        private MusicLibraryPage LibraryPage { get; set; }
        private FIPMusicArtist LibraryArtist { get; set; }
        private FIPMusicAlbum LibraryAlbum { get; set; }
        private FIPMusicSong LibrarySong { get; set; }
        private int LibraryIndex { get; set; }
        private List<FIPMusicSong> Playlist { get; set; }
        private List<int> RandomList { get; set; }
        private bool EndOfPlaylist { get; set; }
        private bool Initialized { get; set; }

        public event FIPPageEventHandler OnSongChanged;
        public event FIPPageEventHandler OnEndOfPlaylist;
        public event FIPCanPlayEventHandler OnCanPlay;

        public FIPMusicPlayer(FIPMusicPlayerProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnVolumeChanged += Properties_OnVolumeChanged;
            properties.OnPathChanged += Properties_OnPathChanged;
            properties.OnShuffleChanged += Properties_OnShuffleChanged;
            properties.OnRepeatChanged += Properties_OnRepeatChanged;
            Core.Initialize();
            libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-sout-video", "--quiet" });
            CreatePlayer();
        }

        public void Init()
        {
            if (!Initialized)
            {
                Initialized = true;
                LoadLibrary(MusicPlayerProperties.Resume);
            }
        }

        public override void StartTimer()
        {
            Init();
        }

        private void Properties_OnRepeatChanged(object sender, EventArgs e)
        {
            UpdatePage();
        }

        private void Properties_OnShuffleChanged(object sender, EventArgs e)
        {
            UpdatePage();
        }

        private void Properties_OnPathChanged(object sender, EventArgs e)
        {
            if (Library != null)
            {
                Library.Dispose();
                Library = null;
            }
            LoadLibrary(false);
        }

        private void Properties_OnVolumeChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null && player.Volume != MusicPlayerProperties.Volume)
                {
                    player.Volume = MusicPlayerProperties.Volume;
                }
            });
        }

        private FIPMusicPlayerProperties MusicPlayerProperties
        {
            get
            {
                return Properties as FIPMusicPlayerProperties;
            }
        }

        public override void UpdatePage()
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                UpdatePlayer();
            }
            else
            {
                UpdatePlayList();
            }
            SetLEDs();
        }

        private void CreatePlayer()
        {
            if (player == null)
            {
                player = new MediaPlayer(libVLC);
                player.EnableHardwareDecoding = true;
                player.EncounteredError += (s, e) =>
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        if (media != null)
                        {
                            media.Dispose();
                            media = null;
                        }
                        player.Media = null;
                        using (Bitmap bmp = ImageHelper.GetErrorImage("An Error Has Occured."))
                        {
                            SendImage(bmp);
                        }
                    });
                };
                player.VolumeChanged += (s, e) =>
                {
                    if (player != null)
                    {
                        MusicPlayerProperties.SetVolume(player.Volume);
                    }
                };
                player.Opening += (s, e) =>
                {
                    opening = true;
                    UpdatePage();
                };
                player.Playing += (s, e) =>
                {
                    if (opening)
                    {
                        opening = false;
                        UpdatePage();
                        if (resume)
                        {
                            ThreadPool.QueueUserWorkItem(_ => player.Pause());
                        }
                    }
                };
                player.EndReached += (s, e) =>
                {
                    PlayNextSong(true);
                };
            }
        }

        public override void Dispose()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (media != null)
                {
                    media.Dispose();
                    media = null;
                }
                player.Stop();
                player.Dispose();
                player = null;
                libVLC.Dispose();
                libVLC = null;
            });
            if (Library != null)
            {
                Library.Dispose();
            }
            base.Dispose();
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        if (player != null)
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                player.Mute = !player.Mute;
                                UpdatePage();
                            });
                        }
                        break;
                    case SoftButtons.Button2:
                        if (player != null)
                        {
                            if (media != null)
                            {
                                ThreadPool.QueueUserWorkItem(_ =>
                                {
                                    if (EndOfPlaylist)
                                    {
                                        EndOfPlaylist = false;
                                        PlayFirstSong(false);
                                    }
                                    else
                                    {
                                        if (player.IsPlaying)
                                        {
                                            player.Pause();
                                        }
                                        else
                                        {
                                            player.Play();
                                        }
                                    }
                                    UpdatePage();
                                });
                            }
                            else
                            {
                                PlayNextSong();
                            }
                        }
                        else if (player != null && Library == null && !string.IsNullOrEmpty(MusicPlayerProperties.Path))
                        {
                            LoadLibrary(false);
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button3:
                        MusicPlayerProperties.Shuffle = !MusicPlayerProperties.Shuffle;
                        UpdatePage();
                        break;
                    case SoftButtons.Button4:
                        switch (MusicPlayerProperties.Repeat)
                        {
                            case MusicRepeatState.Track:
                                MusicPlayerProperties.Repeat = MusicRepeatState.Context;
                                break;
                            case MusicRepeatState.Context:
                                MusicPlayerProperties.Repeat = MusicRepeatState.Off;
                                break;
                            case MusicRepeatState.Off:
                                MusicPlayerProperties.Repeat = MusicRepeatState.Track;
                                break;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button6:
                        if (CurrentPage == MusicPlayerPage.Player)
                        {
                            CurrentPage = MusicPlayerPage.Library;
                            LibraryArtist = CurrentArtist;
                            LibraryAlbum = CurrentAlbum;
                            LibrarySong = CurrentSong;
                            LibraryPage = CurrentSong != null ? MusicLibraryPage.Songs : MusicLibraryPage.Artists;
                        }
                        else
                        {
                            CurrentPage = MusicPlayerPage.Player;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Up:
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            int newVolume = player.Volume + 5;
                            newVolume = Math.Max(0, Math.Min(newVolume, 100));
                            player.Volume = newVolume;
                        });
                        break;
                    case SoftButtons.Down:
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            int newVolume = player.Volume - 5;
                            newVolume = Math.Max(0, Math.Min(newVolume, 100));
                            player.Volume = newVolume;
                        });
                        break;
                    case SoftButtons.Left:
                        if (player != null && Library != null && Playlist != null)
                        {
                            PlayPreviousSong();
                        }
                        else if (player != null && Library == null && !string.IsNullOrEmpty(MusicPlayerProperties.Path))
                        {
                            LoadLibrary(false);
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Right:
                        if (player != null && Library != null && Playlist != null)
                        {
                            PlayNextSong();
                        }
                        else if (player != null && Library == null && !string.IsNullOrEmpty(MusicPlayerProperties.Path))
                        {
                            LoadLibrary(false);
                        }
                        UpdatePage();
                        break;
                    default:
                        base.ExecuteSoftButton(softButton);
                        break;
                }
            }
            else
            {
                switch (softButton)
                {
                    case SoftButtons.Left:
                    case SoftButtons.Down:
                        switch (LibraryPage)
                        {
                            case MusicLibraryPage.Artists:
                                if (Library != null && Library.Artists.Count > 0)
                                {
                                    if (LibraryArtist != null)
                                    {
                                        LibraryArtist = Library.Artists[Math.Max(0, Library.Artists.IndexOf(LibraryArtist) - 1)];
                                    }
                                    else
                                    {
                                        LibraryArtist = Library.Artists.Last();
                                    }
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryArtist != null && LibraryArtist.Albums.Count > 0)
                                {
                                    if (LibraryAlbum != null)
                                    {
                                        LibraryAlbum = LibraryArtist.Albums[Math.Max(0, LibraryArtist.Albums.IndexOf(LibraryAlbum) - 1)];
                                    }
                                    else
                                    {
                                        LibraryAlbum = LibraryArtist.Albums.Last();
                                    }
                                }
                                break;
                            default:
                                if (LibraryAlbum != null && LibraryAlbum.Songs.Count > 0)
                                {
                                    if (LibrarySong != null)
                                    {
                                        LibrarySong = LibraryAlbum.Songs[Math.Max(0, LibraryAlbum.Songs.IndexOf(LibrarySong) - 1)];
                                    }
                                    else
                                    {
                                        LibrarySong = LibraryAlbum.Songs.Last();
                                    }
                                }
                                break;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Right:
                    case SoftButtons.Up:
                        switch (LibraryPage)
                        {
                            case MusicLibraryPage.Artists:
                                if (Library != null && Library.Artists.Count > 0)
                                {
                                    if (LibraryArtist != null)
                                    {
                                        LibraryArtist = Library.Artists[Math.Min(Library.Artists.Count - 1, Library.Artists.IndexOf(LibraryArtist) + 1)];
                                    }
                                    else
                                    {
                                        LibraryArtist = Library.Artists.First();
                                    }
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryArtist != null && LibraryArtist.Albums.Count > 0)
                                {
                                    if (LibraryAlbum != null)
                                    {
                                        LibraryAlbum = LibraryArtist.Albums[Math.Min(LibraryArtist.Albums.Count - 1, LibraryArtist.Albums.IndexOf(LibraryAlbum) + 1)];
                                    }
                                    else
                                    {
                                        LibraryAlbum = LibraryArtist.Albums.First();
                                    }
                                }
                                break;
                            default:
                                if (LibraryAlbum != null && LibraryAlbum.Songs.Count > 0)
                                {
                                    if (LibrarySong != null)
                                    {
                                        LibrarySong = LibraryAlbum.Songs[Math.Min(LibraryAlbum.Songs.Count - 1, LibraryAlbum.Songs.IndexOf(LibrarySong) + 1)];
                                    }
                                    else
                                    {
                                        LibrarySong = LibraryAlbum.Songs.First();
                                    }
                                }
                                break;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button1:
                        switch (LibraryPage)
                        {
                            case MusicLibraryPage.Albums:
                                LibraryPage = MusicLibraryPage.Artists;
                                LibraryAlbum = null;
                                break;
                            case MusicLibraryPage.Songs:
                                LibraryPage = MusicLibraryPage.Albums;
                                LibrarySong = null;
                                break;
                            default:
                                break;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button2:
                        switch (LibraryPage)
                        {
                            case MusicLibraryPage.Artists:
                                if (LibraryArtist != null && LibraryArtist.Albums.Count > 0)
                                {
                                    LibraryPage = MusicLibraryPage.Albums;
                                    LibraryAlbum = LibraryArtist.Albums.First();
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryAlbum != null && LibraryAlbum.Songs.Count > 0)
                                {
                                    LibraryPage = MusicLibraryPage.Songs;
                                    LibrarySong = LibraryAlbum.Songs.First();
                                }
                                break;
                            default:
                                break;
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button3:
                        OnSongChanged?.Invoke(this, new FIPPageEventArgs(this));
                        CurrentPage = MusicPlayerPage.Player;
                        CreatePlaylist();
                        UpdatePage();
                        PlayNextSong();
                        break;
                    case SoftButtons.Button4:
                        CurrentPage = MusicPlayerPage.Player;
                        LibraryAlbum = null;
                        LibraryArtist = null;
                        LibrarySong = null;
                        UpdatePage();
                        break;
                }
            }
            FireSoftButtonNotifcation(softButton);
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        return (player != null);
                    case SoftButtons.Button2:
                        return (player != null && Library != null && CurrentSong != null);
                    case SoftButtons.Button3:
                        return (Library != null);
                    case SoftButtons.Button4:
                        return (Library != null);
                    case SoftButtons.Button6:
                        return (Library != null);
                }
            }
            else
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        return (Library != null && LibraryPage != MusicLibraryPage.Artists);
                    case SoftButtons.Button2:
                        return (Library != null && LibraryPage != MusicLibraryPage.Songs && (LibraryArtist != null || LibraryAlbum != null));
                    case SoftButtons.Button3:
                        return (Library != null && LibrarySong != null || LibraryArtist != null || LibraryAlbum != null);
                    case SoftButtons.Button4:
                        return true;
                }

            }
            return base.IsLEDOn(softButton);
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                    case SoftButtons.Button2:
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                    case SoftButtons.Button6:
                    case SoftButtons.Left:
                    case SoftButtons.Right:
                    case SoftButtons.Up:
                    case SoftButtons.Down:
                        return false;
                }
            }
            else
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                    case SoftButtons.Button2:
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                    case SoftButtons.Left:
                    case SoftButtons.Right:
                    case SoftButtons.Up:
                    case SoftButtons.Down:
                        return false;
                }
            }
            return base.IsButtonAssignable(softButton);
        }

        bool isLoading = false;
        private void LoadLibrary(bool isResuming)
        {
            if (Library == null)
            {
                isLoading = true;
                Library = new FIPMusicLibrary();
                if (!string.IsNullOrEmpty(MusicPlayerProperties.Path))
                {
                    LoadFolder(MusicPlayerProperties.Path);
                }
                CreatePlaylist();
                PlayFirstSong(true, isResuming);
                UpdatePage();
                isLoading = false;
            }
        }

        private void LoadFolder(string path)
        {
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".wma", StringComparison.OrdinalIgnoreCase));
            var folders = Directory.GetDirectories(path);
            foreach(string folder in folders)
            {
                LoadFolder(folder);
            }
            foreach (var file in files)
            {
                using (var tag = TagLib.File.Create(file))
                {
                    FIPMusicSong song = new FIPMusicSong()
                    {
                        Duration = tag.Properties.Duration,
                        Filename = file,
                        Title = string.IsNullOrEmpty(tag.Tag.Title) ? "Unknown Title" : tag.Tag.Title,
                        Year = tag.Tag.Year,
                        Album = string.IsNullOrEmpty(tag.Tag.Album) ? "Unknown Album" : tag.Tag.Album,
                        Artist = string.IsNullOrEmpty(tag.Tag.FirstAlbumArtist) ? "Unknown Artist" : tag.Tag.FirstAlbumArtist,
                        Genre = tag.Tag.FirstGenre??string.Empty,
                        Track = tag.Tag.Track
                    };
                    if (tag.Tag.Pictures.Length > 0)
                    {
                        song.Artwork = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data));
                    }
                    FIPMusicAlbum album = GetAlbum(song.Artist, song.Album);
                    album.Songs.Add(song);
                }
            }
        }

        private FIPMusicAlbum GetAlbum(string artistName, string albumName)
        {
            FIPMusicArtist artist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(artistName, StringComparison.OrdinalIgnoreCase));
            if (artist == null)
            {
                artist = new FIPMusicArtist()
                {
                    ArtistName = artistName
                };
                Library.Artists.Add(artist);
            }
            FIPMusicAlbum album = artist.Albums.FirstOrDefault(a => a.AlbumName.Equals(albumName, StringComparison.OrdinalIgnoreCase));
            if (album == null)
            {
                album = new FIPMusicAlbum()
                {
                    AlbumName = albumName
                };
                artist.Albums.Add(album);
            }
            return album;
        }

        private List<int> GetUniqueRandoms(int count)
        {
            List<int> result = new List<int>(count);
            HashSet<int> set = new HashSet<int>(count);
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int num;
                do
                {
                    num = random.Next(count);
                }
                while (!set.Add(num));
                result.Add(num);
            }
            return result;
        }
        private void CreatePlaylist()
        {
            Playlist = new List<FIPMusicSong>();
            if (LibrarySong != null)
            {
                Playlist.Add(LibrarySong);
            }
            else if (LibraryAlbum != null)
            {
                foreach (FIPMusicSong song in LibraryAlbum.Songs)
                {
                    Playlist.Add(song);
                }
            }
            else if (LibraryArtist != null)
            {
                foreach (FIPMusicAlbum album in LibraryArtist.Albums)
                {
                    foreach (FIPMusicSong song in album.Songs)
                    {
                        Playlist.Add(song);
                    }
                }
            }
            else
            {
                foreach (FIPMusicArtist artist in Library.Artists)
                {
                    foreach (FIPMusicAlbum album in artist.Albums)
                    {
                        foreach (FIPMusicSong song in album.Songs)
                        {
                            Playlist.Add(song);
                        }
                    }
                }
            }
            RandomList = GetUniqueRandoms(Playlist.Count);
        }

        private bool resume = false;
        public void ExternalPause()
        {
            if (player != null && (player.IsPlaying || isLoading || opening) && !resume)
            {
                resume = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (!isLoading && !opening)
                    {
                        player.Pause();
                    }
                });
            }
        }

        public void ExternalResume()
        {
            if (player != null && !player.IsPlaying && resume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (media == null)
                    {
                        PlayFirstSong(true, MusicPlayerProperties.Resume);
                    }
                    else
                    {
                        player.Play();
                    }
                });
            }
            resume = false;
        }

        private FIPMusicSong FindSong(string filename)
        {
            /*if (Library != null)
            {
                foreach (FIPMusicArtist artist in Library.Artists)
                {
                    foreach (FIPMusicAlbum album in artist.Albums)
                    {
                        foreach (FIPMusicSong song in album.Songs)
                        {
                            if (song.Filename.Equals(filename, StringComparison.OrdinalIgnoreCase))
                            {
                                return song;
                            }
                        }
                    }
                }
            }*/
            if (Playlist != null)
            {
                return Playlist.FirstOrDefault(s => s.Filename.Equals(filename, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

        private void PlayFirstSong(bool firstPlay, bool isResuming = false)
        {
            if (Library != null)
            {
                if (Playlist == null)
                {
                    CreatePlaylist();
                }
                if (isResuming && MusicPlayerProperties.Resume)
                {
                    CurrentSong = FindSong(MusicPlayerProperties.LastSong);
                }
                if (CurrentSong == null || !isResuming)
                {
                    if (MusicPlayerProperties.Shuffle)
                    {
                        CurrentSong = Playlist[RandomList.First()];
                    }
                    else
                    {
                        CurrentSong = Playlist.First();
                    }
                }
                CurrentAlbum = GetAlbum(CurrentSong.Artist, CurrentSong.Album);
                CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                UpdatePage();
                OnSongChanged?.Invoke(this, new FIPPageEventArgs(this));
                if (!resume && ((firstPlay && MusicPlayerProperties.AutoPlay) || !firstPlay))
                {
                    FIPCanPlayEventArgs canPlay = new FIPCanPlayEventArgs();
                    OnCanPlay?.Invoke(this, canPlay);
                    if (!canPlay.CanPlay)
                    {
                        resume = true;
                    }
                    if (canPlay.CanPlay)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong.Filename);
                        });
                    }
                }
            }
        }

        private void PlayNextSong(bool endReached = false)
        {
            if (Library != null)
            {
                if (Playlist == null)
                {
                    CreatePlaylist();
                }
                if (Playlist != null && Playlist.Count > 0)
                {
                    if (CurrentSong == null)
                    {
                        if (MusicPlayerProperties.Shuffle)
                        {
                            CurrentSong = Playlist[RandomList.First()];
                        }
                        else
                        {
                            CurrentSong = Playlist.First();
                        }
                        CurrentAlbum = GetAlbum(CurrentSong.Artist, CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        int index = Playlist.IndexOf(CurrentSong);
                        if (MusicPlayerProperties.Repeat == MusicRepeatState.Off || MusicPlayerProperties.Repeat == MusicRepeatState.Context)
                        {
                            if (MusicPlayerProperties.Shuffle)
                            {
                                index = RandomList.IndexOf(index);
                                index++;
                                if (index >= RandomList.Count)
                                {
                                    index = 0;
                                    if (MusicPlayerProperties.Repeat == MusicRepeatState.Off)
                                    {
                                        if (endReached)
                                        {
                                            EndOfPlaylist = true;
                                            OnEndOfPlaylist?.Invoke(this, new FIPPageEventArgs(this));
                                            UpdatePage();
                                        }
                                        return;
                                    }
                                }
                                CurrentSong = Playlist[RandomList[index]];
                            }
                            else
                            {
                                index++;
                                if (index >= Playlist.Count)
                                {
                                    index = 0;
                                    if (MusicPlayerProperties.Repeat == MusicRepeatState.Off)
                                    {
                                        if (endReached)
                                        {
                                            EndOfPlaylist = true;
                                            OnEndOfPlaylist?.Invoke(this, new FIPPageEventArgs(this));
                                            UpdatePage();
                                        }
                                        return;
                                    }
                                }
                                CurrentSong = Playlist[index];
                            }
                            CurrentAlbum = GetAlbum(CurrentSong.Artist, CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    UpdatePage();
                    OnSongChanged?.Invoke(this, new FIPPageEventArgs(this));
                    if (player.IsPlaying || endReached)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong.Filename);
                        });
                    }
                }
            }
        }

        private void PlayPreviousSong()
        {
            if (Library != null)
            {
                if (Playlist == null)
                {
                    CreatePlaylist();
                }
                if (Playlist != null && Playlist.Count > 0)
                {
                    if (CurrentSong == null)
                    {
                        if (MusicPlayerProperties.Shuffle)
                        {
                            CurrentSong = Playlist[RandomList.Last()];
                        }
                        else
                        {
                            CurrentSong = Playlist.Last();
                        }
                        CurrentAlbum = GetAlbum(CurrentSong.Artist, CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        int index = Playlist.IndexOf(CurrentSong);
                        if (MusicPlayerProperties.Repeat == MusicRepeatState.Off || MusicPlayerProperties.Repeat == MusicRepeatState.Context)
                        {
                            if (MusicPlayerProperties.Shuffle)
                            {
                                index = RandomList.IndexOf(index);
                                index--;
                                if (index < 0)
                                {
                                    index = RandomList.Count - 1;
                                    if (MusicPlayerProperties.Repeat == MusicRepeatState.Off)
                                    {
                                        return;
                                    }
                                }
                                CurrentSong = Playlist[RandomList[index]];
                            }
                            else
                            {
                                index--;
                                if (index < 0)
                                {
                                    index = Playlist.Count - 1;
                                    if (MusicPlayerProperties.Repeat == MusicRepeatState.Off)
                                    {
                                        return;
                                    }
                                }
                                CurrentSong = Playlist[index];
                            }
                            CurrentAlbum = GetAlbum(CurrentSong.Artist, CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    UpdatePage();
                    OnSongChanged?.Invoke(this, new FIPPageEventArgs(this));
                    if (player.IsPlaying)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong.Filename);
                        });
                    }
                }
            }
        }

        private void Play(string filename)
        {
            Stop();
            if (media != null)
            {
                media.Dispose();
                media = null;
            }
            if (!string.IsNullOrEmpty(filename) && System.IO.File.Exists(filename))
            {
                CreatePlayer();
                UpdatePlayer();
                MusicPlayerProperties.LastSong = filename;
                media = new Media(libVLC, filename);
                media.MetaChanged += Media_MetaChanged;
                player.Media = media;
                if (!resume)
                {
                    player.Play();
                }
            }
            else
            {
                using (Bitmap bmp = ImageHelper.GetErrorImage("Select a song to play."))
                {
                    SendImage(bmp);
                }
            }
        }

        private void Media_MetaChanged(object sender, MediaMetaChangedEventArgs e)
        {
            try
            {
                if (player != null)
                {
                    string meta = player.Media.Meta(e.MetadataType);
                    if (!string.IsNullOrEmpty(meta) && e.MetadataType == MetadataType.ArtworkURL)
                    {
                        using (var tag = TagLib.File.Create(CurrentSong.Filename))
                        {
                            if (tag.Tag.Pictures.Length > 0)
                            {
                                CurrentSong.MetaData.Artwork = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data));
                                UpdatePage();
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(meta) && e.MetadataType == MetadataType.NowPlaying)
                    {
                        CurrentSong.MetaData.Title = player.Media.Meta(MetadataType.Title);
                        CurrentSong.MetaData.Artist = player.Media.Meta(MetadataType.AlbumArtist);
                        CurrentSong.MetaData.Album = player.Media.Meta(MetadataType.Album);
                        CurrentSong.MetaData.Genre = player.Media.Meta(MetadataType.Genre);
                        CurrentSong.MetaData.Track = Convert.ToUInt32(string.IsNullOrEmpty(player.Media.Meta(MetadataType.TrackNumber)) ? "0" : player.Media.Meta(MetadataType.TrackNumber));
                        UpdatePage();
                    }
                }
            }
            catch(Exception)
            {
            }
        }

        private void Stop()
        {
            MediaPlayer oldPlayer = player;
            player = null;
            Task.Run(() =>
            {
                if (oldPlayer != null)
                {
                    oldPlayer.Stop();
                    oldPlayer.Dispose();
                    oldPlayer = null;
                }
            });
        }

        private void UpdatePlayer()
        {
            try
            {
                Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(MusicPlayerProperties.FontColor))
                    {
                        graphics.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            if (Library == null)
                            {
                                graphics.DrawString("Please choose a path to your music library", MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            else if (CurrentSong != null)
                            {
                                if ((CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork : CurrentSong.Artwork) != null)
                                {
                                    int titleHeight = (int)graphics.MeasureString(CurrentSong.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, MusicPlayerProperties.Font, 288, format).Height;
                                    int artistHeight = (int)graphics.MeasureString(CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, 288, format).Height;
                                    int maxImageWidth = 320 - 34;
                                    int maxImageHeight = 240 - (titleHeight + artistHeight);
                                    //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                                    double ratioX = (double)maxImageWidth / (CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width);
                                    double ratioY = (double)maxImageHeight / (CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height);
                                    double ratio = Math.Min(ratioX, ratioY);
                                    int imageWidth = (int)((CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width) * ratio);
                                    int imageHeight = (int)((CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height) * ratio);
                                    Rectangle destRect = new Rectangle(17 + ((320 - imageWidth) / 2), (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                                    graphics.DrawImage(CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork : CurrentSong.Artwork, destRect, 0, 0, CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width, CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height, GraphicsUnit.Pixel);
                                    graphics.DrawString(CurrentSong.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, MusicPlayerProperties.Font, brush, new RectangleF(32, maxImageHeight, 288, titleHeight), format);
                                    graphics.DrawString(CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, brush, new RectangleF(32, maxImageHeight + titleHeight, 288, artistHeight), format);
                                }
                                else
                                {
                                    string text = string.Format("{0}\n{1}", CurrentSong.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, CurrentSong.IsPlaylist ? CurrentSong.MetaData.Artist : CurrentSong.Artist);
                                    graphics.DrawString(text, MusicPlayerProperties.Font, brush, new RectangleF(32, 0, 288, 240), format);
                                }
                            }
                            else
                            {
                                graphics.DrawString("Loading", MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                        }
                        if (Library != null)
                        {
                            if (player != null)
                            {
                                graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, player.Mute ? Color.Red : Color.Green, true, SoftButtons.Button1);
                                graphics.AddButtonIcon(player.State == VLCState.Playing ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, player.State == VLCState.Playing ? Color.Yellow : Color.Blue, true, SoftButtons.Button2);
                            }
                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.shuffle, MusicPlayerProperties.Shuffle == true ? Color.Green : Color.White, true, SoftButtons.Button3);
                            graphics.AddButtonIcon(MusicPlayerProperties.Repeat == MusicRepeatState.Track ? FIPToolKit.Properties.Resources.repeat_one : FIPToolKit.Properties.Resources.repeat, MusicPlayerProperties.Repeat != MusicRepeatState.Off ? Color.Green : Color.White, true, SoftButtons.Button4);
                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.playlist, Color.Orange, true, SoftButtons.Button6);
                        }
                    }
                }
                SendImage(bmp);
                bmp.Dispose();
            }
            catch
            {
            }
        }

        private void UpdatePlayList()
        {
            try
            {
                Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(MusicPlayerProperties.FontColor))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.FormatFlags = StringFormatFlags.NoWrap;
                            format.LineAlignment = StringAlignment.Center;
                            format.Trimming = StringTrimming.EllipsisCharacter;
                            int page = 0;
                            switch(LibraryPage)
                            {
                                case MusicLibraryPage.Artists:
                                    if (LibraryArtist != null)
                                    {
                                        page = Library.Artists.IndexOf(Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryArtist.ArtistName, StringComparison.OrdinalIgnoreCase))) / 8;
                                    }
                                    break;
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum != null)
                                    {
                                        page = LibraryArtist.Albums.IndexOf(LibraryArtist.Albums.FirstOrDefault(a => a.AlbumName.Equals(LibraryAlbum.AlbumName, StringComparison.OrdinalIgnoreCase))) / 8;
                                    }
                                    break;
                                default:
                                    if (LibrarySong != null)
                                    {
                                        page = LibraryAlbum.Songs.IndexOf(LibraryAlbum.Songs.FirstOrDefault(s => s.Title.Equals(LibrarySong.Title, StringComparison.OrdinalIgnoreCase))) / 8;
                                    }
                                    break;
                            }
                            Rectangle destRect;
                            //Draw playlists for currenttly displayed page
                            for (int i = 0; i < 8; i++)
                            {
                                int count = Library.Artists.Count;
                                switch (LibraryPage)
                                {
                                    case MusicLibraryPage.Albums:
                                        count = LibraryArtist.Albums.Count;
                                        break;
                                    case MusicLibraryPage.Songs:
                                        count = LibraryAlbum.Songs.Count;
                                        break;
                                }
                                if (i + (page * 8) >= count)
                                {
                                    break;
                                }
                                switch(LibraryPage)
                                {
                                    case MusicLibraryPage.Artists:
                                        FIPMusicArtist artist = Library.Artists[i + (page * 8)];
                                        if (artist.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, (30 * i) + 1, 28, 28);
                                            graphics.DrawImage(artist.Artwork, destRect, 0, 0, artist.Artwork.Width, artist.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(64, (30 * i), 256, 30);
                                        graphics.DrawString(artist.ArtistName, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                    case MusicLibraryPage.Albums:
                                        FIPMusicAlbum album = LibraryArtist.Albums[i + (page * 8)];
                                        if (album.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, (30 * i) + 1, 28, 28);
                                            graphics.DrawImage(album.Artwork, destRect, 0, 0, album.Artwork.Width, album.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(64, (30 * i), 256, 30);
                                        graphics.DrawString(album.AlbumName, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                    default:
                                        FIPMusicSong song = LibraryAlbum.Songs[i + (page * 8)];
                                        if (song.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, (30 * i) + 1, 28, 28);
                                            graphics.DrawImage(song.Artwork, destRect, 0, 0, song.Artwork.Width, song.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(64, (30 * i), 256, 30);
                                        graphics.DrawString(song.Title, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                }
                            }
                            //Draw currently selected song/artist/album
                            int index = 0;
                            switch (LibraryPage)
                            {
                                case MusicLibraryPage.Artists:
                                    if (LibraryArtist != null)
                                    {
                                        index = Library.Artists.IndexOf(Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryArtist.ArtistName, StringComparison.OrdinalIgnoreCase)));
                                    }
                                    break;
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum != null)
                                    {
                                        index = LibraryArtist.Albums.IndexOf(LibraryArtist.Albums.FirstOrDefault(a => a.AlbumName.Equals(LibraryAlbum.AlbumName, StringComparison.OrdinalIgnoreCase)));
                                    }
                                    break;
                                default:
                                    if (LibrarySong != null)
                                    {
                                        index = LibraryAlbum.Songs.IndexOf(LibraryAlbum.Songs.FirstOrDefault(s => s.Title.Equals(LibrarySong.Title, StringComparison.OrdinalIgnoreCase)));
                                    }
                                    break;
                            }
                            int selectOffset = (index % 8) * 30;
                            destRect = new Rectangle(32, selectOffset, 288, 30);
                            if (LibraryArtist != null || LibraryAlbum != null || LibrarySong != null)
                            {
                                graphics.FillRectangle(SystemBrushes.Highlight, destRect);
                            }
                            destRect = new Rectangle(64, selectOffset, 256, 30);
                            switch (LibraryPage)
                            {
                                case MusicLibraryPage.Artists:
                                    if (LibraryArtist != null)
                                    {
                                        graphics.DrawString(LibraryArtist.ArtistName, MusicPlayerProperties.Font, brush, destRect, format);
                                        if (LibraryArtist.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, selectOffset + 1, 28, 28);
                                            graphics.DrawImage(LibraryArtist.Artwork, destRect, 0, 0, LibraryArtist.Artwork.Width, LibraryArtist.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                    }
                                    break;
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum != null)
                                    {
                                        graphics.DrawString(LibraryAlbum.AlbumName, MusicPlayerProperties.Font, brush, destRect, format);
                                        if (LibraryAlbum.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, selectOffset + 1, 28, 28);
                                            graphics.DrawImage(LibraryAlbum.Artwork, destRect, 0, 0, LibraryAlbum.Artwork.Width, LibraryAlbum.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                    }
                                    break;
                                case MusicLibraryPage.Songs:
                                    if (LibrarySong != null)
                                    {
                                        graphics.DrawString(LibrarySong.Title, MusicPlayerProperties.Font, brush, destRect, format);
                                        if (LibrarySong.Artwork != null)
                                        {
                                            destRect = new Rectangle(34, selectOffset + 1, 28, 28);
                                            graphics.DrawImage(LibrarySong.Artwork, destRect, 0, 0, LibrarySong.Artwork.Width, LibrarySong.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    if (LibraryPage != MusicLibraryPage.Artists)
                    {

                    }
                    if (LibraryPage != MusicLibraryPage.Songs)
                    {

                    }
                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.checkmark, Color.Green, true, SoftButtons.Button3);
                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.cancel, Color.Red, true, SoftButtons.Button4);
                }
                SendImage(bmp);
                bmp.Dispose();
            }
            catch
            {
            }
        }
    }
}
