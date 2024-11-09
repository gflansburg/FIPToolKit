using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace GMap.NET.MapProviders
{
    /// <summary>
    ///     GoogleHybridMap provider
    /// </summary>
    public class GoogleHybridMapProvider2 : GoogleMapProviderBase, CacheManager, OverlayProvider
    {
        public static readonly GoogleHybridMapProvider2 Instance;
        private Timer cacheTimer = null;
        public delegate void CacheUpdated(GMapProvider provider);
        public event CacheUpdated OnCacheUpdated;

        GoogleHybridMapProvider2()
        {
            cacheTimer = new Timer(RefreshCache, null, 0, 3600000);
        }

        static GoogleHybridMapProvider2()
        {
            Instance = new GoogleHybridMapProvider2();
        }

        public string Version = "h@333000000";

        #region GMapProvider Members

        public override Guid Id
        {
            get;
        } = new Guid("6C554CE0-89C6-438B-AC8D-1CDCAADF919A");

        public override string Name
        {
            get;
        } = "GoogleHybridMap";

        GMapProvider[] _overlays;

        public override GMapProvider[] Overlays
        {
            get
            {
                if (_overlays == null)
                {
                    RefreshOverlays();
                }
                return _overlays;
            }
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);

            return GetTileImageUsingHttp(url);
        }

        #endregion

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            // sec1: after &x=...
            // sec2: after &zoom=...
            GetSecureWords(pos, out string sec1, out string sec2);

            return string.Format(UrlFormat,
                UrlFormatServer,
                GetServerNum(pos, 4),
                UrlFormatRequest,
                Version,
                language,
                pos.X,
                sec1,
                pos.Y,
                zoom,
                sec2,
                Server);
        }

        public void RefreshCache(object state)
        {
            // Delete expired titles
            string db = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + System.IO.Path.DirectorySeparatorChar + "GMap.NET" + System.IO.Path.DirectorySeparatorChar + "TileDBv5" + System.IO.Path.DirectorySeparatorChar + LanguageStr + System.IO.Path.DirectorySeparatorChar + "Data.gmdb";
            string connectionString = string.Format("Data Source=\"{0}\";Page Size=32768;Pooling=True", db);
            if (FIPToolKit.Tools.Net.DeleteTilesOlderThan(connectionString, DateTime.Now - new TimeSpan(TTLCache, 0, 0), DbId) > 0)
            {
                OnCacheUpdated?.Invoke(this);
            }
        }

        public void InitializeWebRequest2(WebRequest request)
        {
            InitializeWebRequest(request);
        }

        public void RefreshOverlays()
        {
            List<GMapProvider> overlays = new List<GMapProvider>()
            {
                ArcGIS_World_Imagery_MapProvider.Instance,
                this
            };
            if (!string.IsNullOrEmpty(OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken))
            {
                overlays.Add(OpenAIP_All_MapProvider.Instance);
            }
            _overlays = overlays.ToArray();
        }

        public void AddOverlay(GMapProvider overlay)
        {
            List<GMapProvider> overlays = _overlays.ToList();
            if (!overlays.Contains(overlay))
            {
                overlays.Add(overlay);
                _overlays = overlays.ToArray();
            }
        }

        public void RemoveOverlay(GMapProvider overlay)
        {
            List<GMapProvider> overlays = _overlays.ToList();
            if (overlays.Contains(overlay))
            {
                overlays.Remove(overlay);
                _overlays = overlays.ToArray();
            }
        }

        internal void GetSecureWords(GPoint pos, out string sec1, out string sec2)
        {
            sec1 = string.Empty; // after &x=...
            sec2 = string.Empty; // after &zoom=...
            int seclen = (int)(pos.X * 3 + pos.Y) % 8;
            sec2 = SecureWord.Substring(0, seclen);

            if (pos.Y >= 10000 && pos.Y < 100000)
            {
                sec1 = Sec1;
            }
        }

        static readonly string Sec1 = "&s=";
        static readonly string UrlFormatServer = "mt";
        static readonly string UrlFormatRequest = "vt";
        static readonly string UrlFormat = "http://{0}{1}.{10}/maps/{2}/lyrs={3}&hl={4}&x={5}{6}&y={7}&z={8}&s={9}";
    }
}
