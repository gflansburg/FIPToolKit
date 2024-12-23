﻿using FIPToolKit.Drawing;
using LibVLCSharp.Shared;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Saitek.DirectOutput;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Data;
using FIPToolKit.Tools;
using System.Web;
using M3U.Media;
using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPMusicLibrary : IDisposable
    {
        private List<FIPMusicArtist> _artists { get; set; } = new List<FIPMusicArtist>();
        public IEnumerable<FIPMusicArtist> Artists
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0);
                }
            }
        }

        public List<FIPMusicSong> Songs
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).SelectMany(a => a.Songs).ToList();
                }
            }
        }

        public FIPMusicArtist FirstArtist
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).FirstOrDefault();
                }
            }
        }

        public FIPMusicArtist LastArtist
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).LastOrDefault();
                }
            }
        }

        public FIPMusicAlbum FirstAlbum
        {
            get
            {
                if (FirstArtist != null)
                {
                    return FirstArtist.Albums.FirstOrDefault();
                }
                return null;
            }
        }

        public FIPMusicAlbum LastAlbum
        {
            get
            {
                if (LastArtist != null)
                {
                    return LastArtist.Albums.LastOrDefault();
                }
                return null;
            }
        }

        public FIPMusicSong FirstSong
        {
            get
            {
                if (FirstAlbum != null)
                {
                    return FirstAlbum.Songs.FirstOrDefault();
                }
                return null;
            }
        }

        public FIPMusicSong LastSong
        {
            get
            {
                if (LastAlbum != null)
                {
                    return LastAlbum.Songs.LastOrDefault();
                }
                return null;
            }
        }

        public int ArtistCount
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).Count();
                }
            }
        }

        public int AlbumCount
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).Sum(a => a.AlbumCount);
                }
            }
        }

        public int SongCount
        {
            get
            {
                lock (_artists)
                {
                    return _artists.Where(a => a.AlbumCount > 0).Sum(a => a.SongCount);
                }
            }
        }

        public void UpdateListenerLocation(LatLong location)
        {
            lock (_artists)
            {
                foreach (FIPMusicArtist artist in _artists)
                {
                    artist.UpdateListenerLocation(location);
                }
            }
        }

        public void UpdateRadioDistance(Models.RadioDistance radioDistance)
        {
            lock (_artists)
            {
                foreach (FIPMusicArtist artist in _artists)
                {
                    artist.UpdateRadioDistance(radioDistance);
                }
            }
        }

        public FIPMusicAlbum GetAlbum(string artistName, string albumName, Models.RadioDistance radioDistance)
        {
            lock (_artists)
            {
                FIPMusicArtist artist = _artists.FirstOrDefault(a => a.ArtistName.Equals(artistName, StringComparison.OrdinalIgnoreCase));
                if (artist == null)
                {
                    artist = new FIPMusicArtist()
                    {
                        ArtistName = artistName
                    };
                    _artists.Add(artist);
                }
                FIPMusicAlbum album = artist.FindAlbum(albumName);
                if (album == null)
                {
                    album = new FIPMusicAlbum(artistName)
                    {
                        AlbumName = albumName,
                        RadioDistance = radioDistance
                    };
                    artist.AddAlbum(album);
                }
                return album;
            }
        }

        public void Sort()
        {
            _artists.Sort((x, y) => x.ArtistName.CompareTo(y.ArtistName));
        }

        public void Dispose()
        {
            lock (_artists)
            {
                foreach (var artist in _artists)
                {
                    artist.Dispose();
                }
            }
        }
    }

    public class FIPMusicArtist : IDisposable
    {
        private List<FIPMusicAlbum> _albums { get; set; } = new List<FIPMusicAlbum>();
        public string ArtistName { get; set; }
        public IEnumerable<FIPMusicAlbum> Albums
        { 
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0);
                }
            }
        }

        public void AddAlbum(FIPMusicAlbum album)
        {
            lock (_albums)
            {
                _albums.Add(album);
            }
        }

        public FIPMusicAlbum FirstAlbum
        {
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0).FirstOrDefault();
                }
            }
        }

        public FIPMusicAlbum LastAlbum
        {
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0).LastOrDefault();
                }
            }
        }

        public List<FIPMusicSong> Songs
        {
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0).SelectMany(a => a.Songs).ToList();
                }
            }
        }

        public FIPMusicSong FirstSong
        {
            get
            {
                if (FirstAlbum != null && FirstAlbum.SongCount > 0)
                {
                    return FirstAlbum.Songs.First();
                }
                return null;
            }
        }

        public FIPMusicSong LastSong
        {
            get
            {
                if (LastAlbum != null && LastAlbum.SongCount > 0)
                {
                    return LastAlbum.LastSong;
                }
                return null;
            }
        }

        public int AlbumCount
        {
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0).Count();
                }
            }
        }

        public int SongCount
        {
            get
            {
                lock (_albums)
                {
                    return _albums.Where(a => a.SongCount > 0).Sum(s => s.SongCount);
                }
            }
        }

        public Bitmap Artwork
        {
            get
            {
                if (FirstAlbum != null && FirstAlbum.SongCount > 0)
                {
                    return FirstAlbum.FirstSong.Artwork;
                }
                return null;
            }
        }

        public void Sort()
        {
            lock (_albums)
            {
                _albums.Sort((x, y) => x.AlbumName.CompareTo(y.AlbumName));
            }
        }

        public void UpdateListenerLocation(LatLong location)
        {
            lock (_albums)
            {
                foreach (FIPMusicAlbum album in _albums)
                {
                    album.UpdateListenerLocation(location);
                }
            }
        }

        public void UpdateRadioDistance(Models.RadioDistance radioDistance)
        {
            lock (_albums)
            {
                foreach (FIPMusicAlbum album in _albums)
                {
                    album.RadioDistance = radioDistance;
                }
            }
        }

        public FIPMusicAlbum FindAlbum(string album)
        {
            lock (_albums)
            {
                return _albums.FirstOrDefault(a => a.AlbumName.Equals(album, StringComparison.OrdinalIgnoreCase));
            }
        }

        public void Dispose()
        {
            lock (_albums)
            {
                foreach (var album in _albums)
                {
                    album.Dispose();
                }
            }
        }
    }

    public class FIPMusicAlbum : IDisposable
    {
        public string Artist { get; private set; }
        private List<FIPMusicSong> _songs { get; set; } = new List<FIPMusicSong>();
        public string AlbumName { get; set; }
        public Models.RadioDistance RadioDistance { get; set; } = RadioDistance.Any;
        public bool IsPlaylist { get; set; }

        public FIPMusicAlbum(string artist)
        {
            Artist = artist;
        }

        public IEnumerable<FIPMusicSong> Songs 
        { 
            get
            {
                lock (_songs)
                {
                    return _songs.Where(s => RadioDistance == RadioDistance.Any || s.Distance <= RadioDistance.Double());
                }
            }
        }

        public int SongCount
        {
            get
            {
                lock (_songs)
                {
                    return _songs.Where(s => RadioDistance == RadioDistance.Any || s.Distance <= RadioDistance.Double()).Count();
                }
            }
        }

        public FIPMusicSong FirstSong
        {
            get
            {
                lock (_songs)
                {
                    return _songs.Where(s => RadioDistance == RadioDistance.Any || s.Distance <= RadioDistance.Double()).FirstOrDefault();
                }
            }
        }

        public FIPMusicSong LastSong
        {
            get
            {
                lock (_songs)
                {
                    return _songs.Where(s => RadioDistance == RadioDistance.Any || s.Distance <= RadioDistance.Double()).LastOrDefault();
                }
            }
        }

        public Bitmap Artwork
        {
            get
            {
                if (SongCount > 0)
                {
                    return FirstSong != null ? FirstSong.Artwork : null;
                }
                return null;
            }
        }

        public void AddSong(FIPMusicSong song)
        {
            lock (_songs)
            {
                _songs.Add(song);
            }
        }

        public void Sort()
        {
            lock (_songs)
            {
                _songs.Sort((x, y) => x.Title.CompareTo(y.Title));
                _songs.Sort((x, y) => x.Track.CompareTo(y.Track));
            }
        }

        public void UpdateListenerLocation(LatLong location)
        {
            lock (_songs)
            {
                foreach (FIPMusicSong song in _songs)
                {
                    song.ListenerLocation = location;
                }
            }
        }

        public void Dispose()
        {
            lock (_songs)
            {
                foreach (var song in _songs)
                {
                    song.Dispose();
                }
            }
        }
    }

    public class FIPMetaData : IDisposable
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

    public class FIPMusicSong : IDisposable
    {
        public event EventHandler OnArtworkDownloaded;

        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string Playlist { get; set; }
        public string Title {  get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public TimeSpan Duration { get; set; }
        public uint Year { get; set; }
        public uint Track { get; set; }
        public FIPMetaData MetaData { get; set; }
        public M3UMedia M3UMedia { get; set; }
        public LatLong ListenerLocation { get; set; }
        public string LogoUrl { get; set; }

        private LatLong _streamLocation;
        public LatLong StreamLocation 
        { 
            get
            {
                if (_streamLocation != null)
                {
                    return _streamLocation;
                }
                if (M3UMedia != null && M3UMedia.Attributes != null && M3UMedia.Attributes.FipLatitude.HasValue && M3UMedia.Attributes.FipLongitude.HasValue)
                {
                    return new LatLong(M3UMedia.Attributes.FipLatitude.Value, M3UMedia.Attributes.FipLongitude.Value);
                }
                return null;
            }
            set
            {
                _streamLocation = value;
            }
        }

        public bool IsStream
        {
            get
            {
                return !string.IsNullOrEmpty(Filename) && Filename.IsStream();
            }
        }

        public bool IsAudio
        {
            get
            {
                return Filename.IsAudio();
            }
        }

        public bool IsVideo
        {
            get
            {
                return Filename.IsVideo();
            }
        }

        private Bitmap _artwork = null;
        private bool _checked = false;
        public Bitmap Artwork 
        { 
            get
            {
                if (_artwork == null && !string.IsNullOrEmpty(LogoUrl) && !_checked)
                {
                    _checked = true;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {

                        Bitmap img = LogoUrl.DownloadImageFromUrl();
                        if (img != null)
                        {
                            try
                            {
                                _artwork = img;
                            }
                            catch(Exception)
                            {
                            }
                            if (MetaData != null)
                            {
                                MetaData.Artwork = new Bitmap(_artwork);
                            }
                            OnArtworkDownloaded?.Invoke(this, new EventArgs());
                        }
                    });
                }
                else if (_artwork == null && File.Exists(Filename))
                {
                    _checked = true;
                    try
                    {
                        using (var tag = TagLib.File.Create(Filename))
                        {
                            if (tag.Tag != null)
                            {
                                if (tag.Tag.Pictures.Length > 0)
                                {
                                    using (Image img = Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data)))
                                    {
                                        _artwork = new Bitmap(img);
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception)
                    {
                    }
                }
                return _artwork;
            }
            set
            {
                _artwork = value;
            }
        }

        public double Distance
        {
            get
            {
                if (ListenerLocation != null && StreamLocation != null)
                {
                    return Net.DistanceBetween(StreamLocation.Latitude.Value, StreamLocation.Longitude.Value, ListenerLocation.Latitude.Value, ListenerLocation.Longitude.Value);
                }
                return -1;
            }
        }

        public FIPMusicSong(string artist, string album)
        {
            Artist = artist;
            Album = album;
        }

        public void Dispose()
        {
            if (_artwork != null)
            {
                _artwork.Dispose();
                _artwork = null;
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
        public Media Media { get; private set; }
        public bool Opening { get; set; }
        public FIPMusicLibrary Library { get; set; }
        public FIPMusicSong CurrentSong { get; set; }
        public FIPMusicArtist CurrentArtist { get; set; }
        public FIPMusicAlbum CurrentAlbum { get; set; }
        public MusicPlayerPage CurrentPage { get; set; }
        private MusicLibraryPage LibraryPage { get; set; }
        public FIPMusicArtist LibraryArtist { get; set; }
        public FIPMusicAlbum LibraryAlbum { get; set; }
        public FIPMusicSong LibrarySong { get; set; }
        private int LibraryIndex { get; set; }
        public List<FIPMusicSong> Playlist { get; set; }
        public List<int> RandomList { get; set; }
        private bool EndOfPlaylist { get; set; }
        public bool Initialized { get; private set; }
        public bool IsLoading { get; set; }
        private string Error { get; set; }
        public int PlaylistIndex { get; private set; }
        public bool IsPlaying { get; set; }
        public bool Resume { get; set; }
        public int LibraryPageNumber { get; set; }


        public event FIPPageEventHandler OnSongChanged;
        public event FIPPageEventHandler OnEndOfPlaylist;
        public event FIPSpotifyPlayer.FIPCanPlayEventHandler OnCanPlay;
        public event FIPPageEventHandler OnMuteChanged;
        public event FIPPageEventHandler OnVolumeChanged;

        public LatLong LocalLocation { get; private set; }

        private LatLong _listenerLocation = null;
        public LatLong ListenerLocation
        {
            get
            {
                return _listenerLocation??LocalLocation;
            }
            set
            {
                if (_listenerLocation != value)
                {
                    _listenerLocation = value;
                    UpdateListenerLocation();
                }
            }
        }

        public MediaPlayer Player
        {
            get
            {
                return player;
            }
        }

        public FIPMusicPlayer(FIPMusicPlayerProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnVolumeChanged += Properties_OnVolumeChanged;
            properties.OnPathChanged += Properties_OnPathChanged;
            properties.OnShuffleChanged += Properties_OnShuffleChanged;
            properties.OnRepeatChanged += Properties_OnRepeatChanged;
            properties.OnRadioDistanceChanged += Properties_OnRadioDistanceChanged;
            properties.OnMuteChanged += Properties_OnMuteChanged;
            CreatePlayer();
            SetLocalLocation();
        }

        public static void InitializeCore()
        {
            try
            {
                Core.Initialize(FlightSim.Tools.GetExecutingDirectory() + "\\libvlc\\win-x64");
            }
            catch (Exception)
            {
            }
        }

        private void Properties_OnMuteChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null && player.Mute != MusicPlayerProperties.Mute)
                {
                    player.Mute = MusicPlayerProperties.Mute;
                }
                UpdatePage();
            });
            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        public virtual void OnSetLocalLocation(LatLong location)
        {
        }

        public void SetLocalLocation()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string ipAddress = Net.GetIPAddress();
                IP2Location location = Net.GetLocation(ipAddress);
                if (location != null)
                {
                    LocalLocation = new LatLong(location.latitude, location.longitude);
                    OnSetLocalLocation(LocalLocation);
                }
            });
        }

        public virtual void OnRadioDistanceChanged()
        {
            if (Library != null)
            {
                Library.UpdateRadioDistance(MusicPlayerProperties.RadioDistance);
                if (!IsLoading)
                {
                    LibrarySong = null;
                    LibraryAlbum = null;
                    LibraryArtist = null;
                    if (CurrentArtist != null && (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Artist || MusicPlayerProperties.PlaylistType == MusicPlaylistType.Album))
                    {
                        LibraryArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentArtist.ArtistName, StringComparison.OrdinalIgnoreCase));
                        if (CurrentAlbum != null && LibraryArtist != null && (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Album))
                        {
                            LibraryAlbum = LibraryArtist.Albums.FirstOrDefault(a => a.AlbumName.Equals(CurrentAlbum.AlbumName, StringComparison.OrdinalIgnoreCase));
                            if (CurrentSong != null && LibraryAlbum != null)
                            {
                                LibrarySong = LibraryAlbum.Songs.FirstOrDefault(a => a.Filename.Equals(CurrentSong.Filename, StringComparison.OrdinalIgnoreCase));
                            }
                        }
                    }
                    CreatePlaylist();
                }
            }
        }

        private void Properties_OnRadioDistanceChanged(object sender, EventArgs e)
        {
            OnRadioDistanceChanged();
        }

        public virtual string EmptySongCountMessage
        {
            get
            {
                return "You do not have any songs or radio streams in your library or there are no radio stations in range.";
            }
        }

        public virtual string EmptyPlaylistMessage
        {
            get
            {
                return "Playlist Is Empty";
            }
        }

        public virtual string EmptySongMessage
        {
            get
            {
                return "Initializing...";
            }
        }

        private void UpdateListenerLocation()
        {
            if (Library != null)
            {
                Library.UpdateListenerLocation(ListenerLocation);
            }
        }

        public virtual void Init()
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
            ThreadPool.QueueUserWorkItem(_ =>
            {
                LoadLibrary(false);
            });
        }

        private void Properties_OnVolumeChanged(object sender, EventArgs e)
        {
            if (player != null && player.Volume != MusicPlayerProperties.Volume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    player.Volume = MusicPlayerProperties.Volume;
                });
            }
            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        public FIPMusicPlayerProperties MusicPlayerProperties
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
            if (libVLC == null)
            {
                try
                {
                    Core.Initialize(FlightSim.Tools.GetExecutingDirectory() + "\\libvlc\\win-x64");
                    libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-sout-video", "--quiet", "--no-video" });
                    CreatePlayer();
                }
                catch (Exception)
                {
                }
            }
            if (player == null && libVLC != null)
            {
                try
                {
                    player = new MediaPlayer(libVLC);
                    player.EnableHardwareDecoding = true;
                    player.Mute = MusicPlayerProperties.Mute;
                    player.Volume = MusicPlayerProperties.Volume;
                    player.EncounteredError += (s, e) =>
                    {
                        Error = "An Error Has Occured";
                        UpdatePage();
                    };
                    player.Muted += (s, e) =>
                    {
                        if (player != null)
                        {
                            MusicPlayerProperties.SetMute(player.Mute);
                            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
                        }
                    };
                    player.VolumeChanged += (s, e) =>
                    {
                        if (player != null)
                        {
                            MusicPlayerProperties.SetVolume(player.Volume);
                            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
                        }
                    };
                    player.Opening += (s, e) =>
                    {
                        Error = null;
                        Opening = true;
                        UpdatePage();
                    };
                    player.Playing += (s, e) =>
                    {
                        IsPlaying = true;
                        if (Opening)
                        {
                            Opening = false;
                            if (Resume)
                            {
                                ThreadPool.QueueUserWorkItem(_ =>
                                {
                                    if (player != null)
                                    {
                                        player.Pause();
                                    }
                                });
                            }
                        }
                        if (MusicPlayerProperties.PauseOtherMedia)
                        {
                            SendActive();
                        }
                        UpdatePage();
                    };
                    player.Paused += (s, e) =>
                    {
                        if (MusicPlayerProperties.PauseOtherMedia)
                        {
                            SendInactive();
                        }
                        UpdatePage();
                    };
                    player.EndReached += (s, e) =>
                    {
                        PlayNextTrack(true);
                    };
                }
                catch(Exception)
                {
                }
            }
        }

        public override void Dispose()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (Media != null)
                {
                    Media.Dispose();
                    Media = null;
                }
                Stop();
                if (libVLC != null)
                {
                    libVLC.Dispose();
                    libVLC = null;
                }
            });
            if (Library != null)
            {
                Library.Dispose();
            }
            base.Dispose();
        }

        public void BaseExecuteSoftButton(SoftButtons softButton)
        {
            base.ExecuteSoftButton(softButton);
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        MusicPlayerProperties.Mute = !MusicPlayerProperties.Mute;
                        break;
                    case SoftButtons.Button2:
                        if (!IsLoading)
                        {
                            if (player != null)
                            {
                                if (player.Media != null)
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
                                                IsPlaying = false;
                                                player.Pause();
                                            }
                                            else
                                            {
                                                IsPlaying = true;
                                                player.Play();
                                            }
                                        }
                                        UpdatePage();
                                    });
                                }
                                else if (CurrentSong != null)
                                {
                                    ThreadPool.QueueUserWorkItem(_ =>
                                    {
                                        Play(CurrentSong, true);
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
                        }
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
                            if (!IsLoading && Library != null)
                            {
                                CurrentPage = MusicPlayerPage.Library;
                                LibrarySong = CurrentSong;
                                LibraryAlbum = CurrentAlbum;
                                LibraryArtist = CurrentArtist;
                                LibraryPage = CurrentSong != null ? MusicLibraryPage.Songs : MusicLibraryPage.Artists;
                            }
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
                            PlayPreviousTrack();
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
                            PlayNextTrack();
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
                                if (Library != null && Library.ArtistCount > 0)
                                {
                                    if (LibraryArtist != null)
                                    {
                                        int index = Library.Artists.ToList().IndexOf(LibraryArtist);
                                        if (index != -1)
                                        {
                                            LibraryArtist = Library.Artists.ToList()[Math.Max(0, index - 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min((LibraryPageNumber + 1) * 5, Math.Max(Library.ArtistCount - 1, 0));
                                            LibraryArtist = index < Library.ArtistCount ? Library.Artists.ToList()[index] : Library.FirstArtist;
                                        }
                                    }
                                    else
                                    {
                                        LibraryArtist = Library.FirstArtist;
                                    }
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryArtist != null && LibraryArtist.AlbumCount > 0)
                                {
                                    if (LibraryAlbum != null)
                                    {
                                        int index = LibraryArtist.Albums.ToList().IndexOf(LibraryAlbum);
                                        if (index != -1)
                                        {
                                            LibraryAlbum = LibraryArtist.Albums.ToList()[Math.Max(0, index - 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min((LibraryPageNumber + 1) * 5, Math.Max(LibraryArtist.AlbumCount - 1, 0));
                                            LibraryAlbum = index < LibraryArtist.AlbumCount ? LibraryArtist.Albums.ToList()[index] : LibraryArtist.FirstAlbum;
                                        }
                                    }
                                    else
                                    {
                                        LibraryAlbum = LibraryArtist.FirstAlbum;
                                    }
                                }
                                break;
                            default:
                                if (LibraryAlbum != null && LibraryAlbum.SongCount > 0)
                                {
                                    if (LibrarySong != null)
                                    {
                                        int index = LibraryAlbum.Songs.ToList().IndexOf(LibrarySong);
                                        if (index != -1)
                                        {
                                            LibrarySong = LibraryAlbum.Songs.ToList()[Math.Max(0, index - 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min((LibraryPageNumber + 1) * 5, Math.Max(LibraryAlbum.SongCount - 1, 0));
                                            LibrarySong = index < LibraryAlbum.SongCount ? LibraryAlbum.Songs.ToList()[index] : LibraryAlbum.FirstSong;
                                        }
                                    }
                                    else
                                    {
                                        LibrarySong = LibraryAlbum.FirstSong;
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
                                if (Library != null && Library.ArtistCount > 0)
                                {
                                    if (LibraryArtist != null)
                                    {
                                        int index = Library.Artists.ToList().IndexOf(LibraryArtist);
                                        if (index != -1)
                                        {
                                            LibraryArtist = Library.Artists.ToList()[Math.Min(Library.ArtistCount - 1, index + 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min(LibraryPageNumber * 5, Math.Max(Library.ArtistCount - 1, 0));
                                            LibraryArtist = index < Library.ArtistCount ? Library.Artists.ToList()[index] : Library.LastArtist;
                                        }
                                    }
                                    else
                                    {
                                        LibraryArtist = Library.LastArtist;
                                    }
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryArtist != null && LibraryArtist.AlbumCount > 0)
                                {
                                    if (LibraryAlbum != null)
                                    {
                                        int index = LibraryArtist.Albums.ToList().IndexOf(LibraryAlbum);
                                        if (index != -1)
                                        {
                                            LibraryAlbum = LibraryArtist.Albums.ToList()[Math.Min(LibraryArtist.AlbumCount - 1, index + 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min(LibraryPageNumber * 5, Math.Max(LibraryArtist.AlbumCount - 1, 0));
                                            LibraryAlbum = index < LibraryArtist.AlbumCount ? LibraryArtist.Albums.ToList()[index] : LibraryArtist.LastAlbum;
                                        }
                                    }
                                    else
                                    {
                                        LibraryAlbum = LibraryArtist.LastAlbum;
                                    }
                                }
                                break;
                            default:
                                if (LibraryAlbum != null && LibraryAlbum.SongCount > 0)
                                {
                                    if (LibrarySong != null)
                                    {
                                        int index = LibraryAlbum.Songs.ToList().IndexOf(LibrarySong);
                                        if (index != -1)
                                        {
                                            LibrarySong = LibraryAlbum.Songs.ToList()[Math.Min(LibraryAlbum.SongCount - 1, index + 1)];
                                        }
                                        else
                                        {
                                            index = Math.Min(LibraryPageNumber * 5, Math.Max(LibraryAlbum.SongCount - 1, 0));
                                            LibrarySong = index < LibraryAlbum.SongCount ? LibraryAlbum.Songs.ToList()[index] : LibraryAlbum.LastSong;
                                        }
                                    }
                                    else
                                    {
                                        LibrarySong = LibraryAlbum.LastSong;
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
                                if (LibraryArtist != null && LibraryArtist.AlbumCount > 0)
                                {
                                    LibraryPage = MusicLibraryPage.Albums;
                                    LibraryAlbum = LibraryArtist.Albums.First();
                                }
                                break;
                            case MusicLibraryPage.Albums:
                                if (LibraryAlbum != null && LibraryAlbum.SongCount > 0)
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
                        CurrentPage = MusicPlayerPage.Player;
                        CreatePlaylist();
                        UpdatePage();
                        if (LibrarySong != null)
                        {
                            PlayFromAlbum(LibrarySong);
                        }
                        else if (LibraryAlbum != null)
                        {
                            PlayFromArtist(LibraryAlbum);
                        }
                        else if (LibraryArtist != null)
                        {
                            PlayFromLibrary(LibraryArtist);
                        }
                        else
                        {
                            PlayFromLibrary(null);
                        }
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
                        return (player != null && Library != null && CurrentSong != null && !IsLoading);
                    case SoftButtons.Button3:
                        return (Library != null);
                    case SoftButtons.Button4:
                        return (Library != null);
                    case SoftButtons.Button6:
                        return (!IsLoading && Library != null && Library.SongCount > 0);
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
                        return (Library != null && (LibrarySong != null || LibraryArtist != null || LibraryAlbum != null));
                    case SoftButtons.Button4:
                        return true;
                }

            }
            return base.IsLEDOn(softButton);
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
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
                default:
                    return true;
            }
        }

        private int GetSongCount()
        {
            return CountFolder(MusicPlayerProperties.Path);
        }

        internal virtual void LoadLibrary(bool isResuming)
        {
            if (Library == null)
            {
                IsLoading = true;
                UpdatePage();
                Library = new FIPMusicLibrary();
                if (!string.IsNullOrEmpty(MusicPlayerProperties.Path))
                {
                    int songCount = GetSongCount();
                    int itemCount = 0;
                    LoadFolder(MusicPlayerProperties.Path, ref itemCount, songCount);
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
                                album.Sort();
                            }
                        }
                        if (IsLoading)
                        {
                            artist.Sort();
                        }
                    }
                    if (IsLoading)
                    {
                        Library.Sort();
                    }
                    if (IsLoading)
                    {
                        PlayFirstSong(true, isResuming);
                    }
                }
                IsLoading = false;
                UpdatePage();
            }
        }

        private FIPMusicSong LoadSongFromFile(string filename, M3UMedia media = null, string playlist = null)
        {
            FIPMusicSong song;
            try
            {
                if (File.Exists(filename))
                {
                    using (var tag = TagLib.File.Create(filename))
                    {
                        if (tag.Tag != null)
                        {
                            song = new FIPMusicSong(string.IsNullOrEmpty(tag.Tag.FirstAlbumArtist) ? (string.IsNullOrEmpty(tag.Tag.FirstPerformer) ? "Unknown Artist" : tag.Tag.FirstPerformer) : tag.Tag.FirstAlbumArtist, string.IsNullOrEmpty(tag.Tag.Album) ? "Unknown Album" : tag.Tag.Album)
                            {
                                Duration = tag.Properties.Duration,
                                Filename = filename,
                                Title = string.IsNullOrEmpty(tag.Tag.Title) ? "Unknown Title" : tag.Tag.Title,
                                Year = tag.Tag.Year,
                                Genre = string.IsNullOrEmpty(tag.Tag.FirstGenre) ? "Unknown Genre" : tag.Tag.FirstGenre,
                                Track = tag.Tag.Track,
                                ListenerLocation = ListenerLocation
                            };
                        }
                        else
                        {
                            song = new FIPMusicSong("Unknown Artist", "Unknown Album")
                            {
                                Duration = tag.Properties != null ? tag.Properties.Duration : new TimeSpan(),
                                Filename = filename,
                                Title = HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(filename)),
                                Genre = "Unknown Genre",
                                ListenerLocation = ListenerLocation
                            };
                        }
                    }
                }
                else
                {
                    song = new FIPMusicSong(filename.IsStream() ? "Stream" : "Unknown Artist", !string.IsNullOrEmpty(playlist) ? playlist : "Unknown Album")
                    {
                        Duration = media != null && media.Duration.HasValue ? media.Duration.Value : (media != null && media.Attributes != null && media.Attributes.TvgDuration.HasValue ? media.Attributes.TvgDuration.Value : new TimeSpan()),
                        Filename = filename,
                        Title = media != null && !string.IsNullOrEmpty(media.Title) ? media.Title : (media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgName) ? media.Attributes.TvgName : (media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgId) ? media.Attributes.TvgId : HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(filename)))),
                        Genre = media != null && media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgType) ? media.Attributes.TvgType : "Unknown Genre",
                        ListenerLocation = ListenerLocation
                    };
                    if (media != null)
                    {
                        song.M3UMedia = media;
                    }
                    if (media != null && media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgLogo))
                    {
                        song.LogoUrl = media.Attributes.TvgLogo;
                    }
                }
                song.OnArtworkDownloaded += Song_OnArtworkDownloaded;
            }
            catch (Exception)
            {
                song = new FIPMusicSong(filename.IsStream() ? "Stream" : "Unknown Artist", !string.IsNullOrEmpty(playlist) ? playlist : "Unknown Album")
                {
                    Duration = media != null && media.Duration.HasValue ? media.Duration.Value : (media != null && media.Attributes != null && media.Attributes.TvgDuration.HasValue ? media.Attributes.TvgDuration.Value : new TimeSpan()),
                    Filename = filename,
                    Title = media != null && !string.IsNullOrEmpty(media.Title) ? media.Title : (media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgName) ? media.Attributes.TvgName : (media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgId) ? media.Attributes.TvgId : HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(filename)))),
                    Genre = media != null && media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgType) ? media.Attributes.TvgType : "Unknown Genre",
                    ListenerLocation = ListenerLocation
                };
                if (media != null)
                {
                    song.M3UMedia = media;
                }
                if (media != null && media.Attributes != null && !string.IsNullOrEmpty(media.Attributes.TvgLogo))
                {
                    song.LogoUrl = media.Attributes.TvgLogo;
                }
                song.OnArtworkDownloaded += Song_OnArtworkDownloaded;
            }
            return song;
        }

        private void Song_OnArtworkDownloaded(object sender, EventArgs e)
        {
            UpdatePage();
        }

        private int CountFolder(string path)
        {
            int count = 0;
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".wma", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m3u", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".m3u8", StringComparison.OrdinalIgnoreCase));
            var folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                if (!IsLoading)
                {
                    break;
                }
                count += CountFolder(folder);
            }
            foreach (var file in files)
            {
                if (!IsLoading)
                {
                    break;
                }
                if (Path.GetExtension(file).Equals(".m3u", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(file).Equals(".m3u8", StringComparison.OrdinalIgnoreCase))
                {
                    M3UMediaContainer playlist = M3U.M3UParser.ParseFromFile(file);
                    count += playlist.Medias.Count;
                }
                else
                {
                    count++;
                }
            }
            return count;
        }

        private void LoadFolder(string path, ref int itemCount, int songCount)
        {
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.IsAudio() || s.IsPlaylist());
            var folders = Directory.GetDirectories(path);
            foreach(string folder in folders)
            {
                if (!IsLoading)
                {
                    break;
                }
                LoadFolder(folder, ref itemCount, songCount);
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
                    if (Path.GetExtension(file).Equals(".m3u", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(file).Equals(".m3u8", StringComparison.OrdinalIgnoreCase))
                    {
                        M3UMediaContainer playlist = M3U.M3UParser.ParseFromFile(file);
                        foreach(M3UMedia media in playlist.Medias)
                        {
                            song = LoadSongFromFile(media.Path, media, HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(file)));
                            song.Playlist = HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(file));
                            UpdateLoading(itemCount, songCount);
                            FIPMusicAlbum album = Library.GetAlbum("Playlists", song.Playlist, MusicPlayerProperties.RadioDistance);
                            album.IsPlaylist = true;
                            album.AddSong(song);
                            itemCount++;
                        }
                    }
                    else
                    {
                        song = LoadSongFromFile(file);
                        UpdateLoading(itemCount, songCount);
                        FIPMusicAlbum album = Library.GetAlbum(song.Artist, song.Album, MusicPlayerProperties.RadioDistance);
                        album.AddSong(song);
                        itemCount++;
                    }
                }
            }
        }

        private FIPMusicAlbum FindAlbum(string artistName, string albumName)
        {
            FIPMusicArtist artist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(artistName, StringComparison.OrdinalIgnoreCase));
            if (artist == null)
            {
                return null;
            }
            return artist.Albums.FirstOrDefault(a => a.AlbumName.Equals(albumName, StringComparison.OrdinalIgnoreCase));
        }

        public List<int> GetUniqueRandoms(int count)
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
        internal virtual void CreatePlaylist()
        {
            Playlist = new List<FIPMusicSong>();
            if (LibraryAlbum != null)
            {
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Album;
                Playlist.AddRange(LibraryAlbum.Songs);
            }
            else if (LibraryArtist != null)
            {
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Artist;
                Playlist.AddRange(LibraryArtist.Songs);
            }
            else
            {
                MusicPlayerProperties.PlaylistType = MusicPlaylistType.Library;
                Playlist.AddRange(Library.Songs);
            }
            RandomList = GetUniqueRandoms(Playlist.Count);
        }

        public virtual void ExternalPause()
        {
            if (Player != null && (Player.IsPlaying || IsLoading || Opening) && !Resume)
            {
                Resume = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (Player != null && (Player.IsPlaying || IsLoading || Opening))
                    {
                        if (!IsLoading && !Opening)
                        {
                            player.Pause();
                        }
                    }
                    UpdatePage();
                });
            }
        }

        public virtual void ExternalResume()
        {
            if (Player != null && !Player.IsPlaying && !Opening && CanPlay() && Library != null && !IsLoading && Resume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (Player != null && !Player.IsPlaying && !Opening && CanPlay() && Library != null && !IsLoading)
                    {
                        Opening = true;
                        if (Media == null)
                        {
                            PlayFirstSong(true, MusicPlayerProperties.Resume);
                        }
                        else
                        {
                            Player.Play();
                        }
                    }
                    UpdatePage();
                });
            }
            Resume = false;
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

        public virtual void PlayFromAlbum(FIPMusicSong song)
        {
            PlaylistIndex = Playlist.IndexOf(song);
            Play(song, true);
        }

        public virtual void PlayFromArtist(FIPMusicAlbum libraryAlbum)
        {
            FIPMusicSong startSong = libraryAlbum.FirstSong;
            LibraryArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(LibraryAlbum.IsPlaylist ? "Playlists" : LibraryAlbum.Artist, StringComparison.OrdinalIgnoreCase));
            LibraryAlbum = null;
            CreatePlaylist();
            if (MusicPlayerProperties.Shuffle)
            {
                int num = RandomList.FirstOrDefault(index => Playlist[index].Album.Equals(libraryAlbum.AlbumName, StringComparison.OrdinalIgnoreCase));
                startSong = Playlist[num];
            }
            PlaylistIndex = Playlist.IndexOf(startSong);
            Play(startSong, true);
        }

        public virtual void PlayFromLibrary(FIPMusicArtist libraryArtist)
        {
            FIPMusicSong startSong = libraryArtist != null ? libraryArtist.FirstSong : Library.FirstSong;
            LibraryArtist = null;
            CreatePlaylist();
            if (MusicPlayerProperties.Shuffle)
            {
                Random random = new Random();
                if (libraryArtist != null)
                {
                    int num = RandomList.FirstOrDefault(index => Playlist[index].Artist.Equals(libraryArtist.ArtistName, StringComparison.OrdinalIgnoreCase));
                    startSong = Playlist[num];
                }
                else
                {
                    startSong = Playlist[RandomList.First()];
                }
            }
            PlaylistIndex = Playlist.IndexOf(startSong);
            Play(startSong, true);
        }

        public virtual void PlayFirstSong(bool firstPlay, bool isResuming = false)
        {
            if (Library != null)
            {
                if (isResuming && MusicPlayerProperties.Resume)
                {
                    LibrarySong = FindSong(MusicPlayerProperties.LastSong);
                    if (LibrarySong != null)
                    {
                        if (MusicPlayerProperties.PlaylistType == MusicPlaylistType.Album)
                        {
                            LibraryAlbum = FindAlbum(!string.IsNullOrEmpty(LibrarySong.Playlist) ? "Playlists" : LibrarySong.Artist, !string.IsNullOrEmpty(LibrarySong.Playlist) ? LibrarySong.Playlist : LibrarySong.Album);
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
                        PlaylistIndex = Playlist.IndexOf(CurrentSong);
                    }
                    else
                    {
                        PlaylistIndex = 0;
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
                    CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                    CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    UpdatePage();
                    if (CanPlay())
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong, !Resume && (IsPlaying || ((firstPlay && MusicPlayerProperties.AutoPlay) || !firstPlay) && CanPlay()));
                        });
                    }
                    else
                    {
                        SongChanged();
                    }
                }
            }
        }

        public void PlayNextTrack(bool endReached = false)
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
                        PlaylistIndex = 0;
                        if (MusicPlayerProperties.Shuffle)
                        {
                            CurrentSong = Playlist[PlaylistIndex];
                        }
                        else
                        {
                            CurrentSong = Playlist.First();
                        }
                        CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        int index = PlaylistIndex;
                        if (MusicPlayerProperties.Repeat == MusicRepeatState.Off || MusicPlayerProperties.Repeat == MusicRepeatState.Context)
                        {
                            if (MusicPlayerProperties.Shuffle)
                            {
                                index = Math.Min(index, RandomList.Count - 1);
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
                                PlaylistIndex = RandomList[index];
                                CurrentSong = Playlist[PlaylistIndex];
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
                                PlaylistIndex = Math.Min(index, Playlist.Count - 1);
                                CurrentSong = Playlist[PlaylistIndex];
                            }
                            CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    UpdatePage();
                    if (CanPlay())
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong, !Resume && (IsPlaying || endReached) && CanPlay());
                        });
                    }
                    else
                    {
                        SongChanged();
                    }
                }
            }
        }

        public void PlayPreviousTrack()
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
                        CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                        CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        int index = PlaylistIndex;
                        if (MusicPlayerProperties.Repeat == MusicRepeatState.Off || MusicPlayerProperties.Repeat == MusicRepeatState.Context)
                        {
                            if (MusicPlayerProperties.Shuffle)
                            {
                                index = Math.Min(index, RandomList.Count - 1);
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
                                PlaylistIndex = RandomList[index];
                                CurrentSong = Playlist[PlaylistIndex];
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
                                PlaylistIndex = Math.Min(index, Playlist.Count - 1);
                                CurrentSong = Playlist[PlaylistIndex];
                            }
                            CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                            CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    UpdatePage();
                    if (CanPlay())
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong, !Resume && (IsPlaying || MusicPlayerProperties.AutoPlay) && CanPlay());
                        });
                    }
                    else
                    {
                        SongChanged();
                    }
                }
            }
        }

        public virtual void Play(FIPMusicSong song, bool play = true)
        {
            if (libVLC != null)
            {
                Stop();
                while (!_stopped)
                {
                    Thread.Sleep(100);
                }
                if (song != CurrentSong)
                {
                    CurrentSong = song;
                    CurrentAlbum = FindAlbum(!string.IsNullOrEmpty(CurrentSong.Playlist) ? "Playlists" : CurrentSong.Artist, !string.IsNullOrEmpty(CurrentSong.Playlist) ? CurrentSong.Playlist : CurrentSong.Album);
                    CurrentArtist = Library.Artists.FirstOrDefault(a => a.ArtistName.Equals(CurrentAlbum.IsPlaylist ? "Playlists" : CurrentSong.Artist, StringComparison.OrdinalIgnoreCase));
                    SongChanged();
                }
                if (Media != null)
                {
                    Media.Dispose();
                    Media = null;
                }
                if (song != null && !string.IsNullOrEmpty(song.Filename))
                {
                    CreatePlayer();
                    UpdatePlayer();
                    MusicPlayerProperties.LastSong = song.Filename;
                    if (song.IsStream)
                    {
                        Media = new Media(libVLC, song.Filename, FromType.FromLocation);
                    }
                    else if (File.Exists(song.Filename))
                    {
                        Media = new Media(libVLC, song.Filename);
                    }
                    if (Media != null)
                    {
                        try
                        {
                            Media.MetaChanged += Media_MetaChanged;
                            if (!Resume)
                            {
                                if (player != null && play)
                                {
                                    IsPlaying = true;
                                    player.Play(Media);
                                }
                            }
                        }
                        catch (Exception)
                        {
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
            else
            {
                Error = "LibVLC failed to initialize.";
                UpdatePage();
            }
        }

        public virtual void Media_MetaChanged(object sender, MediaMetaChangedEventArgs e)
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
                    if (isLocal && File.Exists(meta))
                    {
                        if (CurrentSong.MetaData == null)
                        {
                            CurrentSong.MetaData = new FIPMetaData();
                        }
                        CurrentSong.MetaData.Artwork = new Bitmap(meta);
                    }
                    else if (File.Exists(CurrentSong.Filename))
                    {
                        try
                        {
                            using (var tag = TagLib.File.Create(CurrentSong.Filename))
                            {
                                if (tag.Tag != null)
                                {
                                    if (tag.Tag.Pictures.Length > 0)
                                    {
                                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data)))
                                        {
                                            if (CurrentSong.MetaData == null)
                                            {
                                                CurrentSong.MetaData = new FIPMetaData();
                                            }
                                            CurrentSong.MetaData.Artwork = new Bitmap(img);
                                        }
                                        UpdatePage();
                                    }
                                    else
                                    {
                                        if (CurrentSong.MetaData == null)
                                        {
                                            CurrentSong.MetaData = new FIPMetaData();
                                        }
                                        if (CurrentSong.Artwork != null)
                                        {
                                            CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                                        }
                                        UpdatePage();
                                    }
                                }
                                else
                                {
                                    if (CurrentSong.MetaData == null)
                                    {
                                        CurrentSong.MetaData = new FIPMetaData();
                                    }
                                    if (CurrentSong.Artwork != null)
                                    {
                                        CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                                    }
                                    UpdatePage();
                                }
                            }
                        }
                        catch (Exception)
                        {
                            if (CurrentSong.MetaData == null)
                            {
                                CurrentSong.MetaData = new FIPMetaData();
                            }
                            if (CurrentSong.Artwork != null)
                            {
                                CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                            }
                            UpdatePage();
                        }
                    }
                    else if (CurrentSong.IsStream)
                    {
                        if (CurrentSong.MetaData == null)
                        {
                            CurrentSong.MetaData = new FIPMetaData();
                        }
                        if (CurrentSong.Artwork != null)
                        {
                            CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                        }
                        UpdatePage();
                    }
                }
                else if (e.MetadataType == MetadataType.NowPlaying)
                {
                    if (CurrentSong.MetaData == null)
                    {
                        CurrentSong.MetaData = new FIPMetaData();
                    }
                    if (CurrentSong.IsStream)
                    {
                        if (CurrentSong.Title.Equals(HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(CurrentSong.Filename))))
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
                        if (CurrentSong.Artwork != null)
                        {
                            CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                        }
                    }
                    else
                    {
                        CurrentSong.MetaData.Title = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Title)) || !string.IsNullOrEmpty(CurrentSong.Title) ? CurrentSong.Title : player.Media.Meta(MetadataType.Title);
                        CurrentSong.MetaData.Artist = string.IsNullOrEmpty(player.Media.Meta(MetadataType.AlbumArtist)) || !string.IsNullOrEmpty(CurrentSong.Artist) ? (string.IsNullOrEmpty(player.Media.Meta(MetadataType.Artist)) || !string.IsNullOrEmpty(CurrentSong.Artist) ? CurrentSong.Artist : player.Media.Meta(MetadataType.Artist)) : player.Media.Meta(MetadataType.AlbumArtist);
                    }
                    CurrentSong.MetaData.Album = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Album)) || !string.IsNullOrEmpty(CurrentSong.Album) ? CurrentSong.Album : player.Media.Meta(MetadataType.Album);
                    CurrentSong.MetaData.Genre = string.IsNullOrEmpty(player.Media.Meta(MetadataType.Genre)) || !string.IsNullOrEmpty(CurrentSong.Genre) ? CurrentSong.Genre : player.Media.Meta(MetadataType.Genre);
                    CurrentSong.MetaData.Track = Convert.ToUInt32(string.IsNullOrEmpty(player.Media.Meta(MetadataType.TrackNumber)) || CurrentSong.Track != 0 ? CurrentSong.Track.ToString() : player.Media.Meta(MetadataType.TrackNumber));
                    UpdatePage();
                }
            }
        }

        private bool _stopped = false;
        public void Stop()
        {
            _stopped = false;
            MediaPlayer oldPlayer = player;
            player = null;
            Error = null;
            Task.Run(() =>
            {
                if (oldPlayer != null)
                {
                    oldPlayer.Stop();
                    oldPlayer.Dispose();
                    oldPlayer = null;
                }
                _stopped = true;
            });
        }

        public virtual void UpdateLoading(int current, int max)
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
                                int titleHeight = (int)graphics.MeasureString("Loading", MusicPlayerProperties.Font, 288, format).Height;
                                Rectangle progressBarBorder = new Rectangle(10, (120 - ((titleHeight + 5 + 40) / 2)) + titleHeight + 5, 300, 40);
                                Rectangle progressBar = new Rectangle(progressBarBorder.Left + 2, progressBarBorder.Top + 2, current * 296 / max, 36);
                                graphics.DrawString("Loading", MusicPlayerProperties.Font, brush, new RectangleF(0, (120 - ((titleHeight + 40) / 2)) - 5, 320, titleHeight), format);
                                using (Pen pen = new Pen(brush))
                                {
                                    graphics.DrawRectangle(pen, progressBarBorder);
                                }
                                graphics.FillRectangle(brush, progressBar);
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

        public virtual void UpdatePlayer()
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
                            else if (Library != null && Library.SongCount == 0)
                            {
                                graphics.DrawString(EmptySongCountMessage, MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            else if (CurrentSong != null)
                            {
                                Color color = CurrentSong.M3UMedia != null && CurrentSong.M3UMedia.Adornments != null && CurrentSong.M3UMedia.Adornments.Color.HasValue ? CurrentSong.M3UMedia.Adornments.Color.Value : MusicPlayerProperties.FontColor;
                                Bitmap artwork = CurrentAlbum.IsPlaylist && CurrentSong.MetaData != null && CurrentSong.MetaData.Artwork != null ? CurrentSong.MetaData.Artwork : CurrentSong.Artwork;
                                bool dispose = false;
                                if (artwork == null && CurrentSong.IsStream)
                                {
                                    artwork = FIPToolKit.Properties.Resources.Radio.ChangeToColor(color);
                                    dispose = true;
                                }
                                else if (artwork == null && CurrentSong.IsVideo)
                                {
                                    artwork = FIPToolKit.Properties.Resources.Video.ChangeToColor(color);
                                    dispose = true;
                                }
                                else if (artwork == null)
                                {
                                    artwork = FIPToolKit.Properties.Resources.Music.ChangeToColor(color);
                                    dispose = true;
                                }
                                int titleHeight = (int)graphics.MeasureString(CurrentAlbum.IsPlaylist && CurrentSong.MetaData != null ? CurrentSong.MetaData.Title : CurrentSong.Title, MusicPlayerProperties.Font, 288, format).Height;
                                int artistHeight = (int)graphics.MeasureString(!string.IsNullOrEmpty(Error) ? Error : CurrentAlbum.IsPlaylist && CurrentSong.MetaData != null ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, 288, format).Height;
                                int maxImageWidth = 320 - 34;
                                int maxImageHeight = 240 - (titleHeight + artistHeight);
                                //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                                double ratioX = (double)maxImageWidth / artwork.Width;
                                double ratioY = (double)maxImageHeight / artwork.Height;
                                double ratio = Math.Min(ratioX, ratioY);
                                int imageWidth = (int)(artwork.Width * ratio);
                                int imageHeight = (int)(artwork.Height * ratio);
                                Rectangle destRect = new Rectangle(17 + ((320 - imageWidth) / 2), (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                                graphics.DrawImage(artwork, destRect, 0, 0, artwork.Width, artwork.Height, GraphicsUnit.Pixel);
                                using (SolidBrush brush2 = new SolidBrush(color))
                                {
                                    using (FontEx font = new FontEx(MusicPlayerProperties.Font, CurrentSong.M3UMedia != null && CurrentSong.M3UMedia.Adornments != null && CurrentSong.M3UMedia.Adornments.FontStyle.HasValue ? CurrentSong.M3UMedia.Adornments.FontStyle.Value : MusicPlayerProperties.Font.Style))
                                    {
                                        graphics.DrawString(CurrentAlbum.IsPlaylist && CurrentSong.MetaData != null ? CurrentSong.MetaData.Title : CurrentSong.Title, font, brush2, new RectangleF(32, maxImageHeight, 288, titleHeight), format);
                                    }
                                }
                                graphics.DrawString(CurrentAlbum.IsPlaylist && CurrentSong.MetaData != null ? CurrentSong.MetaData.Artist : CurrentSong.Artist, MusicPlayerProperties.ArtistFont, brush, new RectangleF(32, maxImageHeight + titleHeight, 288, artistHeight), format);
                                if (dispose)
                                {
                                    artwork.Dispose();
                                }
                            }
                            else if (Playlist == null || Playlist.Count == 0)
                            {
                                graphics.DrawString(EmptyPlaylistMessage, MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            else
                            {
                                graphics.DrawString(EmptySongMessage, MusicPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                            }
                            DrawButtons(graphics);
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

        protected override void DrawButtons(Graphics g)
        {
            if (player != null)
            {
                g.AddButtonIcon(MusicPlayerProperties.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, MusicPlayerProperties.Mute ? Color.Red : Color.Green, true, SoftButtons.Button1);
            }
            if (player != null)
            {
                g.AddButtonIcon(player.State == VLCState.Playing ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, player.State == VLCState.Playing ? Color.Yellow : Color.Blue, true, SoftButtons.Button2);
            }
            g.AddButtonIcon(FIPToolKit.Properties.Resources.shuffle, MusicPlayerProperties.Shuffle == true ? Color.Green : Color.White, true, SoftButtons.Button3);
            g.AddButtonIcon(MusicPlayerProperties.Repeat == MusicRepeatState.Track ? FIPToolKit.Properties.Resources.repeat_one : FIPToolKit.Properties.Resources.repeat, MusicPlayerProperties.Repeat != MusicRepeatState.Off ? Color.Green : Color.White, true, SoftButtons.Button4);
            if (!IsLoading && Library != null && Library.SongCount > 0)
            { 
                g.AddButtonIcon(FIPToolKit.Properties.Resources.playlist, Color.Orange, true, SoftButtons.Button6);
            }
            base.DrawButtons(g);
        }

        internal virtual void UpdatePlayList()
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
                            Rectangle destRect;
                            switch (LibraryPage)
                            {
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum == null)
                                    {
                                        LibraryPage = MusicLibraryPage.Artists;
                                    }
                                    break;
                                case MusicLibraryPage.Songs:
                                    if (LibraryArtist == null)
                                    {
                                        LibraryPage = MusicLibraryPage.Albums;
                                        if (LibraryAlbum == null)
                                        {
                                            LibraryPage = MusicLibraryPage.Artists;
                                        }
                                    }
                                    break;
                            }
                            switch (LibraryPage)
                            {
                                case MusicLibraryPage.Artists:
                                    if (LibraryArtist != null)
                                    {
                                        int index = Library.Artists.ToList().IndexOf(LibraryArtist);
                                        LibraryPageNumber = index != -1 ? Math.Max(0, index) / 5 : Math.Min(LibraryPageNumber, (Library.ArtistCount - 1) / 5);
                                    }
                                    destRect = new Rectangle(0, 0, 320, 40);
                                    graphics.DrawString("Artists", MusicPlayerProperties.Font, brush, destRect, format);
                                    break;
                                case MusicLibraryPage.Albums:
                                    if (LibraryAlbum != null)
                                    {
                                        int index = LibraryArtist.Albums.ToList().IndexOf(LibraryAlbum);
                                        LibraryPageNumber = index != -1 ? Math.Max(0, index) / 5 : Math.Min(LibraryPageNumber, (LibraryArtist.AlbumCount - 1) / 5);
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
                                        int index = LibraryAlbum.Songs.ToList().IndexOf(LibrarySong);
                                        LibraryPageNumber = index != -1 ? Math.Max(0, index) / 5 : Math.Min(LibraryPageNumber, (LibraryAlbum.SongCount - 1) / 5);
                                    }
                                    if (LibraryAlbum != null)
                                    {
                                        destRect = new Rectangle(0, 0, 320, 40);
                                        graphics.DrawString(LibraryAlbum.AlbumName, MusicPlayerProperties.Font, brush, destRect, format);
                                    }
                                    break;
                            }
                            format.Alignment = StringAlignment.Near;
                            for (int i = 0; i < 5; i++)
                            {
                                int count = Library.ArtistCount;
                                switch (LibraryPage)
                                {
                                    case MusicLibraryPage.Albums:
                                        count = LibraryArtist.AlbumCount;
                                        break;
                                    case MusicLibraryPage.Songs:
                                        count = LibraryAlbum.SongCount;
                                        break;
                                }
                                if (i + (LibraryPageNumber * 5) >= count)
                                {
                                    break;
                                }
                                switch(LibraryPage)
                                {
                                    case MusicLibraryPage.Artists:
                                        FIPMusicArtist artist = Library.Artists.ToList()[i + (LibraryPageNumber * 5)];
                                        if (artist == LibraryArtist)
                                        {
                                            graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        if (artist.ArtistName.Equals("Playlists"))
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.playlist.ChangeToColor(artist == LibraryArtist ? Color.White : MusicPlayerProperties.FontColor.Color), destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (artist.Artwork != null)
                                        {
                                            graphics.DrawImage(artist.Artwork, destRect, 0, 0, artist.Artwork.Width, artist.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.Music.ChangeToColor(artist == LibraryArtist ? Color.White : MusicPlayerProperties.FontColor.Color), destRect, 0, 0, FIPToolKit.Properties.Resources.Music.Width, FIPToolKit.Properties.Resources.Music.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        using (SolidBrush brush2 = new SolidBrush(artist == LibraryArtist ? SystemColors.HighlightText : MusicPlayerProperties.FontColor.Color))
                                        {
                                            graphics.DrawString(artist.ArtistName, MusicPlayerProperties.Font, brush2, destRect, format);
                                        }
                                        break;
                                    case MusicLibraryPage.Albums:
                                        FIPMusicAlbum album = LibraryArtist.Albums.ToList()[i + (LibraryPageNumber * 5)];
                                        if (album == LibraryAlbum)
                                        {
                                            graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        if (album.IsPlaylist)
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.playlist.ChangeToColor(album == LibraryAlbum ? Color.White : MusicPlayerProperties.FontColor.Color), destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (album.Artwork != null)
                                        {
                                            graphics.DrawImage(album.Artwork, destRect, 0, 0, album.Artwork.Width, album.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.Music.ChangeToColor(album == LibraryAlbum ? Color.White : MusicPlayerProperties.FontColor.Color), destRect, 0, 0, FIPToolKit.Properties.Resources.Music.Width, FIPToolKit.Properties.Resources.Music.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        using (SolidBrush brush2 = new SolidBrush(album == LibraryAlbum ? SystemColors.HighlightText : MusicPlayerProperties.FontColor.Color))
                                        {
                                            graphics.DrawString(album.AlbumName, MusicPlayerProperties.Font, brush2, destRect, format);
                                        }
                                        break;
                                    default:
                                        FIPMusicSong song = LibraryAlbum.Songs.ToList()[i + (LibraryPageNumber * 5)];
                                        if (song == LibrarySong)
                                        {
                                            using (SolidBrush backBrush = new SolidBrush(song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.BackgroundColor.HasValue ? song.M3UMedia.Adornments.BackgroundColor.Value.Color : SystemColors.Highlight))
                                            {
                                                graphics.FillRectangle(backBrush, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                            }
                                        }
                                        destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                        Color color = song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.Color.HasValue ? song.M3UMedia.Adornments.Color.Value : MusicPlayerProperties.FontColor;
                                        if (song.Artwork != null)
                                        {
                                            graphics.DrawImage(song.Artwork, destRect, 0, 0, song.Artwork.Width, song.Artwork.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (song.IsStream)
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.Radio.ChangeToColor(song == LibrarySong ? Color.White : color), destRect, 0, 0, FIPToolKit.Properties.Resources.Radio.Width, FIPToolKit.Properties.Resources.Radio.Height, GraphicsUnit.Pixel);
                                        }
                                        else if (song.IsVideo)
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.Video.ChangeToColor(song == LibrarySong ? Color.White : color), destRect, 0, 0, FIPToolKit.Properties.Resources.Video.Width, FIPToolKit.Properties.Resources.Video.Height, GraphicsUnit.Pixel);
                                        }
                                        else
                                        {
                                            graphics.DrawImage(FIPToolKit.Properties.Resources.Music.ChangeToColor(song == LibrarySong ? Color.White : color), destRect, 0, 0, FIPToolKit.Properties.Resources.Music.Width, FIPToolKit.Properties.Resources.Music.Height, GraphicsUnit.Pixel);
                                        }
                                        destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                        using (SolidBrush brush2 = new SolidBrush(song == LibrarySong ? SystemColors.HighlightText : color))
                                        {
                                            using (FontEx font = new FontEx(MusicPlayerProperties.Font, song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.FontStyle.HasValue ? song.M3UMedia.Adornments.FontStyle.Value : MusicPlayerProperties.Font.Style))
                                            {
                                                graphics.DrawString(song.Title, font, brush2, destRect, format);
                                            }
                                        }
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

        public bool CanPlay()
        {
            FIPCanPlayEventArgs canPlay = new FIPCanPlayEventArgs(this);
            OnCanPlay?.Invoke(this, canPlay);
            if (!canPlay.CanPlay)
            {
                Resume = true;
            }
            return canPlay.CanPlay;
        }

        public void SongChanged()
        {
            OnSongChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        public bool Mute
        {
            get
            {
                return MusicPlayerProperties.Mute;
            }
            set
            {
                MusicPlayerProperties.Mute = value;
            }
        }

        public int Volume
        {
            get
            {
                return MusicPlayerProperties.Volume;
            }
            set
            {
                MusicPlayerProperties.Volume = value;
            }
        }

        public override void Active(bool sendEvent = true)
        {
            base.Active(false);
        }

        public override void Inactive(bool sendEvent = true)
        {
            base.Inactive(false);
        }

        public bool CanPlayOther
        {
            get
            {
                return !(MusicPlayerProperties.PauseOtherMedia && player != null && player.IsPlaying);
            }
        }
    }
}
