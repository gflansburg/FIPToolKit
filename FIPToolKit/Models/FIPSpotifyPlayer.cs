using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Saitek.DirectOutput;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace FIPToolKit.Models
{
    public class FIPCanPlayEventArgs : EventArgs
    {
        public FIPPage Page { get; private set; }
        public bool CanPlay { get; set; } = true;

        public FIPCanPlayEventArgs(FIPPage page)
        {
            Page = page;
        }
    }

    public enum SpotifyPlayerPage
    {
        Player,
        Playlist
    }

    public class SpotifyPlaylist : IDisposable
    {
        public SimplePlaylist SimplePlaylist { get; set; }
        public Bitmap AlbumArtwork { get; set; }
        public int Index { get; set; }
        public int Page { get; set; }
        public int SortOrder { get; set; }

        public void Dispose()
        {
            if(AlbumArtwork != null)
            {
                AlbumArtwork.Dispose();
                AlbumArtwork = null;
            }
        }
    }

    [Serializable]
    public class FIPPlayList
    {
        public string UserId { get; set; }
        public string PlaylistId { get; set; }
    }

    public class FIPSpotifyPlayer : FIPPage
    {
        public bool IsAuthenticating { get; set; } = false;
        
        private AuthorizationCodeAuth authorizationCodeAuth;
        private bool _showArtistImages = true;
        private bool _cacheArtwork = true;
        private Timer Timer;

        public bool IsConfigured
        {
            get
            {
                return !string.IsNullOrEmpty(SpotifyPlayerProperties.ClientId) && !string.IsNullOrEmpty(SpotifyPlayerProperties.SecretId);
            }
        }

        public bool IsAuthorized
        {
            get
            {
                return SpotifyPlayerProperties.Token != null && !string.IsNullOrEmpty(SpotifyPlayerProperties.Token.AccessToken) && !SpotifyPlayerProperties.Token.IsExpired();
            }
        }

        private Microsoft.Web.WebView2.WinForms.WebView2 _browser;
        public Microsoft.Web.WebView2.WinForms.WebView2 Browser 
        { 
            get
            {
                return _browser;
            }
            set
            {
                if(_browser != value)
                {
                    _browser = value;
                    if (authorizationCodeAuth != null)
                    {
                        authorizationCodeAuth.Browser = _browser;
                    }
                }
            }
        }

        public SpotifyWebAPI SpotifyAPI { get; private set; }

        public int PlayListIndex { get; set; }

        public bool AutoPlayLastPlaylist { get; set; }

        public bool ShowArtistImages
        { 
            get
            {
                return _showArtistImages;
            }
            set
            {
                if (_showArtistImages != value)
                {
                    _showArtistImages = value;
                    if (SpotifyController != null)
                    {
                        SpotifyController.AddArtistImages = _showArtistImages;
                    }
                    ShowArtistImagesChanged();
                }
            }
        }

        public bool CacheArtwork
        {
            get
            {
                return _cacheArtwork;
            }
            set
            {
                if (_cacheArtwork != value)
                {
                    _cacheArtwork = value;
                    if (SpotifyController != null)
                    {
                        SpotifyController.CacheArtwork = _cacheArtwork;
                    }
                }
            }
        }

        public List<SpotifyPlaylist> PlayLists { get; private set; }

        private AbortableBackgroundWorker LazyLoader { get; set; }
        private bool Stop { get; set; }

        public bool IsRunning { get; private set; }

        public SpotifyPlayerPage CurrentPage { get; set; }

        public PlaybackContext CurrentPlaybackContext { get; set; }

        private SpotifyController SpotifyController { get; set; }

        private string ErrorMessage { get; set; }

        private int ImageIndex { get;set; }

        public event TrackStateChangedEventHandler OnTrackStateChanged;
        public delegate void TrackStateChangedEventHandler(PlaybackContext playback, SpotifyStateType state);
        public event TokenChangedEventHandler OnTokenChanged;
        public delegate void TokenChangedEventHandler(Token token);
        public delegate void FIPCanPlayEventHandler(object sender, FIPCanPlayEventArgs e);
        public event FIPPageEventHandler OnBeginAuthentication;
        public event FIPPageEventHandler OnEndAuthentication;
        public event FIPCanPlayEventHandler OnCanPlay;
        public event FIPPageEventHandler OnMuteChanged;
        public event FIPPageEventHandler OnVolumeChanged;

        public FIPSpotifyPlayer(FIPSpotifyPlayerProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnTokenExpired += Properties_OnTokenExpired;
            properties.OnTokenChanged += Properties_OnTokenChanged;
            properties.OnTokenCleared += Properties_OnTokenCleared;
            properties.OnVolumeChanged += Properties_OnVolumeChanged;
            properties.OnMuteChanged += Properties_OnMuteChanged;
            PlayLists = new List<SpotifyPlaylist>();
            Properties_OnTokenChanged(this, EventArgs.Empty);
            SpotifyController = new SpotifyController();
            SpotifyController.SpotifyWebAPI = SpotifyAPI;
            SpotifyController.AddArtistImages = ShowArtistImages;
            SpotifyController.TrackStateChanged += SpotifyController_TrackStateChanged;
            SpotifyController.PlayerStateChanged += SpotifyController_PlayerStateChanged;
            SpotifyController.VolumeChanged += SpotifyController_VolumeChanged;
            SpotifyController.MuteChanged += SpotifyController_MuteChanged;
            SpotifyController.OnError += SpotifyController_OnError;
            PlayListIndex = -1;
            UpdatePage();
        }

        private void SpotifyController_MuteChanged(bool mute)
        {
            SpotifyPlayerProperties.SetMute(mute);
            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void SpotifyController_VolumeChanged(int volume)
        {
            SpotifyPlayerProperties.SetVolume(volume);
            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void Properties_OnMuteChanged(object sender, EventArgs e)
        {
            if (SpotifyController != null && SpotifyController.Mute != SpotifyPlayerProperties.Mute)
            {
                SpotifyController.Mute = SpotifyPlayerProperties.Mute;
            }
            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void Properties_OnVolumeChanged(object sender, EventArgs e)
        {
            if (SpotifyController != null && SpotifyController.Volume != SpotifyPlayerProperties.Volume)
            {
                SpotifyController.Volume = SpotifyPlayerProperties.Volume;
            }
            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void SpotifyController_PlayerStateChanged(SpotifyStateType state)
        {
            if (state == SpotifyStateType.Playing && SpotifyPlayerProperties.PauseOtherMedia)
            {
                SendActive();
            }
            else if (state == SpotifyStateType.Paused && SpotifyPlayerProperties.PauseOtherMedia)
            {
                SendInactive();
            }
        }

        private FIPSpotifyPlayerProperties SpotifyPlayerProperties
        {
            get
            {
                return Properties as FIPSpotifyPlayerProperties;
            }
        }

        private void Properties_OnTokenCleared(object sender, EventArgs e)
        {
            SpotifyAPI = null;
            if (SpotifyController != null)
            {
                SpotifyController.SpotifyWebAPI = null;
            }
            Authenticate();
        }

        private void Properties_OnTokenChanged(object sender, EventArgs e)
        {
            if (SpotifyPlayerProperties.Token != null)
            {
                if (SpotifyAPI == null)
                {
                    SpotifyAPI = new SpotifyWebAPI
                    {
                        AccessToken = SpotifyPlayerProperties.Token.AccessToken,
                        TokenType = SpotifyPlayerProperties.Token.TokenType
                    };
                }
                else
                {
                    SpotifyAPI.AccessToken = SpotifyPlayerProperties.Token.AccessToken;
                    SpotifyAPI.TokenType = SpotifyPlayerProperties.Token.TokenType;
                }
            }
            else if (SpotifyAPI != null)
            {
                SpotifyAPI.Dispose();
                SpotifyAPI = null;
            }
            if (SpotifyController != null)
            {
                SpotifyController.SpotifyWebAPI = SpotifyAPI;
                SpotifyController.Volume = SpotifyPlayerProperties.Volume;
                SpotifyController.Mute = SpotifyPlayerProperties.Mute;
            }
            OnTokenChanged?.Invoke(SpotifyPlayerProperties.Token);
        }

        private void Properties_OnTokenExpired(object sender, EventArgs e)
        {
            RefreshToken();
        }

        private void InitAuthServer()
        {
            if (!string.IsNullOrEmpty(SpotifyPlayerProperties.ClientId) && !string.IsNullOrEmpty(SpotifyPlayerProperties.SecretId))
            {
                authorizationCodeAuth = new AuthorizationCodeAuth(SpotifyPlayerProperties.ClientId, SpotifyPlayerProperties.SecretId, "http://localhost:51400", "http://localhost:51400", Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative | Scope.UserReadCurrentlyPlaying | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState | Scope.UserLibraryRead | Scope.UserLibraryModify);
                authorizationCodeAuth.Browser = Browser;
                authorizationCodeAuth.AuthReceived += AuthOnAuthReceived;
                authorizationCodeAuth.AuthReceived += LoadController;
            }
            else
            {
                authorizationCodeAuth = null;
            }
        }

        private void ShowArtistImagesChanged()
        {
            if (Timer != null)
            {
                Timer.Change(_showArtistImages ? 5000 : Timeout.Infinite, _showArtistImages ? 5000 : Timeout.Infinite);
            }
            if (!_showArtistImages && IsRunning)
            {
                ImageIndex = 0;
                UpdateArtwork();
            }
        }

        private void SpotifyController_ImageStateChanged(List<Bitmap> image)
        {
            ImageIndex = 0;
            if (CurrentPage == SpotifyPlayerPage.Player)
            {
                UpdatePlayer();
            }
            FireStateChanged();
        }

        public override void UpdatePage()
        {
            if (CurrentPage == SpotifyPlayerPage.Player)
            {
                UpdatePlayer();
            }
            else
            {
                UpdatePlayList();
            }
            SetLEDs();
        }
        
        private void SpotifyController_TrackStateChanged(PlaybackContext playbackContext, SpotifyStateType state)
        {
            CurrentPlaybackContext = playbackContext;
            if (state == SpotifyStateType.Error)
            {
                CurrentPage = SpotifyPlayerPage.Player;
            }
            if (CurrentPage == SpotifyPlayerPage.Player)
            {
                UpdatePlayer();
            }
            SetLEDs();
            if (SpotifyController.IsPlaying && !CanPlay())
            {
                SpotifyController.PlayPause();
            }
            OnTrackStateChanged?.Invoke(playbackContext, state);
        }

        private void SpotifyController_OnError(string message)
        {
            CurrentPage = SpotifyPlayerPage.Player;
            if (SpotifyPlayerProperties.Token != null && SpotifyPlayerProperties.Token.IsExpired())
            {
                RefreshToken();
            }
            else
            {
                ErrorMessage = message;
                if (SpotifyController.RetryAfter > 0)
                {
                    TimeSpan t = TimeSpan.FromSeconds(SpotifyController.RetryAfter);
                    string time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
                    ErrorMessage += string.Format("\nRetrying in {0}", time);
                }
                Bitmap bmp = Drawing.ImageHelper.GetErrorImage(ErrorMessage);
                SendImage(bmp);
                bmp.Dispose();
            }
            SetLEDs();
        }

        private void ImageTimer(object state)
        {
            FIPSpotifyPlayer spotifyController = state as FIPSpotifyPlayer;
            if (spotifyController != null && SpotifyController != null)
            {
                if (SpotifyController.SpotifyState == SpotifyStateType.Paused || SpotifyController.SpotifyState == SpotifyStateType.Playing)
                {
                    if (SpotifyController.AlbumArtwork.Count > 0)
                    {
                        int newImageIndex = spotifyController.ImageIndex + 1;
                        if (newImageIndex >= SpotifyController.AlbumArtwork.Count)
                        {
                            newImageIndex = 0;
                        }
                        spotifyController.ImageIndex = newImageIndex;
                        spotifyController.UpdateArtwork();
                    }
                }
            }
        }

        public void CancelAuthenticate()
        {
            if(IsAuthenticating && authorizationCodeAuth != null)
            {
                authorizationCodeAuth.Stop();
                IsAuthenticating = false;
            }
        }

        public void Authenticate()
        {
            if (IsActive && (SpotifyPlayerProperties.Token == null || SpotifyPlayerProperties.Token.IsExpired()) && !IsAuthenticating && authorizationCodeAuth != null)
            {
                OnBeginAuthentication?.Invoke(this, new FIPPageEventArgs(this));
                IsAuthenticating = true;
                authorizationCodeAuth.Start();
                authorizationCodeAuth.OpenBrowser();
            }
        }

        public async void RefreshToken()
        {
            if (authorizationCodeAuth != null)
            {
                if (SpotifyPlayerProperties.Token != null && SpotifyPlayerProperties.Token.IsExpired() && !string.IsNullOrEmpty(SpotifyPlayerProperties.Token.RefreshToken) && !IsAuthenticating)
                {
                    IsAuthenticating = true;
                    SpotifyPlayerProperties.Token = await authorizationCodeAuth.RefreshToken(SpotifyPlayerProperties.Token.RefreshToken);
                    if (SpotifyPlayerProperties.Token != null)
                    {
                        if (SpotifyAPI == null)
                        {
                            SpotifyAPI = new SpotifyWebAPI
                            {
                                AccessToken = SpotifyPlayerProperties.Token.AccessToken,
                                TokenType = SpotifyPlayerProperties.Token.TokenType
                            };
                        }
                        else
                        {
                            SpotifyAPI.AccessToken = SpotifyPlayerProperties.Token.AccessToken;
                            SpotifyAPI.TokenType = SpotifyPlayerProperties.Token.TokenType;
                        }
                    }
                    IsAuthenticating = false;
                }
                else if (SpotifyPlayerProperties.Token == null || SpotifyPlayerProperties.Token.IsExpired())
                {
                    Authenticate();
                }
            }
        }

        private async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            authorizationCodeAuth.Stop();
            SpotifyPlayerProperties.Token = await authorizationCodeAuth.ExchangeCode(payload.Code);
            if (SpotifyPlayerProperties.Token != null)
            {
                if (SpotifyAPI == null)
                {
                    SpotifyAPI = new SpotifyWebAPI
                    {
                        AccessToken = SpotifyPlayerProperties.Token.AccessToken,
                        TokenType = SpotifyPlayerProperties.Token.TokenType
                    };
                }
                else
                {
                    SpotifyAPI.AccessToken = SpotifyPlayerProperties.Token.AccessToken;
                    SpotifyAPI.TokenType = SpotifyPlayerProperties.Token.TokenType;
                }
            }
            IsAuthenticating = false;
            OnEndAuthentication?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void LoadController(object sender, AuthorizationCode payload)
        {
            Init();
        }

        public override void StartTimer()
        {
            InitAuthServer();
            if (!IsRunning)
            {
                IsRunning = true;
                Timer = new System.Threading.Timer(new TimerCallback(ImageTimer), this, ShowArtistImages ? 5000 : Timeout.Infinite, ShowArtistImages ? 5000 : Timeout.Infinite);
                Stop = false;
                if (SpotifyPlayerProperties.Token == null)
                {
                    Bitmap bmp = Drawing.ImageHelper.GetErrorImage("Authenticating\nPlease check your browser");
                    SendImage(bmp);
                    bmp.Dispose();
                    Authenticate();
                }
                else if(SpotifyPlayerProperties.Token.IsExpired())
                {
                    RefreshToken();
                }
                else
                {
                    Init();
                }
                base.StartTimer();
            }
        }

        public override void StopTimer(int timeOut = 100)
        {
            if (IsRunning)
            {
                Timer = null;
                IsRunning = false;
                if (LazyLoader != null)
                {
                    Stop = true;
                    DateTime stopTime = DateTime.Now;
                    while (LazyLoader.IsRunning)
                    {
                        TimeSpan span = DateTime.Now - stopTime;
                        if (span.TotalMilliseconds > timeOut)
                        {
                            break;
                        }
                        Thread.Sleep(10);
                        if (LazyLoader == null)
                        {
                            break;
                        }
                    }
                    if (LazyLoader != null && LazyLoader.IsRunning)
                    {
                        LazyLoader.Abort();
                    }
                }
                
                base.StopTimer(timeOut);
            }
        }

        public override bool Reload 
        { 
            get => base.Reload;
            set
            {
                base.Reload = value;
                if(CurrentPage == SpotifyPlayerPage.Player && Reload)
                {
                    UpdatePlayer();
                }
                base.Reload = false;
            }
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused)
                {
                    if (CurrentPage == SpotifyPlayerPage.Playlist)
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Left:
                            case SoftButtons.Down:
                                PlayListIndex = Math.Max(0, PlayListIndex - 1);
                                UpdatePlayList();
                                break;
                            case SoftButtons.Right:
                            case SoftButtons.Up:
                                PlayListIndex = Math.Min(PlayLists.Count - 1, PlayListIndex + 1);
                                UpdatePlayList();
                                break;
                            case SoftButtons.Button1:
                                LoadPlayList(PlayLists[PlayListIndex]);
                                break;
                            case SoftButtons.Button2:
                                LoadPlayList(null);
                                break;
                        }
                    }
                    else
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button1:
                                SpotifyController.Mute = !SpotifyController.Mute;
                                UpdatePlayer();
                                break;
                            case SoftButtons.Button2:
                                SpotifyController.PlayPause();
                                break;
                            case SoftButtons.Button3:
                                SpotifyController.ShuffleState = !SpotifyController.ShuffleState;
                                UpdatePlayer();
                                break;
                            case SoftButtons.Button4:
                                switch (SpotifyController.RepeatState)
                                {
                                    case RepeatState.Track:
                                        SpotifyController.RepeatState = RepeatState.Context;
                                        break;
                                    case RepeatState.Context:
                                        SpotifyController.RepeatState = RepeatState.Off;
                                        break;
                                    case RepeatState.Off:
                                        SpotifyController.RepeatState = RepeatState.Track;
                                        break;
                                }
                                UpdatePlayer();
                                break;
                            case SoftButtons.Button5:
                                if (SpotifyController.IsLiked(CurrentPlaybackContext.Item.Id))
                                {
                                    SpotifyController.UnLike(CurrentPlaybackContext.Item.Id);
                                }
                                else
                                {
                                    SpotifyController.Like(CurrentPlaybackContext.Item.Id);
                                }
                                UpdatePlayer();
                                break;
                            case SoftButtons.Button6:
                                CurrentPage = SpotifyPlayerPage.Playlist;
                                SetLEDs();
                                LoadPlayLists();
                                UpdatePlayList();
                                FireStateChanged();
                                break;
                            case SoftButtons.Down:
                                SpotifyController.VolumeDown();
                                break;
                            case SoftButtons.Up:
                                SpotifyController.VolumeUp();
                                break;
                            case SoftButtons.Left:
                                SpotifyController.PlayPrev();
                                break;
                            case SoftButtons.Right:
                                SpotifyController.PlayNext();
                                break;
                        }
                    }
                }
                FireSoftButtonNotifcation(softButton);
            });
        }

        public bool CanPlay()
        {
            FIPCanPlayEventArgs canPlay = new FIPCanPlayEventArgs(this);
            OnCanPlay?.Invoke(this, canPlay);
            if (!canPlay.CanPlay)
            {
                resume = true;
            }
            return canPlay.CanPlay;
        }

        private bool resume = false;
        public void ExternalPause()
        {
            if (SpotifyController != null && SpotifyController.SpotifyState == SpotifyStateType.Playing && !resume)
            {
                resume = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (SpotifyController != null && SpotifyController.SpotifyState == SpotifyStateType.Playing)
                    {
                        SpotifyController.PlayPause();
                        UpdatePage();
                    }
                });
            }
        }

        public void ExternalResume()
        {
            if (SpotifyController != null && SpotifyController.SpotifyState == SpotifyStateType.Paused && CanPlay() && resume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (SpotifyController != null && SpotifyController.SpotifyState == SpotifyStateType.Paused && CanPlay())
                    {
                        SpotifyController.PlayPause();
                        UpdatePage();
                    }
                });
            }
            resume = false;
        }

        private void LoadPlayList(SpotifyPlaylist playList)
        {
            CurrentPage = SpotifyPlayerPage.Player;
            if(playList != null)
            {
                SpotifyPlayerProperties.Playlist = new FIPPlayList()
                {
                    UserId = playList.SimplePlaylist.Owner.Id,
                    PlaylistId = playList.SimplePlaylist.Id
                };
                SpotifyController.PlayUserPlaylist(playList.SimplePlaylist.Owner.Id, playList.SimplePlaylist.Id);
            }
            SetLEDs();
            UpdatePlayer();
            FireStateChanged();
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            switch (CurrentPage)
            {
                case SpotifyPlayerPage.Player:
                    return (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused);
                case SpotifyPlayerPage.Playlist:
                    return ((softButton == SoftButtons.Button1 || softButton == SoftButtons.Button2) && (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused));
            }
            return false;
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            return false;
        }

        protected override void ButtonChanged()
        {
            UpdatePlayList();
            base.ButtonChanged();
        }

        public void UpdateArtwork()
        {
            if (CurrentPage == SpotifyPlayerPage.Player && SpotifyController != null && (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused) && SpotifyController.AlbumArtwork.Count > 0)
            {
                try
                {
                    using (Graphics graphics = Graphics.FromImage(Image))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            int titleHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, SpotifyPlayerProperties.Font, 288, format).Height;
                            int artistHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, SpotifyPlayerProperties.ArtistFont, 288, format).Height;
                            int maxImageWidth = 320 - 34;
                            int maxImageHeight = 240 - (titleHeight + artistHeight);
                            //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                            double ratioX = (double)maxImageWidth / SpotifyController.AlbumArtwork[ImageIndex].Width;
                            double ratioY = (double)maxImageHeight / SpotifyController.AlbumArtwork[ImageIndex].Height;
                            double ratio = Math.Min(ratioX, ratioY);
                            int imageWidth = (int)(SpotifyController.AlbumArtwork[ImageIndex].Width * ratio);
                            int imageHeight = (int)(SpotifyController.AlbumArtwork[ImageIndex].Height * ratio);
                            graphics.FillRectangle(Brushes.Black, new Rectangle(32, 0, 288, maxImageHeight));
                            Rectangle destRect = new Rectangle(17 + ((320 - imageWidth) / 2), (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                            graphics.DrawImage(SpotifyController.AlbumArtwork[ImageIndex], destRect, 0, 0, SpotifyController.AlbumArtwork[ImageIndex].Width, SpotifyController.AlbumArtwork[ImageIndex].Height, GraphicsUnit.Pixel);
                        }
                    }
                    if (Device != null && Device.CurrentPage != null && SpotifyPlayerProperties.Page == Device.CurrentPage.Properties.Page)
                    {
                        try
                        {
                            Device.DeviceClient.SetImage(SpotifyPlayerProperties.Page, 0, Image.ImageToByte());
                        }
                        catch
                        {
                        }
                    }
                    FireImageChanged();
                }
                catch
                {
                }
            }
        }

        private void UpdatePlayer()
        {
            if (CurrentPage == SpotifyPlayerPage.Player)
            {
                try
                {
                    Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        using (SolidBrush brush = new SolidBrush(SpotifyPlayerProperties.FontColor))
                        {
                            graphics.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                            using (StringFormat format = new StringFormat())
                            {
                                format.Alignment = StringAlignment.Center;
                                format.LineAlignment = StringAlignment.Center;
                                if (SpotifyController != null && (SpotifyController.SpotifyState == SpotifyStateType.Error || SpotifyController.RetryAfter > 0))
                                {
                                    string errorMessage = SpotifyController.Error;
                                    if (SpotifyController.RetryAfter > 0)
                                    {
                                        TimeSpan t = TimeSpan.FromSeconds(SpotifyController.RetryAfter);
                                        string time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
                                        errorMessage += String.Format("\nRetrying in {0}", time);
                                    }
                                    graphics.DrawString(errorMessage, SpotifyPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                                else if (SpotifyController != null && (CurrentPlaybackContext == null || SpotifyController.SpotifyState == SpotifyStateType.Closed))
                                {
                                    graphics.DrawString("Start Spotify on your Mobile Device or PC", SpotifyPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                                else if (SpotifyController != null)
                                {
                                    if (SpotifyController.AlbumArtwork.Count > 0)
                                    {
                                        int titleHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, SpotifyPlayerProperties.Font, 288, format).Height;
                                        int artistHeight = (int)graphics.MeasureString(SpotifyController.TrackArtist, SpotifyPlayerProperties.ArtistFont, 288, format).Height;
                                        int maxImageWidth = 320 - 34;
                                        int maxImageHeight = 240 - (titleHeight + artistHeight);
                                        //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                                        double ratioX = (double)maxImageWidth / SpotifyController.AlbumArtwork[ImageIndex].Width;
                                        double ratioY = (double)maxImageHeight / SpotifyController.AlbumArtwork[ImageIndex].Height;
                                        double ratio = Math.Min(ratioX, ratioY);
                                        int imageWidth = (int)(SpotifyController.AlbumArtwork[ImageIndex].Width * ratio);
                                        int imageHeight = (int)(SpotifyController.AlbumArtwork[ImageIndex].Height * ratio);
                                        Rectangle destRect = new Rectangle(17 + ((320 - imageWidth) / 2), (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                                        graphics.DrawImage(SpotifyController.AlbumArtwork[ImageIndex], destRect, 0, 0, SpotifyController.AlbumArtwork[ImageIndex].Width, SpotifyController.AlbumArtwork[ImageIndex].Height, GraphicsUnit.Pixel);
                                        graphics.DrawString(SpotifyController.TrackTitle, SpotifyPlayerProperties.Font, brush, new RectangleF(32, maxImageHeight, 288, titleHeight), format);
                                        graphics.DrawString(SpotifyController.TrackArtist, SpotifyPlayerProperties.ArtistFont, brush, new RectangleF(32, maxImageHeight + titleHeight, 288, artistHeight), format);
                                    }
                                    else
                                    {
                                        string text = string.Format("{0}\n{1}", SpotifyController.TrackTitle, SpotifyController.TrackArtist);
                                        graphics.DrawString(text, SpotifyPlayerProperties.Font, brush, new RectangleF(32, 0, 288, 240), format);
                                    }
                                }
                                else
                                {
                                    graphics.DrawString("Initializing", SpotifyPlayerProperties.Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                            }
                            if (SpotifyController != null && (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused))
                            {
                                graphics.AddButtonIcon(SpotifyController.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, SpotifyController.Mute ? Color.Red : Color.Green, true, SoftButtons.Button1);
                                graphics.AddButtonIcon(SpotifyController.SpotifyState == SpotifyStateType.Playing ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, SpotifyController.SpotifyState == SpotifyStateType.Playing ? Color.Yellow : Color.Blue, true, SoftButtons.Button2);
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.shuffle, SpotifyController.ShuffleState == true ? Color.Green : Color.White, true, SoftButtons.Button3);
                                graphics.AddButtonIcon(SpotifyController.RepeatState == RepeatState.Track ? FIPToolKit.Properties.Resources.repeat_one : FIPToolKit.Properties.Resources.repeat, SpotifyController.RepeatState != RepeatState.Off ? Color.Green : Color.White, true, SoftButtons.Button4);
                                graphics.AddButtonIcon(CurrentPlaybackContext != null && CurrentPlaybackContext.Item != null && SpotifyController.IsLiked(CurrentPlaybackContext.Item.Id) ? FIPToolKit.Properties.Resources.heart : FIPToolKit.Properties.Resources.heart_outline, Color.Pink, true, SoftButtons.Button5);
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
        }

        private void UpdatePlayList()
        {
            if (PlayListIndex != -1 && CurrentPage == SpotifyPlayerPage.Playlist)
            {
                try
                {
                    Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        using (SolidBrush brush = new SolidBrush(SpotifyPlayerProperties.FontColor))
                        {
                            using (StringFormat format = new StringFormat())
                            {
                                format.FormatFlags = StringFormatFlags.NoWrap;
                                format.LineAlignment = StringAlignment.Center;
                                format.Trimming = StringTrimming.EllipsisCharacter;
                                int page = PlayListIndex / 8;
                                Rectangle destRect;
                                //Draw playlists for currenttly displayed page
                                for (int i = 0; i < 8; i++)
                                {
                                    if (i + (page * 8) >= PlayLists.Count)
                                    {
                                        break;
                                    }
                                    SpotifyPlaylist spotifyPlaylist = PlayLists[i + (page * 8)];
                                    if (spotifyPlaylist == PlayLists[PlayListIndex])
                                    {
                                        graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(34, (40 * (i + 1)), 288, 40));
                                    }
                                    if (spotifyPlaylist.AlbumArtwork != null)
                                    {
                                        destRect = new Rectangle(34, (30 * i) + 1, 28, 28);
                                        graphics.DrawImage(spotifyPlaylist.AlbumArtwork, destRect, 0, 0, spotifyPlaylist.AlbumArtwork.Width, spotifyPlaylist.AlbumArtwork.Height, GraphicsUnit.Pixel);
                                    }
                                    destRect = new Rectangle(64, (30 * i), 256, 30);
                                    using (SolidBrush brush2 = new SolidBrush(spotifyPlaylist == PlayLists[PlayListIndex] ? SystemColors.HighlightText : SpotifyPlayerProperties.FontColor.Color))
                                    {
                                        graphics.DrawString(spotifyPlaylist.SimplePlaylist.Name, SpotifyPlayerProperties.Font, brush2, destRect, format);
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
        }

        private void LazyLoader_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Stop = false;
            PrivateProfile profile = SpotifyAPI.GetPrivateProfile();
            //Currently displayed page
            int page = PlayListIndex / 8;
            bool currentPageLoaded = false;
            List<string> tempCache = new List<string>();
            Paging<SimplePlaylist> playLists = SpotifyAPI.GetUserPlaylists(profile.Id, 50);
            if (playLists != null)
            {
                do
                {
                    playLists.Items.ForEach(playList =>
                    {
                        tempCache.Add(playList.Id);
                        if (!PlayLists.Any(p => p.SimplePlaylist.Id.Equals(playList.Id, StringComparison.Ordinal)))
                        {
                            SpotifyPlaylist spotifyPlayList = new SpotifyPlaylist() { SimplePlaylist = playList, Index = PlayLists.Count, Page = (int)(PlayLists.Count / 8) };
                            PlayLists.Add(spotifyPlayList);
                            if (PlayListIndex == -1 && PlayLists.Count > 0)
                            {
                                //Select the first playlist
                                PlayListIndex = 0;
                                page = PlayListIndex / 8;
                            }
                            if (spotifyPlayList.Page == page)
                            {
                                //Currently viewing this page so let's refresh it
                                UpdatePlayList();
                            }
                            else if ((int)(spotifyPlayList.Index / 8) > page && !currentPageLoaded)
                            {
                                //We've shown the text for the current page. Let's download all the images for this current page and display them as well.
                                currentPageLoaded = true;
                                int index = page * 8;
                                using (WebClient client = new WebClient())
                                {
                                    for (int i = index; i < Math.Min(PlayLists.Count, index + 8); i++)
                                    {
                                        if (spotifyPlayList.AlbumArtwork == null)
                                        {
                                            spotifyPlayList = PlayLists[i];
                                            if (spotifyPlayList.SimplePlaylist.Images.Count > 0)
                                            {
                                                try
                                                {
                                                    using (Stream stream = client.OpenRead(spotifyPlayList.SimplePlaylist.Images[0].Url))
                                                    {
                                                        using (Bitmap bitmap = new Bitmap(stream))
                                                        {
                                                            stream.Flush();
                                                            stream.Close();
                                                            spotifyPlayList.AlbumArtwork = new Bitmap(bitmap);
                                                            UpdatePlayList();
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
                    });
                    if (!playLists.HasNextPage())
                    {
                        break;
                    }
                    playLists = SpotifyAPI.GetNextPage(playLists);
                }
                while (!Stop);
                //Remove any playlists that the user has removed from Spotify
                List<SpotifyPlaylist> playListsToDelete = new List<SpotifyPlaylist>();
                foreach(SpotifyPlaylist playList in PlayLists)
                {
                    if(!tempCache.Contains(playList.SimplePlaylist.Id))
                    {
                        playListsToDelete.Add(playList);
                    }
                }
                foreach(SpotifyPlaylist playList in playListsToDelete)
                {
                    PlayLists.Remove(playList);
                }
                //Sort the playlist
                for(int i = 0; i < PlayLists.Count; i++)
                {
                    SpotifyPlaylist playList = PlayLists[i];
                    //Set the sort order in the order that we retrieved them
                    playList.SortOrder = tempCache.IndexOf(playList.SimplePlaylist.Id);
                }
                //Do the sorting heavy lifting here
                PlayLists.Sort(delegate (SpotifyPlaylist playList1, SpotifyPlaylist playList2)
                {
                    return playList1.SortOrder.CompareTo(playList2.SortOrder);
                });
                //Update the new index
                foreach (SpotifyPlaylist playList in PlayLists)
                {
                    playList.Index = playList.SortOrder;
                    playList.Page = (int)(playList.Index / 8);
                }
                UpdatePlayList();
            }
            //Finish downloading the remaining icons.
            using (WebClient client = new WebClient())
            {
                foreach(SpotifyPlaylist playList in PlayLists)
                //for (int i = 0; i < PlayLists.Count; i++)
                {
                    if (Stop)
                    {
                        break;
                    }
                    //SpotifyPlaylist playList = PlayLists[i];
                    if (playList.AlbumArtwork == null)
                    {
                        try
                        {
                            if (playList.SimplePlaylist.Images.Count > 0)
                            {
                                using (Stream stream = client.OpenRead(playList.SimplePlaylist.Images[0].Url))
                                {
                                    using (Bitmap bitmap = new Bitmap(stream))
                                    {
                                        stream.Flush();
                                        stream.Close();
                                        playList.AlbumArtwork = new Bitmap(bitmap);
                                        //Is this artwork on the currently displayed page?
                                        if (playList.Page == page)
                                        {
                                            //Yep! Let's update the display
                                            UpdatePlayList();
                                        }
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

        private void Init()
        {
            if (SpotifyPlayerProperties.Playlist != null && !string.IsNullOrEmpty(SpotifyPlayerProperties.Playlist.PlaylistId) && !String.IsNullOrEmpty(SpotifyPlayerProperties.Playlist.UserId) && AutoPlayLastPlaylist)
            {
                SpotifyController.PlayUserPlaylist(SpotifyPlayerProperties.Playlist.PlaylistId, SpotifyPlayerProperties.Playlist.PlaylistId);
            }
            UpdatePlayer();
        }

        private void LoadPlayLists(int timeOut = 100)
        {
            if (LazyLoader != null)
            {
                Stop = true;
                DateTime stopTime = DateTime.Now;
                while (LazyLoader.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (LazyLoader.IsRunning)
                {
                    LazyLoader.Abort();
                }
            }
            LazyLoader = new AbortableBackgroundWorker();
            LazyLoader.DoWork += LazyLoader_DoWork;
            LazyLoader.RunWorkerAsync();
        }

        public override void Dispose()
        {
            if (Timer != null)
            {
                Timer.Change(Timeout.Infinite, Timeout.Infinite);
                Timer = null;
            }
            StopTimer();
            base.Dispose();
        }

        public bool Mute
        {
            get
            {
                return SpotifyPlayerProperties.Mute;
            }
            set
            {
                SpotifyPlayerProperties.Mute = value;
            }
        }

        public int Volume
        {
            get
            {
                return SpotifyPlayerProperties.Volume;
            }
            set
            {
                SpotifyPlayerProperties.Volume = value;
            }
        }

        public override void Inactive(bool sendEvent)
        {
            base.Inactive(false);
        }

        public override void Active(bool sendEvent)
        {
            base.Active(false);
        }

        public bool CanPlayOther
        {
            get
            {
                return !(SpotifyPlayerProperties.PauseOtherMedia && SpotifyController != null && SpotifyController.IsPlaying);
            }
        }
    }
}
