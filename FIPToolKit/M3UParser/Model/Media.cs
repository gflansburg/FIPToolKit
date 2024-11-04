using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using LibVLCSharp.Shared;
using Sprache;

namespace M3UParser
{
    public class Media
    {
        public decimal Duration { get; set; }

        public Title Title { get; set; }

        public string MediaFile { get; set; }

        public bool IsStream { get { return this.Duration <= 0; } }

        public Attributes Attributes { get; set; }

        internal Media(string source)
        {
            try
            {
                var media = LinesSpecification.Extinf.Parse(source);

                this.Duration = media.Duration;
                this.Title = media.Title;
                this.MediaFile = media.MediaFile;
                this.Attributes = media.Attributes;
            }
            catch(Exception)
            {
                this.MediaFile = source;
                this.Title = new Title(System.IO.Path.GetFileNameWithoutExtension(source), System.IO.Path.GetFileNameWithoutExtension(source));
            }
        }

        internal Media(decimal duration, IEnumerable<KeyValuePair<string, string>> attributes, Title title, string mediafile)
        {
            Duration = duration;
            Title = title;
            MediaFile = mediafile;
            Attributes = new Attributes(attributes);
        }
    }
}