using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using LibVLCSharp.Shared;
using Newtonsoft.Json;
using RestSharp;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace FIPToolKit.Models
{
    public abstract class FIPRadioPlayer : FIPMusicPlayer
    {
        public FlightSimProviderBase FlightSimProvider { get; private set; }

        public bool CanPlayFirstSong { get; set; }

        public FIPRadioPlayer(FIPRadioProperties properties, FlightSimProviderBase flightSimProvider) : base(properties)
        {
            FlightSimProvider = flightSimProvider;
            properties.ControlType = GetType().FullName;
            properties.Name = string.Format("{0} Radio Player", flightSimProvider.Name);
            properties.IsDirty = false;
            CanPlayFirstSong = flightSimProvider.IsConnected;
            flightSimProvider.OnFlightDataReceived += FlightSimProvider_OnFlightDataReceived;
            flightSimProvider.OnConnected += FlightSimProvider_OnConnected;
            if (flightSimProvider.IsConnected)
            {
                flightSimProvider.Connected();
            }
        }

        private void FlightSimProvider_OnConnected(FlightSimProviderBase sender)
        {
            CanPlayFirstSong = true;
        }

        private LatLong cachedLocation = null;
        private void FlightSimProvider_OnFlightDataReceived(FlightSimProviderBase sender)
        {
            LatLong location = new LatLong(FlightSimProvider.Latitude, FlightSimProvider.Longitude);
            if (location.IsEmpty())
            {
                ListenerLocation = LocalLocation;
            }
            else
            {
                ListenerLocation = location;
            }
            if (cachedLocation == null || Net.DistanceBetween(location.Latitude.Value, location.Longitude.Value, cachedLocation.Latitude.Value, cachedLocation.Longitude.Value) >= 1)
            {
                cachedLocation = location;
                CreatePlaylist();
            }
        }

        private List<FIPRadioStation> GetStations()
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetAllStations", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<FIPRadioStation>>(response.Content);
                }
            }
            catch (Exception)
            {
                // Offline?
            }
            return null;
        }

        public FIPRadioProperties RadioProperties
        {
            get
            {
                return Properties as FIPRadioProperties;
            }
        }

        public override void ExternalResume()
        {
            if (Player != null && !Player.IsPlaying && !Opening && CanPlay() && Library != null && !IsLoading && Resume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (Player != null && !Player.IsPlaying && !Opening && CanPlay() && Library != null && !IsLoading)
                    {
                        Opening = true;
                        if (CurrentSong != null)
                        {
                            Play(CurrentSong);
                        }
                        else
                        {
                            PlayFirstSong(true, MusicPlayerProperties.Resume);
                        }
                    }
                    UpdatePage();
                });
            }
            Resume = false;
        }

        public override void OnRadioDistanceChanged()
        {
            if (Library != null)
            {
                Library.UpdateRadioDistance(MusicPlayerProperties.RadioDistance);
                if (!IsLoading)
                {
                    LibrarySong = CurrentSong == null ? CurrentPage == MusicPlayerPage.Library ? LibrarySong : Library.Songs.OrderBy(s => s.Distance).FirstOrDefault() : CurrentSong;
                    LibraryArtist = Library.FirstArtist;
                    LibraryAlbum = LibraryArtist.FirstAlbum;
                    CreatePlaylist();
                }
            }
        }

        internal override void LoadLibrary(bool isResuming)
        {
            if (Library == null)
            {
                IsLoading = true;
                UpdatePage();
                Library = new FIPMusicLibrary();
                List<FIPRadioStation> stations = GetStations();
                if (stations != null)
                {
                    foreach (FIPRadioStation station in stations)
                    {
                        FIPMusicSong song = new FIPMusicSong("Playlists", "Stations")
                        {
                            Title = station.Name,
                            Playlist = "Stations",
                            Genre = station.Tags,
                            Filename = station.Url,
                            StreamLocation = new FlightSim.LatLong(station.Geo_Lat, station.Geo_Long),
                            ListenerLocation = ListenerLocation
                        };
                        if (!string.IsNullOrEmpty(station.Favicon))
                        {
                            song.LogoUrl = station.Favicon;
                        }
                        else
                        {
                            song.Artwork = new Bitmap(FIPToolKit.Properties.Resources.Radio.ChangeToColor(SystemColors.Highlight));
                        }
                        song.OnArtworkDownloaded += Song_OnArtworkDownloaded;
                        FIPMusicAlbum album = Library.GetAlbum("Playlists", "Stations", RadioProperties.RadioDistance);
                        album.IsPlaylist = true;
                        album.AddSong(song);
                    }
                }
                if (IsLoading)
                {
                    foreach (FIPMusicArtist artist in Library.Artists)
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
                        IsLoading = false;
                        PlayFirstSong(true, isResuming);
                    }
                }
                IsLoading = false;
                UpdatePage();
            }
        }

        public override void PlayFirstSong(bool firstPlay, bool isResuming = false)
        {
            if (Library != null)
            {
                if (Playlist == null)
                {
                    CreatePlaylist();
                }
                if (Playlist != null)
                {
                    if (isResuming && MusicPlayerProperties.Resume)
                    {
                        CurrentSong = Playlist.FirstOrDefault(p => p.Filename.Equals(MusicPlayerProperties.LastSong, StringComparison.OrdinalIgnoreCase));
                    }
                }
                if (CurrentSong != null)
                {
                    CurrentArtist = Library.Artists.First();
                    CurrentAlbum = CurrentArtist.Albums.First();
                    UpdatePage();
                    SongChanged();
                    if (CanPlay())
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            Play(CurrentSong, !Resume && (IsPlaying || ((firstPlay && MusicPlayerProperties.AutoPlay) || !firstPlay)));
                        });
                    }
                }
            }
        }

        private void Song_OnArtworkDownloaded(object sender, EventArgs e)
        {
            UpdatePage();
        }

        internal override void CreatePlaylist()
        {
            if (Library == null)
            {
                if (!IsLoading)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        LoadLibrary(CanPlayFirstSong);
                        CanPlayFirstSong = false;
                    });
                }
            }
            else if (!IsLoading)
            {
                Playlist = new List<FIPMusicSong>();
                Playlist.AddRange(Library.Songs.OrderBy(s => s.Distance));
                RandomList = GetUniqueRandoms(Playlist.Count);
                UpdatePage();
            }
        }

        internal override void UpdatePlayList()
        {
            try
            {
                Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(RadioProperties.FontColor))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.FormatFlags = StringFormatFlags.NoWrap;
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            format.Trimming = StringTrimming.EllipsisCharacter;
                            if (LibraryAlbum != null)
                            {
                                Rectangle destRect;
                                if (LibrarySong != null)
                                {
                                    int index = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList().IndexOf(LibrarySong);
                                    LibraryPageNumber = index != -1 ? Math.Max(0, index) / 5 : Math.Min(LibraryPageNumber, (LibraryAlbum.SongCount - 1) / 5);
                                }
                                destRect = new Rectangle(0, 0, 320, 40);
                                graphics.DrawString(LibraryAlbum.AlbumName, RadioProperties.Font, brush, destRect, format);
                                format.Alignment = StringAlignment.Near;
                                for (int i = 0; i < 5; i++)
                                {
                                    int count = LibraryAlbum.SongCount;
                                    if (i + (LibraryPageNumber * 5) >= count)
                                    {
                                        break;
                                    }
                                    FIPMusicSong song = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList()[i + (LibraryPageNumber * 5)];
                                    if (song == LibrarySong)
                                    {
                                        using (SolidBrush backBrush = new SolidBrush(song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.BackgroundColor.HasValue ? song.M3UMedia.Adornments.BackgroundColor.Value.Color : SystemColors.Highlight))
                                        {
                                            graphics.FillRectangle(backBrush, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                        }
                                    }
                                    destRect = new Rectangle(36, (40 * (i + 1)) + 2, 36, 36);
                                    if (song.Artwork != null)
                                    {
                                        graphics.DrawImage(song.Artwork, destRect, 0, 0, song.Artwork.Width, song.Artwork.Height, GraphicsUnit.Pixel);
                                    }
                                    else
                                    {
                                        graphics.DrawImage(FIPToolKit.Properties.Resources.Radio, destRect, 0, 0, FIPToolKit.Properties.Resources.Radio.Width, FIPToolKit.Properties.Resources.Radio.Height, GraphicsUnit.Pixel);
                                    }
                                    destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                    Color color = song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.Color.HasValue ? song.M3UMedia.Adornments.Color.Value : RadioProperties.FontColor;
                                    using (SolidBrush brush2 = new SolidBrush(color))
                                    {
                                        using (FontEx font = new FontEx(RadioProperties.Font, song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.FontStyle.HasValue ? song.M3UMedia.Adornments.FontStyle.Value : RadioProperties.Font.Style))
                                        {
                                            graphics.DrawString(song.Title, font, brush2, destRect, format);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.checkmark, Color.Green, true, SoftButtons.Button1);
                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.cancel, Color.Red, true, SoftButtons.Button2);
                }
                SendImage(bmp);
                bmp.Dispose();
            }
            catch
            {
            }
        }

        public override string EmptyPlaylistMessage
        {
            get
            {
                return "No Stations In Range";
            }
        }

        public override string EmptySongCountMessage
        {
            get
            {
                return "No Stations In Range";
            }
        }

        public override string EmptySongMessage
        {
            get
            {
                return "Select a Station";
            }
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            if (CurrentPage == MusicPlayerPage.Player)
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        RadioProperties.Mute = !RadioProperties.Mute;
                        if (Player != null)
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                Player.Mute = RadioProperties.Mute;
                                UpdatePage();
                            });
                        }
                        break;
                    case SoftButtons.Button2:
                        if (!IsLoading)
                        {
                            if (Player != null)
                            {
                                if (Player.Media != null)
                                {
                                    ThreadPool.QueueUserWorkItem(_ =>
                                    {
                                        if (Player.IsPlaying)
                                        {
                                            IsPlaying = false;
                                            Player.Pause();
                                        }
                                        else
                                        {
                                            IsPlaying = true;
                                            Player.Play();
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
                            }
                            else if (Player != null && Library == null && !string.IsNullOrEmpty(RadioProperties.Path))
                            {
                                ThreadPool.QueueUserWorkItem(_ =>
                                {
                                    LoadLibrary(false);
                                });
                            }
                            UpdatePage();
                        }
                        break;
                    case SoftButtons.Button6:
                        if (CurrentPage == MusicPlayerPage.Player)
                        {
                            if (!IsLoading && Library != null)
                            {
                                CurrentPage = MusicPlayerPage.Library;
                                LibrarySong = CurrentSong == null ? Library.Songs.OrderBy(s => s.Distance).FirstOrDefault() : CurrentSong;
                                LibraryArtist = Library.FirstArtist;
                                LibraryAlbum = LibraryArtist.FirstAlbum;
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
                            int newVolume = Player.Volume + 5;
                            newVolume = Math.Max(0, Math.Min(newVolume, 100));
                            Player.Volume = newVolume;
                        });
                        break;
                    case SoftButtons.Down:
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            int newVolume = Player.Volume - 5;
                            newVolume = Math.Max(0, Math.Min(newVolume, 100));
                            Player.Volume = newVolume;
                        });
                        break;
                    case SoftButtons.Left:
                        if (Player != null && Library != null && Playlist != null)
                        {
                            PlayPreviousTrack();
                        }
                        else if (Player != null && Library == null && !string.IsNullOrEmpty(RadioProperties.Path))
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                LoadLibrary(false);
                            });
                        }
                        break;
                    case SoftButtons.Right:
                        if (Player != null && Library != null && Playlist != null)
                        {
                            PlayNextTrack();
                        }
                        else if (Player != null && Library == null && !string.IsNullOrEmpty(RadioProperties.Path))
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
                        if (LibraryAlbum != null && LibraryAlbum.SongCount > 0)
                        {
                            if (LibrarySong != null)
                            {
                                int index = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList().IndexOf(LibrarySong);
                                if (index != -1)
                                {
                                    LibrarySong = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList()[Math.Max(0, index - 1)];
                                }
                                else
                                {
                                    index = Math.Min((LibraryPageNumber + 1) * 5, Math.Max(LibraryAlbum.SongCount - 1, 0));
                                    LibrarySong = index < LibraryAlbum.SongCount ? LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList()[index] : LibraryAlbum.Songs.OrderBy(s => s.Distance).FirstOrDefault();
                                }
                            }
                            else
                            {
                                LibrarySong = LibraryAlbum.Songs.OrderBy(s => s.Distance).FirstOrDefault();
                            }
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Right:
                    case SoftButtons.Up:
                        if (LibraryAlbum != null && LibraryAlbum.SongCount > 0)
                        {
                            if (LibrarySong != null)
                            {
                                int index = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList().IndexOf(LibrarySong);
                                if (index != -1)
                                {
                                    LibrarySong = LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList()[Math.Min(LibraryAlbum.SongCount - 1, index + 1)];
                                }
                                else
                                {
                                    index = Math.Min(LibraryPageNumber * 5, Math.Max(LibraryAlbum.SongCount - 1, 0));
                                    LibrarySong = index < LibraryAlbum.SongCount ? LibraryAlbum.Songs.OrderBy(s => s.Distance).ToList()[index] : LibraryAlbum.Songs.OrderBy(s => s.Distance).LastOrDefault();
                                }
                            }
                            else
                            {
                                LibrarySong = LibraryAlbum.Songs.OrderBy(s => s.Distance).LastOrDefault();
                            }
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button1:
                        CurrentPage = MusicPlayerPage.Player;
                        Play(LibrarySong);
                        UpdatePage();
                        break;
                    case SoftButtons.Button2:
                        CurrentPage = MusicPlayerPage.Player;
                        LibraryAlbum = null;
                        LibraryArtist = null;
                        LibrarySong = null;
                        UpdatePage();
                        break;
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                        BaseExecuteSoftButton(softButton);
                        break;
                    default:
                        base.ExecuteSoftButton(softButton);
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
                        return (Player != null);
                    case SoftButtons.Button2:
                        return (Player != null && Library != null && CurrentSong != null && !IsLoading);
                    case SoftButtons.Button6:
                        return (Library != null && !IsLoading);
                }
            }
            else
            {
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        return (Library != null && LibrarySong != null);
                    case SoftButtons.Button2:
                        return true;
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                        return false;
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
                    case SoftButtons.Left:
                    case SoftButtons.Right:
                    case SoftButtons.Up:
                    case SoftButtons.Down:
                        return false;
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                        return true;
                }
            }
            return base.IsButtonAssignable(softButton);
        }

        public override void Media_MetaChanged(object sender, MediaMetaChangedEventArgs e)
        {
            if (Player != null)
            {
                string meta = Player.Media.Meta(e.MetadataType);
                if (e.MetadataType == MetadataType.NowPlaying)
                {
                    if (CurrentSong.MetaData == null)
                    {
                        CurrentSong.MetaData = new FIPMetaData();
                    }
                    if (CurrentSong.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || CurrentSong.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        if (CurrentSong.Title.Equals(HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(CurrentSong.Filename))))
                        {
                            if (!string.IsNullOrEmpty(meta))
                            {
                                CurrentSong.MetaData.Title = meta;
                                CurrentSong.MetaData.Artist = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : Player.Media.Meta(MetadataType.Title);
                            }
                            else
                            {
                                CurrentSong.MetaData.Title = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : Player.Media.Meta(MetadataType.Title);
                                CurrentSong.MetaData.Artist = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.AlbumArtist)) ? (string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Artist)) ? CurrentSong.Artist : Player.Media.Meta(MetadataType.Artist)) : Player.Media.Meta(MetadataType.AlbumArtist);
                            }
                        }
                        else
                        {
                            CurrentSong.MetaData.Title = CurrentSong.Title;
                            CurrentSong.MetaData.Artist = !string.IsNullOrEmpty(meta) ? meta : string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : Player.Media.Meta(MetadataType.Title);
                        }
                        if (CurrentSong.Artwork != null)
                        {
                            CurrentSong.MetaData.Artwork = new Bitmap(CurrentSong.Artwork);
                        }
                    }
                    else
                    {
                        CurrentSong.MetaData.Title = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Title)) ? CurrentSong.Title : Player.Media.Meta(MetadataType.Title);
                        CurrentSong.MetaData.Artist = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.AlbumArtist)) ? (string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Artist)) ? CurrentSong.Artist : Player.Media.Meta(MetadataType.Artist)) : Player.Media.Meta(MetadataType.AlbumArtist);
                    }
                    CurrentSong.MetaData.Album = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Album)) ? CurrentSong.Album : Player.Media.Meta(MetadataType.Album);
                    CurrentSong.MetaData.Genre = string.IsNullOrEmpty(Player.Media.Meta(MetadataType.Genre)) ? CurrentSong.Genre : Player.Media.Meta(MetadataType.Genre);
                    CurrentSong.MetaData.Track = Convert.ToUInt32(string.IsNullOrEmpty(Player.Media.Meta(MetadataType.TrackNumber)) ? CurrentSong.Track.ToString() : Player.Media.Meta(MetadataType.TrackNumber));
                    UpdatePage();
                }
            }
        }
    }
}
