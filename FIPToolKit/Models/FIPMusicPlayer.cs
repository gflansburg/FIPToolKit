using FIPToolKit.Drawing;
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
using System.Security.Cryptography;
using FIPToolKit.FlightSim;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using FIPToolKit.Tools;
using System.Web;

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
        public Bitmap Artwork
        {
            get
            {
                if (Albums.Count > 0 && Albums.First().Songs.Count > 0)
                {
                    return Albums.First().IsPlaylist ? FIPToolKit.Properties.Resources.playlist : Albums.First().Songs.First().Artwork;
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
        public bool IsPlaylist { get; set; }
        public Bitmap Artwork
        {
            get
            {
                if (Songs.Count > 0)
                {
                    return IsPlaylist ? FIPToolKit.Properties.Resources.playlist : Songs.First().Artwork;
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
        public Bitmap Artwork { get; set; }

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
        public string Playlist { get; set; }
        public string Title {  get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public TimeSpan Duration { get; set; }
        public uint Year { get; set; }
        public Bitmap Artwork { get; set; }
        public uint Track { get; set; }
        public FIPMetaData MetaData { get; set; } = new FIPMetaData();
        
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
        private const int BufferSize = 1024 * 1024;

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
        private bool IsLoading { get; set; }
        private string Error { get; set; }

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
            libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-sout-video", "--quiet", "--no-video" });
            CreatePlayer();
        }

        public void Init()
        {
            if (!Initialized)
            {
                Initialized = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    LoadLibrary(MusicPlayerProperties.Resume);
                });
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
            IsLoading = false;
            if (Library != null)
            {
                Library.Dispose();
                Library = null;
            }
            MusicPlayerProperties.LastSong = null;
            MusicPlayerProperties.PlaylistType = MusicPlaylistType.Library;
            LibraryAlbum = null;
            LibraryArtist = null;
            LibrarySong = null;
            Playlist = null;
            IsLoading = false;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string cs = string.Format("{0}\\FIPToolKit.sqlite", FlightSim.Tools.GetExecutingDirectory());
                if (System.IO.File.Exists(cs))
                {
                    using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                    {
                        sqlConnection.Open();
                        using (var command = new SQLiteCommand("DELETE FROM Music", sqlConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                        sqlConnection.Close();
                    }
                }
                LoadLibrary(false);
            });
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
                        Error = "An Error Has Occured";
                        UpdatePage();
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
                    Error = null;
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
                try
                {
                    player.Stop();
                    player.Dispose();
                    player = null;
                }
                catch (Exception)
                {
                }
                try
                {
                    libVLC.Dispose();
                    libVLC = null;
                }
                catch (Exception)
                {
                }
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
                                PlayFirstSong(false);
                            }
                        }
                        else if (player != null && Library == null && !string.IsNullOrEmpty(MusicPlayerProperties.Path))
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                LoadLibrary(false);
                            });
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
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                LoadLibrary(false);
                            });
                        }
                        break;
                    case SoftButtons.Right:
                        if (player != null && Library != null && Playlist != null)
                        {
                            PlayNextSong();
                        }
                        else if (player != null && Library == null && !string.IsNullOrEmpty(MusicPlayerProperties.Path))
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                LoadLibrary(false);
                            });
                        }
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

        int itemCount = 0;
        private void LoadLibrary(bool isResuming)
        {
            if (Library == null)
            {
                IsLoading = true;
                UpdatePage();
                Library = new FIPMusicLibrary();
                if (!string.IsNullOrEmpty(MusicPlayerProperties.Path))
                {
                    itemCount = 0;
                    string cs = string.Format("{0}\\FIPToolKit.sqlite", FlightSim.Tools.GetExecutingDirectory());
                    if (System.IO.File.Exists(cs))
                    {
                        using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                        {
                            sqlConnection.Open();
                            LoadFolder(sqlConnection, MusicPlayerProperties.Path);
                            sqlConnection.Close();
                        }
                    }
                }
                if (IsLoading)
                {
                    foreach(FIPMusicArtist artist in Library.Artists)
                    {
                        if (!IsLoading)
                        {
                            break;
                        }
                        foreach (FIPMusicAlbum album in artist.Albums)
                        {
                            if (!IsLoading)
                            {
                                break;
                            }
                            if (!album.IsPlaylist)
                            {
                                album.Songs.Sort((x, y) => x.Title.CompareTo(y.Title));
                                album.Songs.Sort((x, y) => x.Track.CompareTo(y.Track));
                            }
                        }
                        if (IsLoading)
                        {
                            artist.Albums.Sort((x, y) => x.AlbumName.CompareTo(y.AlbumName));
                        }
                    }
                    if (IsLoading)
                    {
                        Library.Artists.Sort((x, y) => x.ArtistName.CompareTo(y.ArtistName));
                    }
                    if (IsLoading)
                    {
                        PlayFirstSong(true, isResuming);
                        UpdatePage();
                    }
                }
                IsLoading = false;
            }
        }

        private static byte[] ComputeHash(HashAlgorithm hashAlgorithm, System.IO.Stream stream)
        {
            byte[] readAheadBuffer, buffer;
            int readAheadBytesRead, bytesRead;
            readAheadBuffer = new byte[BufferSize];
            readAheadBytesRead = stream.Read(readAheadBuffer, 0, readAheadBuffer.Length);
            do
            {
                bytesRead = readAheadBytesRead;
                buffer = readAheadBuffer;
                readAheadBuffer = new byte[BufferSize];
                readAheadBytesRead = stream.Read(readAheadBuffer, 0, readAheadBuffer.Length);
                if (readAheadBytesRead == 0)
                {
                    hashAlgorithm.TransformFinalBlock(buffer, 0, bytesRead);
                }
                else
                {
                    hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                }
            }
            while (readAheadBytesRead != 0);
            return hashAlgorithm.Hash;
        }

        public static string CalculateMD5(string filename)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite, BufferSize))
                {
                    byte[] hash = ComputeHash(md5, stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private FIPMusicSong FindSong(SQLiteConnection sqlConnection, string hash)
        {
            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM Music WHERE Hash = '{0}'", hash.Replace("'", "''")), sqlConnection);
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    FIPMusicSong song = new FIPMusicSong()
                    {
                        Title = Null.SetNullString(reader["Title"]),
                        Artist = Null.SetNullString(reader["Artist"]),
                        Album = Null.SetNullString(reader["Album"]),
                        Genre = Null.SetNullString(reader["Genre"]),
                        Year = Null.SetNullUInt(reader["Year"]),
                        Track = Null.SetNullUInt(reader["Track"]),
                        Filename = Null.SetNullString(reader["Filename"])
                    };
                    if (reader["Artwork"] != DBNull.Value)
                    {
                        byte[] data = (byte[])reader["Artwork"];
                        if (data != null && data.Length > 0)
                        {
                            song.Artwork = new Bitmap(data.ByteArrayToImage());
                        }
                    }
                    else
                    {
                        song.Artwork = new Bitmap((song.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || song.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? FIPToolKit.Properties.Resources.Radio : FIPToolKit.Properties.Resources.Music).ChangeToColor(SystemColors.Highlight));
                    }
                    return song;
                }
            }
            return null;
        }

        private FIPMusicSong LoadSong(SQLiteConnection sqlConnection, string filename)
        {
            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM Music WHERE Filename = '{0}'", filename.Replace("'", "''")), sqlConnection);
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    FIPMusicSong song = new FIPMusicSong()
                    {
                        Title = Null.SetNullString(reader["Title"]),
                        Artist = Null.SetNullString(reader["Artist"]),
                        Album = Null.SetNullString(reader["Album"]),
                        Genre = Null.SetNullString(reader["Genre"]),
                        Year = Null.SetNullUInt(reader["Year"]),
                        Track = Null.SetNullUInt(reader["Track"]),
                        Filename = Null.SetNullString(reader["Filename"])
                    };
                    if (reader["Artwork"] != DBNull.Value)
                    {
                        byte[] data = (byte[])reader["Artwork"];
                        if (data != null && data.Length > 0)
                        {
                            song.Artwork = new Bitmap(data.ByteArrayToImage());
                        }
                    }
                    else
                    {
                        song.Artwork = new Bitmap((song.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || song.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? FIPToolKit.Properties.Resources.Radio : FIPToolKit.Properties.Resources.Music).ChangeToColor(SystemColors.Highlight));
                    }
                    return song;
                }
            }
            return null;
        }

        private FIPMusicSong LoadSongFromFile(SQLiteConnection sqlConnection, string filename, string hash, SimpleM3U8Parser.Media.M3u8Media media = null, string playlist = null)
        {
            FIPMusicSong song;
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    using (var tag = TagLib.File.Create(filename))
                    {
                        if (tag.Tag != null)
                        {
                            song = new FIPMusicSong()
                            {
                                Duration = tag.Properties.Duration,
                                Filename = filename,
                                Title = string.IsNullOrEmpty(tag.Tag.Title) ? "Unknown Title" : tag.Tag.Title,
                                Year = tag.Tag.Year,
                                Album = string.IsNullOrEmpty(tag.Tag.Album) ? "Unknown Album" : tag.Tag.Album,
                                Artist = string.IsNullOrEmpty(tag.Tag.FirstAlbumArtist) ? (string.IsNullOrEmpty(tag.Tag.FirstPerformer) ? "Unknown Artist" : tag.Tag.FirstPerformer) : tag.Tag.FirstAlbumArtist,
                                Genre = string.IsNullOrEmpty(tag.Tag.FirstGenre) ? "Unknown Genre" : tag.Tag.FirstGenre,
                                Track = tag.Tag.Track
                            };
                            if (tag.Tag.Pictures.Length > 0)
                            {
                                using (Image img = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data)))
                                {
                                    song.Artwork = new Bitmap(img);
                                }
                            }
                            else
                            {
                                song.Artwork = new Bitmap(FIPToolKit.Properties.Resources.Music.ChangeToColor(SystemColors.Highlight));
                            }
                        }
                        else
                        {
                            song = new FIPMusicSong()
                            {
                                Duration = tag.Properties != null ? tag.Properties.Duration : new TimeSpan(),
                                Filename = filename,
                                Title = Path.GetFileNameWithoutExtension(filename),
                                Album = "Unknown Album",
                                Artist = "Unknown Artist",
                                Genre = "Unknown Genre",
                                Artwork = new Bitmap(FIPToolKit.Properties.Resources.Music.ChangeToColor(SystemColors.Highlight))
                            };
                        }
                    }
                }
                else
                {
                    song = new FIPMusicSong()
                    {
                        Duration = media != null ? media.Duration : new TimeSpan(),
                        Filename = filename,
                        Title = media != null && !string.IsNullOrEmpty(media.Title) ? media.Title : Path.GetFileNameWithoutExtension(filename),
                        Album = !string.IsNullOrEmpty(playlist) ? playlist : "Unknown Album",
                        Artist = filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? "Stream" : "Unknown Artist",
                        Genre = "Unknown Genre",
                        Artwork = new Bitmap(FIPToolKit.Properties.Resources.Radio.ChangeToColor(SystemColors.Highlight))
                    };
                }
            }
            catch (Exception)
            {
                song = new FIPMusicSong()
                {
                    Duration = media != null ? media.Duration : new TimeSpan(),
                    Filename = filename,
                    Title = media != null && !string.IsNullOrEmpty(media.Title) ? media.Title : Path.GetFileNameWithoutExtension(filename),
                    Album = !string.IsNullOrEmpty(playlist) ? playlist : "Unknown Album",
                    Artist = filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? "Stream" : "Unknown Artist",
                    Genre = "Unknown Genre",
                    Artwork = new Bitmap((filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? FIPToolKit.Properties.Resources.Radio : FIPToolKit.Properties.Resources.Music).ChangeToColor(SystemColors.Highlight))
                };
            }
            FIPMusicSong exists = LoadSong(sqlConnection, song.Filename);
            if (exists != null)
            {
                using (var command = new SQLiteCommand("UPDATE Music SET Title = @title, Artist = @artist, Album = @album, Genre = @genre, Year = @year, Track = @track, Hash = @hash, Artwork = @artwork WHERE Filename = @filename", sqlConnection))
                {
                    command.Parameters.Add("@title", DbType.String).Value = song.Title;
                    command.Parameters.Add("@artist", DbType.String).Value = song.Artist;
                    command.Parameters.Add("@album", DbType.String).Value = song.Album;
                    command.Parameters.Add("@genre", DbType.String).Value = song.Genre;
                    command.Parameters.Add("@year", DbType.Int32).Value = song.Year;
                    command.Parameters.Add("@track", DbType.Int32).Value = song.Track;
                    command.Parameters.Add("@hash", DbType.String).Value = hash;
                    command.Parameters.Add("@filename", DbType.String).Value = filename;
                    command.Parameters.Add("@artwork", DbType.Binary).Value = song.Artwork != null ? song.Artwork.ImageToByteArray() : null;
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var command = new SQLiteCommand("INSERT INTO Music (Title, Artist, Album, Genre, Year, Track, Hash, Filename, Artwork) VALUES (@title, @artist, @album, @genre, @year, @track, @hash, @filename, @artwork)", sqlConnection))
                {
                    command.Parameters.Add("@title", DbType.String).Value = song.Title;
                    command.Parameters.Add("@artist", DbType.String).Value = song.Artist;
                    command.Parameters.Add("@album", DbType.String).Value = song.Album;
                    command.Parameters.Add("@genre", DbType.String).Value = song.Genre;
                    command.Parameters.Add("@year", DbType.Int32).Value = song.Year;
                    command.Parameters.Add("@track", DbType.Int32).Value = song.Track;
                    command.Parameters.Add("@hash", DbType.String).Value = hash;
                    command.Parameters.Add("@filename", DbType.String).Value = filename;
                    command.Parameters.Add("@artwork", DbType.Binary).Value = song.Artwork != null ? song.Artwork.ImageToByteArray() : null;
                    command.ExecuteNonQuery();
                }
            }
            return song;
        }

        private void LoadFolder(SQLiteConnection sqlConnection, string path)
        {
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".wma", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m3u", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m3u8", StringComparison.OrdinalIgnoreCase));
            var folders = Directory.GetDirectories(path);
            foreach(string folder in folders)
            {
                if (!IsLoading)
                {
                    break;
                }
                LoadFolder(sqlConnection, folder);
            }
            if (IsLoading)
            {
                foreach (var file in files)
                {
                    if (!IsLoading)
                    {
                        break;
                    }
                    FIPMusicSong song;
                    string hash = CalculateMD5(file);
                    song = Path.GetExtension(file).Equals(".m3u", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(file).Equals(".m3u8", StringComparison.OrdinalIgnoreCase) ? null : FindSong(sqlConnection, hash);
                    bool draw = false;
                    if (song == null)
                    {
                        if (Path.GetExtension(file).Equals(".m3u", StringComparison.OrdinalIgnoreCase))
                        {
                            M3UParser.Extm3u playlist = M3UParser.M3U.ParseFromFile(file);
                            foreach(M3UParser.Media media in playlist.Medias)
                            {
                                hash = System.IO.File.Exists(media.MediaFile) ? CalculateMD5(media.MediaFile) : Guid.NewGuid().ToString().Replace("-", string.Empty);
                                draw = false;
                                song = System.IO.File.Exists(media.MediaFile) ? FindSong(sqlConnection, hash) : null;
                                if (song == null)
                                {
                                    draw = true;
                                    song = LoadSongFromFile(sqlConnection, media.MediaFile, hash);
                                }
                                song.Playlist = Path.GetFileNameWithoutExtension(file);
                                if (itemCount % 10 == 0 || draw)
                                {
                                    UpdateLoading(song.Artist, song.Title);
                                }
                                itemCount++;
                                FIPMusicAlbum album = GetAlbum("Playlists", song.Playlist);
                                album.IsPlaylist = true;
                                album.Songs.Add(song);
                            }
                        }
                        else if (Path.GetExtension(file).Equals(".m3u8", StringComparison.OrdinalIgnoreCase))
                        {
                            SimpleM3U8Parser.Media.M3u8MediaContainer playlist = SimpleM3U8Parser.M3u8Parser.ParseFromFile(file);
                            foreach(SimpleM3U8Parser.Media.M3u8Media media in playlist.Medias)
                            {
                                hash = System.IO.File.Exists(media.Path) ? CalculateMD5(media.Path) : Guid.NewGuid().ToString().Replace("-", string.Empty);
                                draw = false;
                                song = System.IO.File.Exists(media.Path) ? FindSong(sqlConnection, hash) : null;
                                if (song == null)
                                {
                                    draw = true;
                                    song = LoadSongFromFile(sqlConnection, media.Path, hash, media, Path.GetFileNameWithoutExtension(file));
                                }
                                song.Playlist = Path.GetFileNameWithoutExtension(file);
                                if (itemCount % 10 == 0 || draw)
                                {
                                    UpdateLoading(song.Artist, song.Title);
                                }
                                itemCount++;
                                FIPMusicAlbum album = GetAlbum("Playlists", song.Playlist);
                                album.IsPlaylist = true;
                                album.Songs.Add(song);
                            }
                        }
                        else
                        {
                            draw = true;
                            song = LoadSongFromFile(sqlConnection, file, hash);
                            if (itemCount % 10 == 0 || draw)
                            {
                                UpdateLoading(song.Artist, song.Title);
                            }
                            itemCount++;
                            FIPMusicAlbum album = GetAlbum(song.Artist, song.Album);
                            album.Songs.Add(song);
                        }
                    }
                    else
                    {
                        if (itemCount % 10 == 0 || draw)
                        {
                            UpdateLoading(song.Artist, song.Title);
                        }
                        itemCount++;
                        FIPMusicAlbum album = GetAlbum(song.Artist, song.Album);
                        album.Songs.Add(song);
                    }
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
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Song;
                Playlist.Add(LibrarySong);
            }
            else if (LibraryAlbum != null)
            {
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Album;
                foreach (FIPMusicSong song in LibraryAlbum.Songs)
                {
                    Playlist.Add(song);
                }
            }
            else if (LibraryArtist != null)
            {
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Artist;
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
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Library;
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
            if (player != null && (player.IsPlaying || IsLoading || opening) && !resume)
            {
                resume = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (!IsLoading && !opening)
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
            if (Library != null)
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
            }
            return null;
        }

        private void PlayFirstSong(bool firstPlay, bool isResuming = false)
        {
            if (Library != null)
            {
                if (isResuming && MusicPlayerProperties.Resume)
                {
                    LibrarySong = FindSong(MusicPlayerProperties.LastSong);
                    if (LibrarySong != null)
                    {
                        if (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Song)
                        {
                            LibraryAlbum = GetAlbum(!string.IsNullOrEmpty(LibrarySong.Playlist) ? "Playlists" : LibrarySong.Artist, !string.IsNullOrEmpty(LibrarySong.Playlist) ? LibrarySong.Playlist : LibrarySong.Album);
                            LibraryArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryAlbum.IsPlaylist ? "Playlists" : LibrarySong.Artist, StringComparison.OrdinalIgnoreCase));
                        }
                        else if (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Album)
                        {
                            LibraryAlbum = GetAlbum(!string.IsNullOrEmpty(LibrarySong.Playlist) ? "Playlists" : LibrarySong.Artist, !string.IsNullOrEmpty(LibrarySong.Playlist) ? LibrarySong.Playlist : LibrarySong.Album);
                            LibraryArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryAlbum.IsPlaylist ? "Playlists" : LibrarySong.Artist, StringComparison.OrdinalIgnoreCase));
                            LibrarySong = null;
                        }
                        else if (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Artist)
                        {
                            LibraryArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryAlbum.IsPlaylist ? "Playlists" : LibrarySong.Artist, StringComparison.OrdinalIgnoreCase));
                            LibrarySong = null;
                            LibraryAlbum = null;
                        }
                        else
                        {
                            LibrarySong = null;
                            LibraryAlbum = null;
                            LibraryArtist = null;
                        }
                        CreatePlaylist();
                    }
                }
                if (Playlist == null)
                {
                    CreatePlaylist();
                }
                if ((CurrentSong == null || !isResuming) && Playlist != null && Playlist.Count > 0)
                {
                    if (isResuming && MusicPlayerProperties.Resume)
                    {
                        CurrentSong = FindSong(MusicPlayerProperties.LastSong);
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
                        }
                    }
                    else
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
                }
                if (CurrentSong != null)
                {
                    CurrentAlbum = GetAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                    CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
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
                        CurrentAlbum = GetAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
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
                            CurrentAlbum = GetAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
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
                        CurrentAlbum = GetAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
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
                            CurrentAlbum = GetAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
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
            if (!string.IsNullOrEmpty(filename))
            {
                CreatePlayer();
                UpdatePlayer();
                MusicPlayerProperties.LastSong = filename;
                if (filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    media = new Media(libVLC, filename, FromType.FromLocation);
                }
                else if (System.IO.File.Exists(filename))
                {
                    media = new Media(libVLC, filename);
                }
                if (media != null)
                {
                    media.MetaChanged += Media_MetaChanged;
                    player.Media = media;
                    if (!resume)
                    {
                        player.Play();
                    }
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
            if (player != null)
            {
                string meta = player.Media.Meta(e.MetadataType);
                if (!string.IsNullOrEmpty(meta) && e.MetadataType == MetadataType.ArtworkURL)
                {
                    bool isLocal = false;
                    if (meta.StartsWith("file:///"))
                    {
                        meta = HttpUtility.UrlDecode(meta.Substring(8)).Replace("/", "\\");
                        isLocal = true;
                    }
                    else if (meta.StartsWith("file://"))
                    {
                        meta = HttpUtility.UrlDecode(meta.Substring(7)).Replace("/", "\\");
                        isLocal = true;
                    }
                    if (isLocal && System.IO.File.Exists(meta))
                    {
                        CurrentSong.MetaData.Artwork = new Bitmap(Bitmap.FromFile(meta));
                    }
                    else if (System.IO.File.Exists(CurrentSong.Filename))
                    {
                        try
                        {
                            using (var tag = TagLib.File.Create(CurrentSong.Filename))
                            {
                                if (tag.Tag != null)
                                {
                                    if (tag.Tag.Pictures.Length > 0)
                                    {
                                        using (Image img = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data)))
                                        {
                                            CurrentSong.MetaData.Artwork = new Bitmap(img);
                                        }
                                        UpdatePage();
                                    }
                                    else
                                    {
                                        CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                                        UpdatePage();
                                    }
                                }
                                else
                                {
                                    CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                                    UpdatePage();
                                }
                            }
                        }
                        catch (Exception)
                        {
                            CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                            UpdatePage();
                        }
                    }
                    else if (CurrentSong.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || CurrentSong.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                        UpdatePage();
                    }
                }
                else if (e.MetadataType == MetadataType.NowPlaying)
                {
                    if (CurrentSong.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || CurrentSong.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        if (CurrentSong.Title.Equals(Path.GetFileNameWithoutExtension(CurrentSong.Filename)))
                        {
                            if (!string.IsNullOrEmpty(meta))
                            {
                                CurrentSong.MetaData.Title = meta;
                                CurrentSong.MetaData.Artist = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : player.Media.Meta(MetadataType.Title);
                            }
                            else
                            {
                                CurrentSong.MetaData.Title = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : player.Media.Meta(MetadataType.Title);
                                CurrentSong.MetaData.Artist = string.IsNullOrEmpty(player.Media.Meta(MetadataType.AlbumArtist)) ? (string.IsNullOrEmpty(player.Media.Meta(MetadataType.Artist)) ? CurrentSong.Artist : player.Media.Meta(MetadataType.Artist)) : player.Media.Meta(MetadataType.AlbumArtist);
                            }
                        }
                        else
                        {
                            CurrentSong.MetaData.Title = CurrentSong.Title;
                            CurrentSong.MetaData.Artist = !string.IsNullOrEmpty(meta) ? meta : string.IsNullOrEmpty(player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : player.Media.Meta(MetadataType.Title);
                        }
                        CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                    }
                    else
                    {
                        CurrentSong.MetaData.Title = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : player.Media.Meta(MetadataType.Title);
                        CurrentSong.MetaData.Artist = string.IsNullOrEmpty(player.Media.Meta(MetadataType.AlbumArtist)) ? (string.IsNullOrEmpty(player.Media.Meta(MetadataType.Artist)) ? CurrentSong.Artist : player.Media.Meta(MetadataType.Artist)) : player.Media.Meta(MetadataType.AlbumArtist);
                    }
                    CurrentSong.MetaData.Album = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Album)) ? CurrentSong.Album : player.Media.Meta(MetadataType.Album);
                    CurrentSong.MetaData.Genre = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Genre)) ? CurrentSong.Genre : player.Media.Meta(MetadataType.Genre);
                    CurrentSong.MetaData.Track = Convert.ToUInt32(string.IsNullOrEmpty(player.Media.Meta(MetadataType.TrackNumber)) ? CurrentSong.Track.ToString() : player.Media.Meta(MetadataType.TrackNumber));
                    UpdatePage();
                }
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

        private void UpdateLoading(string artist, string title)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb))
                {
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
                                graphics.DrawString(string.Format("Indexing\n\n{0}\n{1}", title, artist), MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                        }
                    }
                    SendImage(bmp);
                }
            }
            catch
            {
            }
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
                            if (IsLoading)
                            {
                                graphics.DrawString("Loading...", MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            else if (Library == null)
                            {
                                graphics.DrawString("Please choose a path to your music library", MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            else if (CurrentSong != null)
                            {
                                if ((CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork : CurrentSong.Artwork) != null)
                                {
                                    int titleHeight = (int)graphics.MeasureString(CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, MusicPlayerProperties.Font, 288, format).Height;
                                    int artistHeight = (int)graphics.MeasureString(!string.IsNullOrEmpty(Error) ? Error : CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, 288, format).Height;
                                    int maxImageWidth = 320 - 34;
                                    int maxImageHeight = 240 - (titleHeight + artistHeight);
                                    //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                                    double ratioX = (double)maxImageWidth / (CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width);
                                    double ratioY = (double)maxImageHeight / (CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height);
                                    double ratio = Math.Min(ratioX, ratioY);
                                    int imageWidth = (int)((CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width) * ratio);
                                    int imageHeight = (int)((CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height) * ratio);
                                    Rectangle destRect = new Rectangle(17 + ((320 - imageWidth) / 2), (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                                    graphics.DrawImage(CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork : CurrentSong.Artwork, destRect, 0, 0, CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Width : CurrentSong.Artwork.Width, CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artwork.Height : CurrentSong.Artwork.Height, GraphicsUnit.Pixel);
                                    graphics.DrawString(CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, MusicPlayerProperties.Font, brush, new RectangleF(32, maxImageHeight, 288, titleHeight), format);
                                    graphics.DrawString(!string.IsNullOrEmpty(Error) ? Error : CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, brush, new RectangleF(32, maxImageHeight + titleHeight, 288, artistHeight), format);
                                }
                                else
                                {
                                    string text = string.Format("{0}\n{1}", CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Title : CurrentSong.Title, CurrentAlbum.IsPlaylist ? CurrentSong.MetaData.Artist : !string.IsNullOrEmpty(Error) ? Error : CurrentSong.Artist);
                                    graphics.DrawString(text, MusicPlayerProperties.Font, brush, new RectangleF(32, 0, 288, 240), format);
                                }
                                graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, player.Mute ? Color.Red : Color.Green, true, SoftButtons.Button1);
                                graphics.AddButtonIcon(player.State == VLCState.Playing ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, player.State == VLCState.Playing ? Color.Yellow : Color.Blue, true, SoftButtons.Button2);
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.shuffle, MusicPlayerProperties.Shuffle == true ? Color.Green : Color.White, true, SoftButtons.Button3);
                                graphics.AddButtonIcon(MusicPlayerProperties.Repeat == MusicRepeatState.Track ? FIPToolKit.Properties.Resources.repeat_one : FIPToolKit.Properties.Resources.repeat, MusicPlayerProperties.Repeat != MusicRepeatState.Off ? Color.Green : Color.White, true, SoftButtons.Button4);
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.playlist, Color.Orange, true, SoftButtons.Button6);
                            }
                            else
                            {
                                graphics.DrawString("Initializing...", MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
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
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            format.Trimming = StringTrimming.EllipsisCharacter;
                            int page = 0;
                            Rectangle destRect;
                            switch (LibraryPage)
                            {
                                case MusicLibraryPage.Artists:
                                    if (LibraryArtist != null)
                                    {
                                        page = Library.Artists.IndexOf(Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryArtist.ArtistName, StringComparison.OrdinalIgnoreCase))) / 5;
                                    }
                                    destRect = new Rectangle(0, 0, 320, 40);
                                    graphics.DrawString("Artists", MusicPlayerProperties.Font, brush, destRect, format);
                                    break;
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum != null)
                                    {
                                        page = LibraryArtist.Albums.IndexOf(LibraryArtist.Albums.FirstOrDefault(a => a.AlbumName.Equals(LibraryAlbum.AlbumName, StringComparison.OrdinalIgnoreCase))) / 5;
                                    }
                                    if (LibraryArtist != null)
                                    {
                                        destRect = new Rectangle(0, 0, 320, 40);
                                        graphics.DrawString(LibraryArtist.ArtistName, MusicPlayerProperties.Font, brush, destRect, format);
                                    }
                                    break;
                                default:
                                    if (LibrarySong != null)
                                    {
                                        page = LibraryAlbum.Songs.IndexOf(LibraryAlbum.Songs.FirstOrDefault(s => s.Title.Equals(LibrarySong.Title, StringComparison.OrdinalIgnoreCase))) / 5;
                                    }
                                    if (LibraryAlbum != null)
                                    {
                                        destRect = new Rectangle(0, 0, 320, 40);
                                        graphics.DrawString(LibraryAlbum.AlbumName, MusicPlayerProperties.Font, brush, destRect, format);
                                    }
                                    break;
                            }
                            format.Alignment = StringAlignment.Near;
                            //Draw playlists for currenttly displayed page
                            for (int i = 0; i < 5; i++)
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
                                if (i + (page * 5) >= count)
                                {
                                    break;
                                }
                                switch(LibraryPage)
                                {
                                    case MusicLibraryPage.Artists:
                                        FIPMusicArtist artist = Library.Artists[i + (page * 5)];
                                        if (artist == LibraryArtist)
                                        {
                                            graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        if (artist.Artwork != null)
                                        {
                                            graphics.DrawImage(artist.Artwork, destRect, 0, 0, artist.Artwork.Width, artist.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (artist.ArtistName.Equals("Playlists"))
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.playlist, destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        graphics.DrawString(artist.ArtistName, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                    case MusicLibraryPage.Albums:
                                        FIPMusicAlbum album = LibraryArtist.Albums[i + (page * 5)];
                                        if (album == LibraryAlbum)
                                        {
                                            graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        if (album.Artwork != null)
                                        {
                                            graphics.DrawImage(album.Artwork, destRect, 0, 0, album.Artwork.Width, album.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (album.IsPlaylist)
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.playlist, destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        graphics.DrawString(album.AlbumName, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                    default:
                                        FIPMusicSong song = LibraryAlbum.Songs[i + (page * 5)];
                                        if (song == LibrarySong)
                                        {
                                            graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        if (song.Artwork != null)
                                        {
                                            graphics.DrawImage(song.Artwork, destRect, 0, 0, song.Artwork.Width, song.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (LibraryAlbum.IsPlaylist)
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.playlist, destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        graphics.DrawString(song.Title, MusicPlayerProperties.Font, brush, destRect, format);
                                        break;
                                }
                            }
                        }
                    }
                    if (LibraryPage == MusicLibraryPage.Artists)
                    {
                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Albums, Color.Blue, true, SoftButtons.Button2);
                    }
                    else if (LibraryPage == MusicLibraryPage.Albums)
                    {
                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.UpFolderLevel, Color.Yellow, true, SoftButtons.Button1);
                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Songs, Color.Blue, true, SoftButtons.Button2);
                    }
                    else if (LibraryPage == MusicLibraryPage.Songs)
                    {
                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.UpFolderLevel, Color.Yellow, true, SoftButtons.Button1);
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
