/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * 
 * License: MIT
 * 
 */

using FIPToolKit.Tools;
using SimpleM3U8Parser.Exceptions;
using SimpleM3U8Parser.Media;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace SimpleM3U8Parser
{
    public class M3u8Parser
    {
        const string M3U8_TAG = "#EXTM3U";

        const string PATTERN = @"#EXTINF:(?<duration>.*),\n(?<link>(\S+))";

        static public M3u8MediaContainer ParseFromFile(string filename)
        {
            return Parse(System.IO.File.ReadAllText(filename));
        }

        static public M3u8MediaContainer Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException("M3u8Parser.Parse(content)");
            if (!content.Contains(M3U8_TAG))
                throw new ParserException("'content' is not a `m3u/m3u8` file.");
            var mediaList = new List<M3u8Media>();
            /*foreach (Match m in Regex.Matches(content, PATTERN))
            {
                var path = m.Groups["link"]?.Value;
                var duration = m.Groups["duration"]?.Value;
                if (!string.IsNullOrEmpty(path) && double.TryParse(duration, out double durationAsDouble))
                    mediaList.Add(new M3u8Media { Duration = durationAsDouble, Path = path });
            }*/
            if (mediaList.Count == 0)
            {
                string[] medias = content.Split('\n');
                for (int i = 1; i < medias.Length; i++)
                {
                    var path = medias[i].Trim();
                    if (path.StartsWith("#EXTINF"))
                    {
                        TimeSpan dur = new TimeSpan();
                        string duration = path.Substring(8, path.IndexOf(',') - 8);
                        if (duration.IsNumber())
                        {
                            if (Convert.ToDouble(duration) != -1)
                            {
                                dur = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                            }
                        }
                        else
                        {
                            string[] parts = duration.Split(':');
                            int hours = Convert.ToInt32(parts[0]);
                            int minutes = Convert.ToInt32(parts[1]);
                            double seconds = Convert.ToDouble(parts[2]);
                            int milliseconds = 0;
                            if (parts[2].Contains('.'))
                            {
                                string[] parts2 = parts[2].Split('.');
                                milliseconds = Convert.ToInt32(parts2[1]);
                            }
                            dur = new TimeSpan(0, hours, minutes, (int)seconds, milliseconds);
                        }
                        string title = path.Substring(path.IndexOf(',') + 1);
                        i++;
                        path = medias[i].Trim();
                        mediaList.Add(new M3u8Media { Path = path.Trim(), Duration = dur, Title = title });
                    }
                    else if (!string.IsNullOrEmpty(path) && !path.StartsWith("#"))
                    {
                        mediaList.Add(new M3u8Media { Path = path.Trim() });
                    }
                }
            }
            var durations = mediaList.Select(m => m.Duration).ToArray();
            var container = new M3u8MediaContainer
            {
                Medias = mediaList,
                Duration = mediaList.Sum(m => m.Duration)
            };
            return container;
        }
    }
}