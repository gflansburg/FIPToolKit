using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Serialization;
using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace FIPToolKit.Models
{
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

    [Serializable]
    public class FIPSpotifyPlayer : FIPPage
    {
        private Token _token;

        [XmlIgnore]
        [JsonIgnore]
        public bool IsAuthenticating { get; set; } = false;
        
        private AuthorizationCodeAuth authorizationCodeAuth;
        private bool _showArtistImages;
        private bool _cacheArtwork;
        private System.Threading.Timer Timer;

        [XmlIgnore]
        [JsonIgnore]
        public bool IsConfigured
        {
            get
            {
                return !string.IsNullOrEmpty(ClientId) && !string.IsNullOrEmpty(SecretId);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsAuthorized
        {
            get
            {
                return Token != null && !string.IsNullOrEmpty(Token.AccessToken) && !Token.IsExpired();
            }
        }

        private FIPPlayList _playList;
        public FIPPlayList Playlist 
        { 
            get
            {
                return _playList;
            }
            set
            {
                if(!(_playList.PlaylistId ?? string.Empty).Equals(value.PlaylistId ?? string.Empty, StringComparison.OrdinalIgnoreCase) || !(_playList.UserId ?? string.Empty).Equals(value.UserId ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _playList = value;
                    IsDirty = true;
                }
            }
        }

        private Microsoft.Web.WebView2.WinForms.WebView2 _browser;
        [XmlIgnore]
        [JsonIgnore]
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

        [XmlIgnore]
        [JsonIgnore]
        public SpotifyWebAPI SpotifyAPI { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool AutoPlayLastPlaylist { get; set; }

        public Token Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
                IsDirty = true;
                if (_token != null)
                {
                    if (_token.IsExpired())
                    {
                        RefreshToken();
                    }
                    else
                    {
                        if (SpotifyAPI == null)
                        {
                            SpotifyAPI = new SpotifyWebAPI
                            {
                                AccessToken = _token.AccessToken,
                                TokenType = _token.TokenType
                            };
                        }
                        else
                        {
                            SpotifyAPI.AccessToken = _token.AccessToken;
                            SpotifyAPI.TokenType = _token.TokenType;
                        }
                        if (SpotifyController != null)
                        {
                            SpotifyController.SpotifyWebAPI = SpotifyAPI;
                        }
                        OnTokenChanged?.Invoke(_token);
                    }
                } 
                else
                {
                    SpotifyAPI = null;
                    if (SpotifyController != null)
                    {
                        SpotifyController.SpotifyWebAPI = null;
                    }
                    Authenticate();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public int PlayListIndex { get; set; }

        [XmlIgnore]
        [JsonIgnore]
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
                    ShowArtistImagesChanged?.Invoke();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
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

        [XmlIgnore]
        [JsonIgnore]
        public List<SpotifyPlaylist> PlayLists { get; private set; }

        private AbortableBackgroundWorker LazyLoader { get; set; }
        private bool Stop { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsRunning { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public SpotifyPlayerPage CurrentPage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public PlaybackContext CurrentPlaybackContext { get; set; }

        private SpotifyController SpotifyController { get; set; }

        private string ErrorMessage { get; set; }

        private int ImageIndex { get;set; }

        private string _clientId = string.Empty;
        public string ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                if (!(_clientId ?? string.Empty).Equals(value ?? string.Empty))
                {
                    _clientId = value;
                    IsDirty = true;
                }
            }
        }

        private string _secretId = string.Empty;
        public string SecretId
        {
            get
            {
                return _secretId;
            }
            set
            {
                if (!(_secretId ?? string.Empty).Equals(value ?? string.Empty))
                {
                    _secretId = value;
                    IsDirty = true;
                }
            }
        }

        private FontEx _artistFont;
        public FontEx ArtistFont
        { 
            get
            {
                return _artistFont;
            }
            set
            {
                if (!_artistFont.FontFamily.Name.Equals(value.FontFamily.Name, StringComparison.OrdinalIgnoreCase) || _artistFont.Size != value.Size || _artistFont.Style != value.Style || _artistFont.Strikeout != value.Strikeout || _artistFont.Underline != value.Underline || _artistFont.Unit != value.Unit || _artistFont.GdiCharSet != value.GdiCharSet)
                {
                    _artistFont = value;
                    IsDirty = true;
                }
            }
        }

        public event TrackStateChangedEventHandler OnTrackStateChanged;
        public delegate void TrackStateChangedEventHandler(PlaybackContext playback, SpotifyStateType state);
        public event TokenChangedEventHandler OnTokenChanged;
        public delegate void TokenChangedEventHandler(Token token);
        public event FIPPageEventHandler OnBeginAuthentication;
        public event FIPPageEventHandler OnEndAuthentication;

        private event ShowArtistImagesChangedEventHandler ShowArtistImagesChanged;
        private delegate void ShowArtistImagesChangedEventHandler();

        public FIPSpotifyPlayer() : base()
        {
            Name = "Spotify Player";
            if (PlayLists == null)
            {
                PlayLists = new List<SpotifyPlaylist>();
            }
            if (SpotifyController == null)
            {
                SpotifyController = new SpotifyController();
                SpotifyController.SpotifyWebAPI = SpotifyAPI;
                SpotifyController.AddArtistImages = ShowArtistImages;
            }
            SpotifyController.TrackStateChanged += SpotifyController_TrackStateChanged;
            SpotifyController.ImageStateChanged += SpotifyController_ImageStateChanged;
            SpotifyController.OnError += SpotifyController_OnError;
            Font = new Font("Arial", 12.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _artistFont = new Font("Arial", 10.0F, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(0)));
            FontColor = Color.White;
            PlayListIndex = -1;
            ShowArtistImagesChanged += FIPSpotifyController_ShowArtistImagesChanged;
            _playList = new FIPPlayList();
            IsDirty = false;
            UpdatePage();
        }

        private void InitAuthServer()
        {
            if (!string.IsNullOrEmpty(_clientId) && !string.IsNullOrEmpty(_secretId))
            {
                authorizationCodeAuth = new AuthorizationCodeAuth(_clientId, _secretId, "http://localhost:51400", "http://localhost:51400", Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative | Scope.UserReadCurrentlyPlaying | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState | Scope.UserLibraryRead | Scope.UserLibraryModify);
                authorizationCodeAuth.Browser = Browser;
                authorizationCodeAuth.AuthReceived += AuthOnAuthReceived;
                authorizationCodeAuth.AuthReceived += LoadController;
            }
            else
            {
                authorizationCodeAuth = null;
            }
        }

        private void FIPSpotifyController_ShowArtistImagesChanged()
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
            OnTrackStateChanged?.Invoke(playbackContext, state);
        }

        private void SpotifyController_OnError(string message)
        {
            CurrentPage = SpotifyPlayerPage.Player;
            if (_token != null && _token.IsExpired())
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
            if (IsActive && (Token == null || Token.IsExpired()) && !IsAuthenticating && authorizationCodeAuth != null)
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
                if (Token != null && Token.IsExpired() && !string.IsNullOrEmpty(Token.RefreshToken) && !IsAuthenticating)
                {
                    IsAuthenticating = true;
                    Token = await authorizationCodeAuth.RefreshToken(Token.RefreshToken);
                    if (Token != null)
                    {
                        if (SpotifyAPI == null)
                        {
                            SpotifyAPI = new SpotifyWebAPI
                            {
                                AccessToken = Token.AccessToken,
                                TokenType = Token.TokenType
                            };
                        }
                        else
                        {
                            SpotifyAPI.AccessToken = Token.AccessToken;
                            SpotifyAPI.TokenType = Token.TokenType;
                        }
                    }
                    IsAuthenticating = false;
                }
                else if (Token == null || Token.IsExpired())
                {
                    Authenticate();
                }
            }
        }

        private async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            authorizationCodeAuth.Stop();
            Token = await authorizationCodeAuth.ExchangeCode(payload.Code);
            if (Token != null)
            {
                if (SpotifyAPI == null)
                {
                    SpotifyAPI = new SpotifyWebAPI
                    {
                        AccessToken = Token.AccessToken,
                        TokenType = Token.TokenType
                    };
                }
                else
                {
                    SpotifyAPI.AccessToken = Token.AccessToken;
                    SpotifyAPI.TokenType = Token.TokenType;
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
                if (Token == null)
                {
                    Bitmap bmp = Drawing.ImageHelper.GetErrorImage("Authenticating\nPlease check your browser");
                    SendImage(bmp);
                    bmp.Dispose();
                    Authenticate();
                }
                else if(Token.IsExpired())
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

        [XmlIgnore]
        [JsonIgnore]
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

        private void LoadPlayList(SpotifyPlaylist playList)
        {
            CurrentPage = SpotifyPlayerPage.Player;
            if(playList != null)
            {
                Playlist = new FIPPlayList()
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
                            int titleHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, Font, 288, format).Height;
                            int artistHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, ArtistFont, 288, format).Height;
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
                    if (Device != null && Device.CurrentPage != null && Page == Device.CurrentPage.Page)
                    {
                        try
                        {
                            Device.DeviceClient.SetImage(Page, 0, Image.ImageToByte());
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
                        using (SolidBrush brush = new SolidBrush(FontColor))
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
                                    graphics.DrawString(errorMessage, Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                                else if (SpotifyController != null && (CurrentPlaybackContext == null || SpotifyController.SpotifyState == SpotifyStateType.Closed))
                                {
                                    graphics.DrawString("Start Spotify on your Mobile Device or PC", Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                                else if (SpotifyController != null)
                                {
                                    if (SpotifyController.AlbumArtwork.Count > 0)
                                    {
                                        int titleHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, Font, 288, format).Height;
                                        int artistHeight = (int)graphics.MeasureString(SpotifyController.TrackTitle, ArtistFont, 288, format).Height;
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
                                        graphics.DrawString(SpotifyController.TrackTitle, Font, brush, new RectangleF(32, maxImageHeight, 288, titleHeight), format);
                                        graphics.DrawString(SpotifyController.TrackArtist, ArtistFont, brush, new RectangleF(32, maxImageHeight + titleHeight, 288, artistHeight), format);
                                    }
                                    else
                                    {
                                        string text = string.Format("{0}\n{1}", SpotifyController.TrackTitle, SpotifyController.TrackArtist);
                                        graphics.DrawString(text, Font, brush, new RectangleF(32, 0, 288, 240), format);
                                    }
                                }
                                else
                                {
                                    graphics.DrawString("Initializing", Font, brush, new RectangleF(0, 0, 320, 240), format);
                                }
                            }
                            if (SpotifyController != null && (SpotifyController.SpotifyState == SpotifyStateType.Playing || SpotifyController.SpotifyState == SpotifyStateType.Paused))
                            {
                                graphics.AddButtonIcon(SpotifyController.Mute ? Properties.Resources.media_mute : Properties.Resources.media_volumeup, SpotifyController.Mute ? Color.Red : Color.Green, true, SoftButtons.Button1);
                                graphics.AddButtonIcon(SpotifyController.SpotifyState == SpotifyStateType.Playing ? Properties.Resources.pause : Properties.Resources.play, SpotifyController.SpotifyState == SpotifyStateType.Playing ? Color.Yellow : Color.Blue, true, SoftButtons.Button2);
                                graphics.AddButtonIcon(Properties.Resources.shuffle, SpotifyController.ShuffleState == true ? Color.Green : Color.White, true, SoftButtons.Button3);
                                graphics.AddButtonIcon(SpotifyController.RepeatState == RepeatState.Track ? Properties.Resources.repeat_one : Properties.Resources.repeat, SpotifyController.RepeatState != RepeatState.Off ? Color.Green : Color.White, true, SoftButtons.Button4);
                                graphics.AddButtonIcon(CurrentPlaybackContext != null && CurrentPlaybackContext.Item != null && SpotifyController.IsLiked(CurrentPlaybackContext.Item.Id) ? Properties.Resources.heart : Properties.Resources.heart_outline, Color.Pink, true, SoftButtons.Button5);
                                graphics.AddButtonIcon(Properties.Resources.playlist, Color.Orange, true, SoftButtons.Button6);
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
                        using (SolidBrush brush = new SolidBrush(FontColor))
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
                                    if (spotifyPlaylist.AlbumArtwork != null)
                                    {
                                        destRect = new Rectangle(34, (30 * i) + 1, 28, 28);
                                        graphics.DrawImage(spotifyPlaylist.AlbumArtwork, destRect, 0, 0, spotifyPlaylist.AlbumArtwork.Width, spotifyPlaylist.AlbumArtwork.Height, GraphicsUnit.Pixel);
                                    }
                                    destRect = new Rectangle(64, (30 * i), 256, 30);
                                    graphics.DrawString(spotifyPlaylist.SimplePlaylist.Name, Font, brush, destRect, format);
                                }
                                //Draw currently selectd playlist
                                SpotifyPlaylist playList = PlayLists[PlayListIndex];
                                int selectOffset = (PlayListIndex % 8) * 30;
                                destRect = new Rectangle(32, selectOffset, 288, 30);
                                graphics.FillRectangle(SystemBrushes.Highlight, destRect);
                                destRect = new Rectangle(64, selectOffset, 256, 30);
                                graphics.DrawString(playList.SimplePlaylist.Name, Font, brush, destRect, format);
                                if (playList.AlbumArtwork != null)
                                {
                                    destRect = new Rectangle(34, selectOffset + 1, 28, 28);
                                    graphics.DrawImage(playList.AlbumArtwork, destRect, 0, 0, playList.AlbumArtwork.Width, playList.AlbumArtwork.Height, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        graphics.AddButtonIcon(Properties.Resources.checkmark, Color.Green, true, SoftButtons.Button1);
                        graphics.AddButtonIcon(Properties.Resources.cancel, Color.Red, true, SoftButtons.Button2);
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
            if (Playlist != null && !String.IsNullOrEmpty(Playlist.PlaylistId) && !String.IsNullOrEmpty(Playlist.UserId) && AutoPlayLastPlaylist)
            {
                SpotifyController.PlayUserPlaylist(Playlist.PlaylistId, Playlist.PlaylistId);
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
    }
}
