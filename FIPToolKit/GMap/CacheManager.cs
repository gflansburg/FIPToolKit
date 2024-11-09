using GMap.NET.MapProviders;
using System.Net;
using System.Threading;

namespace GMap.NET
{
    /// <summary>
    ///     cache manager interface
    /// </summary>
    public interface CacheManager
    {
        void RefreshCache(object state);
        void InitializeWebRequest2(WebRequest request);
    }
}
