using FIPToolKit.Models;
using FIPToolKit.Threading;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FIPToolKit.Tools
{
    public enum SpotifyStateType
    {
        Closed = 0,
        Playing = 1,
        Paused = 2,
        Error = 4,
        Running = Playing | Paused
    }

    [Serializable]
    public class SpotifyController : IDisposable
    {
        private static AbortableBackgroundWorker timer;
        private static AbortableBackgroundWorker lazyLoader;
        private static bool stop = false;
        private static bool stopLazyLoader = false;
        private static int _volumePercent = 0;
        private static DateTime? lastFullRequestTime = null;
        private static List<Bitmap> _albumArtwork = new List<Bitmap>();
        private static PlaybackContext _playbackContext;
        private static SpotifyStateType _spotifyState = SpotifyStateType.Closed;
        public static event TrackStateChangedEventHandler TrackStateChanged;
        public static event ImageStateChangedEventHandler ImageStateChanged;
        public static event ErrorEventHandler OnError;
        public delegate void TrackStateChangedEventHandler(PlaybackContext playback, SpotifyStateType state);
        public delegate void ImageStateChangedEventHandler(List<Bitmap> image);
        public delegate void ErrorEventHandler(string message);

        private const int REFRESH_RATE = 2000;
        private static bool _addArtistImages { get; set; }

        public static bool AddArtistImages
        { 
            get
            {
                return _addArtistImages;
            }
            set
            {
                _addArtistImages = value;
                if (_addArtistImages && _playbackContext != null && _playbackContext.Item != null)
                {
                    StartLazyLoader(_playbackContext.Item);
                }
            }
        }

        public static bool CacheArtwork { get; set; }
        public static SpotifyWebAPI SpotifyWebAPI { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public static int RetryAfter
        {
            get
            {
                if (_playbackContext != null)
                {
                    return _playbackContext.RetryAfter;
                }
                return 0;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static SpotifyStateType SpotifyState
        {
            get
            {
                return _spotifyState;
            }

            set
            {
                if (value != _spotifyState)
                {
                    _spotifyState = value;
                    TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string Error
        {
            get
            {
                if (_playbackContext != null && _playbackContext.Error != null)
                {
                    return _playbackContext.Error.Message;
                }
                return String.Empty;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string TrackTitle
        {
            get
            {
                if (_playbackContext != null && _playbackContext.Item != null)
                {
                    return _playbackContext.Item.Name;
                }
                return String.Empty;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static List<Bitmap> AlbumArtwork
        {
            get
            {
                return _albumArtwork;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string TrackArtist
        {
            get
            {
                if (_playbackContext != null && _playbackContext.Item != null)
                {
                    List<string> artists = new List<string>();
                    foreach(SimpleArtist artist in _playbackContext.Item.Artists)
                    {
                        artists.Add(artist.Name);
                    }
                    return String.Join(", ", artists);
                }
                return String.Empty;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string TrackAlbum
        {
            get
            {
                if (_playbackContext != null && _playbackContext.Item != null)
                {
                    return _playbackContext.Item.Album.Name;
                }
                return String.Empty;
            }
        }

        public SpotifyController()
        {
            if (timer == null)
            {
                CacheArtwork = true;
                timer = new AbortableBackgroundWorker();
                timer.DoWork += RefreshController;
                timer.RunWorkerAsync(this);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsPlaying
        {
            get
            {
                if(_playbackContext != null)
                {
                    return _spotifyState == SpotifyStateType.Playing;
                }
                return false;
            }
        }

        public static bool Mute
        {
            get
            {
                return (_playbackContext != null && _playbackContext.Device != null && _playbackContext.Device.VolumePercent == 0);
            }
            set
            {
                if (SpotifyWebAPI != null)
                {
                    if (value == true)
                    {
                        SpotifyWebAPI.SetVolume(0);
                        _playbackContext.Device.VolumePercent = 0;
                        TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                    }
                    else
                    {
                        SpotifyWebAPI.SetVolume(_volumePercent == 0 ? 100 : _volumePercent);
                        _playbackContext.Device.VolumePercent = _volumePercent == 0 ? 100 : _volumePercent;
                        TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                    }
                }
            }
        }

        public static void Stop()
        {
            if (SpotifyWebAPI != null)
            {
                SpotifyWebAPI.PausePlayback();
            }
        }

        public static void PlayPause()
        {
            if (SpotifyWebAPI != null)
            {
                if (_playbackContext.IsPlaying)
                {
                    SpotifyWebAPI.PausePlayback();
                    _playbackContext.IsPlaying = false;
                    SpotifyState = SpotifyStateType.Paused;
                }
                else
                {
                    SpotifyWebAPI.ResumePlayback(String.Empty, String.Empty, null, String.Empty, 0);
                    _playbackContext.IsPlaying = true;
                    SpotifyState = SpotifyStateType.Playing;
                }
            }
        }

        public static void PlayPrev()
        {
            if (SpotifyWebAPI != null)
            {
                SpotifyWebAPI.SkipPlaybackToPrevious();
            }
        }

        public static void PlayNext()
        {
            if (SpotifyWebAPI != null)
            {
                SpotifyWebAPI.SkipPlaybackToNext();
            }
        }

        public static void VolumeUp()
        {
            if(SpotifyWebAPI != null && _playbackContext != null && _playbackContext.Device != null)
            {
                int volumePercent = ActiveDevice.VolumePercent;
                volumePercent = Math.Min(100, volumePercent + 5);
                if (volumePercent != _playbackContext.Device.VolumePercent)
                {
                    _playbackContext.Device.VolumePercent = volumePercent;
                    SpotifyWebAPI.SetVolume(volumePercent);
                }
            }
        }

        public static void VolumeDown()
        {
            if (SpotifyWebAPI != null && _playbackContext != null && _playbackContext.Device != null)
            {
                int volumePercent = _playbackContext.Device.VolumePercent;
                volumePercent = Math.Max(0, volumePercent - 5);
                if (volumePercent != _playbackContext.Device.VolumePercent)
                {
                    _playbackContext.Device.VolumePercent = volumePercent;
                    SpotifyWebAPI.SetVolume(volumePercent);
                }
            }
        }

        public static bool ShuffleState
        {
            get
            {
                if (SpotifyWebAPI != null && _playbackContext != null)
                {
                    return _playbackContext.ShuffleState;
                }
                return false;
            }
            set
            {
                if (SpotifyWebAPI != null && _playbackContext != null)
                {
                    _playbackContext.ShuffleState = value;
                    SpotifyWebAPI.SetShuffle(_playbackContext.ShuffleState);
                }
            }
        }

        public static RepeatState RepeatState
        {
            get
            {
                if (SpotifyWebAPI != null && _playbackContext != null)
                {
                    return _playbackContext.RepeatState;
                }
                return RepeatState.Off;
            }
            set
            {
                if (SpotifyWebAPI != null && _playbackContext != null)
                {
                    _playbackContext.RepeatState = value;
                    SpotifyWebAPI.SetRepeatMode(_playbackContext.RepeatState);
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static Device ActiveDevice
        {
            get
            {
                if (_playbackContext == null || _playbackContext.Device == null)
                {
                    if (SpotifyWebAPI != null)
                    {
                        AvailabeDevices devices = SpotifyWebAPI.GetDevices();
                        if (devices != null && devices.Devices != null && devices.Error == null)
                        {
                            foreach (Device device in devices.Devices)
                            {
                                if (device.IsActive)
                                {
                                    return device;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return _playbackContext.Device;
                }
                return null;
            }
        }

        public static bool IsLiked(string id)
        {
            if (SpotifyWebAPI != null)
            {
                ListResponse<bool> liked = SpotifyWebAPI.CheckSavedTracks(new List<string> { id });
                if(liked != null && liked.List != null && liked.List.Count > 0)
                {
                    return liked.List[0];
                }
            }
            return false;
        }

        public static void Like(string id)
        {
            if (SpotifyWebAPI != null)
            {
                SpotifyWebAPI.SaveTrack(id);
            }
        }

        public static void UnLike(string id)
        {
            if (SpotifyWebAPI != null)
            {
                SpotifyWebAPI.RemoveSavedTrack(id);
            }
        }

        public static void PlayUserPlaylist(string user, string playList)
        {
            string uri = string.Format("spotify:user:{0}:playlist:{1}", user, playList);
            if(SpotifyWebAPI != null)
            {
                SpotifyWebAPI.ResumePlayback(String.Empty, uri, null, 0, 0);
            }
        }

        private static void RefreshController(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DateTime retryStartTime = DateTime.Now;
            DateTime refreshTime = DateTime.Now;
            bool retryWait = false;
            int originalWait = 0;
            while (!stop)
            {
                refreshTime = DateTime.Now;
                if (_playbackContext != null && _playbackContext.RetryAfter > 0)
                {
                    _spotifyState = SpotifyStateType.Error;
                    if (!retryWait)
                    {
                        originalWait = _playbackContext.RetryAfter;
                        retryStartTime = DateTime.Now;
                        retryWait = true;
                    }
                    _playbackContext.RetryAfter = originalWait - (int)(DateTime.Now - retryStartTime).TotalSeconds;
                    _playbackContext.RetryAfter = Math.Max(0, _playbackContext.RetryAfter);
                    if (_playbackContext.RetryAfter == 0)
                    {
                        retryWait = false;
                    }
                    TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                    Thread.Sleep(100);
                }
                else
                {
                    GetNowPlaying(false);
                    while ((DateTime.Now - refreshTime).TotalMilliseconds < REFRESH_RATE && !stop)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private static PlaybackContext GetNowPlaying(bool forced)
        {
            bool isChanged = false;
            if (SpotifyWebAPI != null)
            {
                PlaybackContext playBack;
                if (lastFullRequestTime != null && (DateTime.Now - lastFullRequestTime.Value).TotalMilliseconds < 8000)
                {
                    playBack = SpotifyWebAPI.GetPlayingTrack();
                    if(playBack != null)
                    {
                        SpotifyStateType currentState = _spotifyState;
                        if (playBack.Error == null && _playbackContext != null && _playbackContext.Item != null && playBack.Item != null && playBack.Item.Id != _playbackContext.Item.Id)
                        {
                            isChanged = true;
                            Debug.WriteLine("Old: {0}, Now playing: {1}", _playbackContext.Item.Id, playBack.Item.Id);
                        }
                        if (playBack.Error != null)
                        {
                            // This stupid Forbidden error just started happening.  It might be related to the refresh rate. Ignore for now.
                            if (!playBack.Error.Message.Equals("SpotifyAPI.Web - Forbidden"))
                            {
                                currentState = SpotifyStateType.Error;
                                OnError?.Invoke(playBack.Error.Message);
                            }
                        }
                        else if (forced == true || isChanged || _spotifyState != currentState)
                        {
                            StopLazyLoader();
                            _playbackContext.Item = playBack.Item;
                            _albumArtwork.Clear();
                            _spotifyState = currentState;
                            TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                            StartLazyLoader(_playbackContext.Item);
                        }
                    }
                }
                else
                {
                    lastFullRequestTime = DateTime.Now;
                    playBack = SpotifyWebAPI.GetPlayback();
                    if (playBack != null)
                    {
                        SpotifyStateType currentState = _spotifyState;
                        if (playBack.Device == null && playBack.Error == null)
                        {
                            currentState = SpotifyStateType.Closed;
                        }
                        else if (playBack.IsPlaying && playBack.Error == null)
                        {
                            currentState = playBack.Device == null ? SpotifyStateType.Closed : playBack.IsPlaying ? SpotifyStateType.Playing : SpotifyStateType.Paused;
                        }
                        if (playBack.Error == null && _playbackContext != null && _playbackContext.Item != null && playBack.Item != null && playBack.Item.Id != _playbackContext.Item.Id)
                        {
                            isChanged = true;
                            Debug.WriteLine("Old: {0}, Now playing: {1}", _playbackContext.Item.Id, playBack.Item.Id);
                        }
                        else if (playBack.Error == null && _playbackContext != null && (playBack.RepeatState != _playbackContext.RepeatState || playBack.ShuffleState != _playbackContext.ShuffleState))
                        {
                            isChanged = true;
                            Debug.WriteLine("Repeat State: {0}, Shuffle State: {1}", playBack.RepeatState, playBack.ShuffleState);
                        }
                        if (playBack.Error == null)
                        {
                            _playbackContext = playBack;
                        }
                        if (_playbackContext != null && _playbackContext.Device != null && _playbackContext.Device.VolumePercent > 0)
                        {
                            _volumePercent = _playbackContext.Device.VolumePercent;
                        }
                        if (playBack.Error != null)
                        {
                            // This stupid Forbidden error just started happening.  It might be related to the refresh rate. Ignore for now.
                            if (!playBack.Error.Message.Equals("SpotifyAPI.Web - Forbidden"))
                            {
                                currentState = SpotifyStateType.Error;
                                OnError?.Invoke(playBack.Error.Message);
                            }
                        }
                        else if (forced == true || isChanged || _spotifyState != currentState)
                        {
                            StopLazyLoader();
                            _albumArtwork.Clear();
                            _spotifyState = currentState;
                            TrackStateChanged?.Invoke(_playbackContext, _spotifyState);
                            StartLazyLoader(_playbackContext.Item);
                        }
                    }
                }
            }
            return _playbackContext;
        }

        private static void StartLazyLoader(FullTrack track)
        {
            if (lazyLoader == null)
            {
                foreach (Bitmap bmp in _albumArtwork)
                {
                    bmp.Dispose();
                }
                _albumArtwork.Clear();
                if (SpotifyWebAPI != null)
                {
                    lazyLoader = new AbortableBackgroundWorker();
                    lazyLoader.DoWork += LazyLoader_DoWork;
                    lazyLoader.RunWorkerCompleted += LazyLoader_RunWorkerCompleted;
                    lazyLoader.RunWorkerAsync(track);
                }
            }
        }

        private static void LazyLoader_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ImageStateChanged?.Invoke(_albumArtwork);
            StopLazyLoader();
        }

        private static void LazyLoader_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            stopLazyLoader = false;
            FullTrack track = e.Argument as FullTrack;
            if (track != null)
            {
                int height = 0;
                int index = -1;
                using (WebClient client = new WebClient())
                {
                    if (track.Album != null && track.Album.Images != null)
                    {
                        //Get the biggest image. Spotify API says the widest one is first but we want the tallest.
                        for (int i = 0; i < track.Album.Images.Count; i++)
                        {
                            if (stopLazyLoader)
                            {
                                break;
                            }
                            SpotifyAPI.Web.Models.Image image = track.Album.Images[i];
                            if (image.Height > height)
                            {
                                height = image.Height;
                                index = i;
                            }
                        }
                        if (index != -1 && !stopLazyLoader && index < track.Album.Images.Count)
                        {
                            string filename = String.Format("{0}\\Images\\{1}.bmp", FIPToolKit.FlightSim.Tools.GetExecutingDirectory(), System.IO.Path.GetFileName(track.Album.Images[index].Url));
                            if (CacheArtwork && System.IO.File.Exists(filename))
                            {
                                try
                                {
                                    using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                                    {
                                        using (Bitmap bitmap = new Bitmap(stream))
                                        {
                                            stream.Flush();
                                            stream.Close();
                                            _albumArtwork.Add(new Bitmap(bitmap));
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    using (Stream stream = client.OpenRead(track.Album.Images[index].Url))
                                    {
                                        using (Bitmap bitmap = new Bitmap(stream))
                                        {
                                            stream.Flush();
                                            stream.Close();
                                            _albumArtwork.Add(new Bitmap(bitmap));
                                            if (CacheArtwork)
                                            {
                                                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
                                                {
                                                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));
                                                }
                                                bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    if (AddArtistImages && !stopLazyLoader && track.Artists != null)
                    {
                        foreach (SimpleArtist artist in track.Artists)
                        {
                            if (stopLazyLoader)
                            {
                                break;
                            }
                            FullArtist fullArtist = SpotifyWebAPI.GetArtist(artist.Id);
                            if (fullArtist != null && fullArtist.Images != null)
                            {
                                height = 0;
                                index = -1;
                                for (int i = 0; i < fullArtist.Images.Count; i++)
                                {
                                    if (stopLazyLoader)
                                    {
                                        break;
                                    }
                                    SpotifyAPI.Web.Models.Image image = fullArtist.Images[i];
                                    if (image.Height > height)
                                    {
                                        height = image.Height;
                                        index = i;
                                    }
                                }
                                if (index != -1 && !stopLazyLoader && index < fullArtist.Images.Count)
                                {
                                    SpotifyAPI.Web.Models.Image image = fullArtist.Images[index];
                                    string filename = String.Format("{0}\\Images\\{1}.bmp", FIPToolKit.FlightSim.Tools.GetExecutingDirectory(), System.IO.Path.GetFileName(image.Url));
                                    if (CacheArtwork && System.IO.File.Exists(filename))
                                    {
                                        try
                                        {
                                            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                                            {
                                                using (Bitmap bitmap = new Bitmap(stream))
                                                {
                                                    stream.Flush();
                                                    stream.Close();
                                                    _albumArtwork.Add(new Bitmap(bitmap));
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            using (Stream stream = client.OpenRead(image.Url))
                                            {
                                                using (Bitmap bitmap = new Bitmap(stream))
                                                {
                                                    stream.Flush();
                                                    stream.Close();
                                                    _albumArtwork.Add(new Bitmap(bitmap));
                                                    if (CacheArtwork)
                                                    {
                                                        if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
                                                        {
                                                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));
                                                        }
                                                        bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void StopLazyLoader(int timeOut = 1000)
        {
            if (lazyLoader != null)
            {
                stopLazyLoader = true;
                DateTime stopTime = DateTime.Now;
                while (lazyLoader.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (lazyLoader.IsRunning)
                {
                    lazyLoader.Abort();
                }
                lazyLoader = null;
            }
        }

        public void Dispose()
        {
            if (timer != null)
            {
                stop = true;
                DateTime stopTime = DateTime.Now;
                while (timer.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > 1000)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (timer.IsRunning)
                {
                    timer.Abort();
                }
                timer = null;
            }
            StopLazyLoader();
            foreach(Bitmap bmp in _albumArtwork)
            {
                bmp.Dispose();
            }
            _albumArtwork.Clear();
        }
    }
}
