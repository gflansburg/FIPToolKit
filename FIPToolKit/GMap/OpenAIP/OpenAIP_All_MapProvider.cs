using System;
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
    ///     Open AIP all layers map provider - https://openaip.net/
    /// </summary>
    public class OpenAIP_All_MapProvider : GMapProvider, CacheManager
    {
        public static readonly OpenAIP_All_MapProvider Instance;
        private Timer cacheTimer = null;
        public delegate void CacheUpdated(GMapProvider provider);
        public event CacheUpdated OnCacheUpdated;

        OpenAIP_All_MapProvider()
        {
            RefererUrl = "https://openaip.net/";
            Copyright = string.Format("© OpenAIP", DateTime.Today.Year);
            cacheTimer = new Timer(RefreshCache, null, 0, 3600000);
        }

        static OpenAIP_All_MapProvider()
        {
            Instance = new OpenAIP_All_MapProvider();
        }

        #region GMapProvider Members

        public override Guid Id
        {
            get;
        } = new Guid("B61D5456-AB7B-4F49-B44A-0057B4808E8A");

        public override string Name
        {
            get;
        } = "OpenAIP_All_MapProvider";

        public string OpenAIPClientIDToken { get; set; }

        public override PureProjection Projection
        {
            get
            {
                return MercatorProjection.Instance;
            }
        }

        GMapProvider[] _overlays;

        public override GMapProvider[] Overlays
        {
            get
            {
                if (_overlays == null)
                {
                    _overlays = new GMapProvider[] { this };
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

        protected override void InitializeWebRequest(WebRequest request)
        {
            base.InitializeWebRequest(request);

            if (!string.IsNullOrEmpty(OpenAIPClientIDToken))
            {
                request.Headers.Add("x-openaip-api-key", OpenAIPClientIDToken);
            }
        }

        #endregion

        public readonly string ServerLetters = "abc";

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            char letter = ServerLetters[GetServerNum(pos, 3)];
            return string.Format(UrlFormat, letter, zoom, pos.X, pos.Y);
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

        static readonly string UrlFormat = "https://{0}.api.tiles.openaip.net/api/data/openaip/{1}/{2}/{3}.png";
    }
}