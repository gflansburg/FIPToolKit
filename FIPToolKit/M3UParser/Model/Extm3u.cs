using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Sprache;

namespace M3UParser
{
    public class Extm3u
    {
        public string PlayListType { get; set; }
        public bool HasEndList { get; set; }
        public int? TargetDuration { get; set; }
        public int? Version { get; set; }
        public int? MediaSequence { get; set; }
        public Attributes Attributes { get; set; }
        public IEnumerable<Media> Medias { get; set; }
        public IEnumerable<string> Warnings { get; set; }

        internal Extm3u(string content)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var segments = SegmentSpecification.SegmentCollection.Parse(content);

            if (segments == null || segments.Count() == 0)
            {
                throw new Exception("The content cannot be parsed");
            }

            if (!segments.First().StartsWith("#EXTM3U", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("The content do not has extm3u header");
            }
            else
            {
                // parse attributes
                Attributes = new Attributes(LinesSpecification.HeaderLine.Parse(segments.First()));
            }

            IList<string> warnings = new List<string>();
            IList<Media> medias = new List<Media>();

            foreach (var item in segments.Skip(1))
            {
                var tag = PairsSpecification.Tag.Parse(item);
                if (item.IndexOf(".m3u8", StringComparison.OrdinalIgnoreCase) != -1 && item.Contains("\r\n"))
                {
                    tag = new KeyValuePair<string, string>(item.Substring(1, item.IndexOf("\r\n") - 1), item.Substring(item.IndexOf("\r\n") + 2));
                }
                switch (tag.Key)
                {
                    case "EXT-X-PLAYLIST-TYPE":
                        this.PlayListType = tag.Value;
                        break;

                    case "EXT-X-TARGETDURATION":
                        this.TargetDuration = int.Parse(tag.Value);
                        break;

                    case "EXT-X-VERSION":
                        this.Version = int.Parse(tag.Value);
                        break;

                    case "EXT-X-MEDIA-SEQUENCE":
                        this.MediaSequence = int.Parse(tag.Value);
                        break;

                    case "EXTINF":
                        try
                        {
                            medias.Add(new Media(tag.Value));
                        }
                        catch
                        {
                            warnings.Add($"Can't parse media #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");
                        }
                        break;

                    case "EXT-X-ENDLIST":
                        this.HasEndList = true;
                        break;

                    default:
                        if (tag.Key.EndsWith(".m3u8"))
                        {
                            string[] files = tag.Value.Split('\n');
                            foreach(string file in files)
                            {
                                if (!string.IsNullOrEmpty(file.Trim()))
                                {
                                    medias.Add(new Media(file.Trim()));
                                }
                            }
                        }
                        else
                        {
                            warnings.Add($"Can't parse content #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");
                        }
                        break;
                }
            }

            this.Warnings = warnings.AsEnumerable();
            this.Medias = medias.AsEnumerable();
        }
    }
}
