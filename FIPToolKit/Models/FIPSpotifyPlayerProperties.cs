using FIPToolKit.Drawing;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPSpotifyPlayerProperties : FIPPageProperties
    {
        public event EventHandler OnTokenExpired;
        public event EventHandler OnTokenChanged;
        public event EventHandler OnTokenCleared;

        public FIPSpotifyPlayerProperties() : base()
        {
            Name = "Spotify Player";
            Font = new Font("Arial", 12.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _artistFont = new Font("Arial", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            FontColor = Color.White;
            _playList = new FIPPlayList();
            IsDirty = false;
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
                if (!(_playList.PlaylistId ?? string.Empty).Equals(value.PlaylistId ?? string.Empty, StringComparison.OrdinalIgnoreCase) || !(_playList.UserId ?? string.Empty).Equals(value.UserId ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _playList = value;
                    IsDirty = true;
                }
            }
        }

        private Token _token;
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
                        OnTokenExpired?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        OnTokenChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    OnTokenCleared?.Invoke(this, EventArgs.Empty);
                }
            }
        }

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
    }
}
