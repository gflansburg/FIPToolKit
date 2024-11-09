using GMap.NET.MapProviders;

namespace GMap.NET
{
    /// <summary>
    ///     overlay interface
    /// </summary>
    public interface OverlayProvider
    {
        void RefreshOverlays();
        void AddOverlay(GMapProvider overlay);
        void RemoveOverlay(GMapProvider overlay);
    }
}
