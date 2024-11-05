using System;
using System.Collections.Generic;

namespace M3U.Media
{
    public class M3UMediaContainer
    {
        public IReadOnlyList<M3UMedia> Medias { get; internal set; } = new List<M3UMedia>();

        public TimeSpan Duration { get; internal set; }
    }
}
