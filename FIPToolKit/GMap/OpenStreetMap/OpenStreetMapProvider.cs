using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Xml;
using GMap.NET.Entity;
using GMap.NET.Internals;
using GMap.NET.Projections;
using Newtonsoft.Json;
using System.Threading;

namespace GMap.NET.MapProviders
{
    /// <summary>
    ///     OpenStreetMap provider - http://www.openstreetmap.org/
    /// </summary>
    public class OpenStreetMapProvider2 : OpenStreetMapProviderBase, CacheManager, OverlayProvider
    {
        public static readonly OpenStreetMapProvider2 Instance;
        private Timer cacheTimer = null;
        public delegate void CacheUpdated(GMapProvider provider);
        public event CacheUpdated OnCacheUpdated;

        OpenStreetMapProvider2()
        {
            RefererUrl = "https://www.openstreetmap.org/";
            Copyright = string.Format("© OpenStreetMap - Map data ©{0} OpenStreetMap", DateTime.Today.Year);
            cacheTimer = new Timer(RefreshCache, null, 0, 3600000);
        }

        static OpenStreetMapProvider2()
        {
            Instance = new OpenStreetMapProvider2();
        }

        #region GMapProvider Members

        public override Guid Id
        {
            get;
        } = new Guid("0521335C-92EC-47A8-98A5-6FD333DDA9C0");

        public override string Name
        {
            get;
        } = "OpenStreetMap";


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
            string url = MakeTileImageUrl(pos, zoom, string.Empty);
            //string url = MakeAlternateTileImageUrl(pos, zoom, string.Empty);
            try
            {
                return FIPToolKit.Tools.Net.GetTileImageUsingHttp(this, url);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            char letter = ServerLetters[GetServerNum(pos, 3)];
            return string.Format(UrlFormat, letter, zoom, pos.X, pos.Y);
        }

        string MakeAlternateTileImageUrl(GPoint pos, int zoom, string language)
        {
            return string.Format(AlternateUrlFormat, zoom, pos.X, pos.Y);
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

        public void RefreshOverlays()
        {
            List<GMapProvider> overlays = new List<GMapProvider>();
            overlays.Add(this);
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

        public void InitializeWebRequest2(WebRequest request)
        {
            InitializeWebRequest(request);
        }

        static readonly string UrlFormat = "https://{0}.tile.openstreetmap.de/{1}/{2}/{3}.png";
        static readonly string AlternateUrlFormat = "https://tile.openstreetmap.de/{0}/{1}/{2}.png";
    }
}
