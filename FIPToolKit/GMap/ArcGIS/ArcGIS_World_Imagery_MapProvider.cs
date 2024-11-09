using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Xml;
using GMap.NET.Entity;
using GMap.NET.Internals;
using GMap.NET.Projections;
using Newtonsoft.Json;

namespace GMap.NET.MapProviders
{
    /// <summary>
    ///     ArcGIS_World_NatGeo_Map provider - https://arcgisonline.com/
    /// </summary>
    public class ArcGIS_World_Imagery_MapProvider : ArcGISMapMercatorProviderBase, OverlayProvider, CacheManager
    {
        public static readonly ArcGIS_World_Imagery_MapProvider Instance;
        private Timer cacheTimer = null;
        public delegate void CacheUpdated(GMapProvider provider);
        public event CacheUpdated OnCacheUpdated;

        ArcGIS_World_Imagery_MapProvider()
        {
            RefererUrl = "https://arcgisonline.com/";
            cacheTimer = new Timer(RefreshCache, null, 0, 3600000);
        }

        static ArcGIS_World_Imagery_MapProvider()
        {
            Instance = new ArcGIS_World_Imagery_MapProvider();
        }

        #region GMapProvider Members

        public override Guid Id
        {
            get;
        } = new Guid("8363787E-0FCE-446B-ABF9-EB857D9E74CB");

        public override string Name
        {
            get;
        } = "ArcGIS_World_Imagery_Map";

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
            return string.Format(UrlFormat, zoom, pos.Y, pos.X);
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

        static readonly string UrlFormat = "https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{0}/{1}/{2}";
    }
}