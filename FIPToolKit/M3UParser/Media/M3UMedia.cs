using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace M3U.Media
{
    public class M3UMedia
    {
        internal M3UMedia(string title, TimeSpan? duration, string path, bool isStream, IEnumerable<KeyValuePair<string, string>> attributes, IEnumerable<string> adornments)
        {
            Title = title;
            Duration = duration;
            Path = path;
            IsStream = isStream;
            Attributes = new M3UAttrubutes(attributes);
            Adornments = new M3UAdornments(adornments);
        }

        public string Title { get; private set; }

        public TimeSpan? Duration { get; private set; }

        public string Path { get; private set; }

        public bool IsStream { get; private set; }

        public M3UAttrubutes Attributes { get; private set; }

        public M3UAdornments Adornments { get; private set; }
    }
}
