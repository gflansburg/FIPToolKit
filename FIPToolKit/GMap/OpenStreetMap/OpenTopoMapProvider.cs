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
    ///     OpenTopoMap provider - https://www.opentopomap.org/
    /// </summary>
    public class OpenTopoMapProvider : OpenStreetMapProviderBase, OverlayProvider, CacheManager
    {
        public static readonly OpenTopoMapProvider Instance;
        private Timer cacheTimer = null;
        public delegate void CacheUpdated(GMapProvider provider);
        public event CacheUpdated OnCacheUpdated;

        OpenTopoMapProvider()
        {
            RefererUrl = "https://www.opentopomap.org/";
            Copyright = string.Format("© OpenTopoMap - Map data ©{0} OpenTopoMap", DateTime.Today.Year);
            cacheTimer = new Timer(RefreshCache, null, 0, 3600000);
        }

        static OpenTopoMapProvider()
        {
            Instance = new OpenTopoMapProvider();
        }

        #region GMapProvider Members

        public override Guid Id
        {
            get;
        } = new Guid("0120BDF8-90DD-47A3-836F-A277C1334D65");

        public override string Name
        {
            get;
        } = "OpenTopoMap";

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
            string url = MakeAlternateTileImageUrl(pos, zoom, string.Empty);

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

        static readonly string UrlFormat = "https://{0}.tile.opentopomap.org/{1}/{2}/{3}.png";
        static readonly string AlternateUrlFormat = "https://tile.opentopomap.org/{0}/{1}/{2}.png";
    }
}