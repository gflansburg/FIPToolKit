using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
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
using System.Numerics;
using System.Threading;
using System.Web;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCRadio : FIPMusicPlayer, IFIPFSUIPC
    {
        public FIPFSUIPC FIPFSUIPC { get; set; } = new FIPFSUIPC();

        public FIPFSUIPCRadio(FIPRadioProperties properties) : base(properties) 
        {
            RadioProperties.Name = "Radio (FSUIPC)";
            RadioProperties.IsDirty = false;
            FIPFSUIPC.OnFlightDataReceived += FIPFSUIPCMap_OnFlightDataReceived;
        }

        private void FIPFSUIPCMap_OnFlightDataReceived()
        {
            LatLong location = new LatLong(FIPFSUIPC.Latitude, FIPFSUIPC.Longitude);
            if (!location.IsEmpty())
            {
                Location = location;
            }
            else
            {
                SetLocalLocation();
            }
        }

        private List<FIPRadioStation> GetStations()
        {
            RestClient client = new RestClient("https://cloud.gafware.com/Home");
            RestRequest request = new RestRequest("GetAllStations", Method.Get);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<FIPRadioStation>>(response.Content);
            }
            return null;
        }

        private FIPRadioProperties RadioProperties
        {
            get
            {
                return Properties as FIPRadioProperties;
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
                        FIPMusicSong song = new FIPMusicSong()
                        {
                            Title = station.Name,
                            Artist = "Playlists",
                            Album = Properties.Name,
                            Playlist = Properties.Name,
                            Genre = station.Tags,
                            Filename = station.Url,
                            Location = new FlightSim.LatLong(station.Geo_Lat, station.Geo_Long),
                            ListenerLocation = Location
                        };
                        if (!string.IsNullOrEmpty(station.Favicon))
                        {
                            song.LogoUrl = station.Favicon;
                            song.ArtworkDownloaded += Song_ArtworkDownloaded;
                        }
                        else
                        {
                            song.Artwork = new Bitmap(FIPToolKit.Properties.Resources.Radio.ChangeToColor(SystemColors.Highlight));
                        }
                        FIPMusicAlbum album = Library.GetAlbum("Playlists", Properties.Name, RadioProperties.RadioDistance);
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
                        PlayFirstSong(true, isResuming);
                    }
                }
                IsLoading = false;
                UpdatePage();
            }
        }

        private void Song_ArtworkDownloaded(object sender, EventArgs e)
        {
            UpdatePage();
        }

        internal override void CreatePlaylist()
        {
            Playlist = new List<FIPMusicSong>();
            Playlist.AddRange(Library.Songs);
            RandomList = GetUniqueRandoms(Playlist.Count);
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
                            int page = 0;
                            Rectangle destRect;
                            if (LibrarySong != null)
                            {
                                page = Math.Max(0, LibraryAlbum.Songs.ToList().IndexOf(LibrarySong)) / 5;
                            }
                            if (LibraryAlbum != null)
                            {
                                destRect = new Rectangle(0, 0, 320, 40);
                                graphics.DrawString(LibraryAlbum.AlbumName, RadioProperties.Font, brush, destRect, format);
                            }
                            format.Alignment = StringAlignment.Near;
                            for (int i = 0; i < 5; i++)
                            {
                                int count = LibraryAlbum.SongCount;
                                if (i + (page * 5) >= count)
                                {
                                    break;
                                }
                                FIPMusicSong song = LibraryAlbum.Songs.ToList()[i + (page * 5)];
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
                                else if (LibraryAlbum.IsPlaylist)
                                {
                                    graphics.DrawImage(FIPToolKit.Properties.Resources.playlist, destRect, 0, 0, FIPToolKit.Properties.Resources.playlist.Width, FIPToolKit.Properties.Resources.playlist.Height, GraphicsUnit.Pixel);
                                }
                                destRect = new Rectangle(76, (40 * (i + 1)) + 2, 254, 40);
                                Color color = song.M3UMedia != null && song.M3UMedia.Adornments != null && song.M3UMedia.Adornments.Color.HasValue ? song.M3UMedia.Adornments.Color.Value : RadioProperties.FontColor;
                                using (SolidBrush brush2 = new SolidBrush(color))
                                {
                                    using (FontEx font = new FontEx(RadioProperties.Font, CurrentSong.M3UMedia != null && CurrentSong.M3UMedia.Adornments != null && CurrentSong.M3UMedia.Adornments.FontStyle.HasValue ? CurrentSong.M3UMedia.Adornments.FontStyle.Value : RadioProperties.Font.Style))
                                    {
                                        graphics.DrawString(song.Title, font, brush2, destRect, format);
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

        public override string EmptySongCountMessage
        {
            get
            {
                return "There are no radio stations in range.";
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
                                        Play(CurrentSong.Filename, true);
                                        UpdatePage();
                                    });
                                }
                                else
                                {
                                    PlayFirstSong(false);
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
                                LibrarySong = CurrentSong;
                                LibraryAlbum = CurrentAlbum;
                                LibraryArtist = CurrentArtist;
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
                                LibrarySong = LibraryAlbum.Songs.ToList()[Math.Max(0, LibraryAlbum.Songs.ToList().IndexOf(LibrarySong) - 1)];
                            }
                            else
                            {
                                LibrarySong = LibraryAlbum.Songs.Last();
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
                                LibrarySong = LibraryAlbum.Songs.ToList()[Math.Min(LibraryAlbum.SongCount - 1, LibraryAlbum.Songs.ToList().IndexOf(LibrarySong) + 1)];
                            }
                            else
                            {
                                LibrarySong = LibraryAlbum.Songs.First();
                            }
                        }
                        UpdatePage();
                        break;
                    case SoftButtons.Button1:
                        SongChanged();
                        CurrentPage = MusicPlayerPage.Player;
                        CreatePlaylist();
                        UpdatePage();
                        PlayFirstSong(true);
                        break;
                    case SoftButtons.Button2:
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
