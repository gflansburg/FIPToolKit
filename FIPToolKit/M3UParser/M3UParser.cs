﻿using FIPToolKit.Tools;
using M3U.Media;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace M3U
{
    public class M3UParser
    {
        const string M3U8_TAG = "#EXTM3U";

        static public M3UMediaContainer ParseFromFile(string filename)
        {
            return Parse(System.IO.File.ReadAllText(filename, filename.GetEncoding()));
        }

        static public M3UMediaContainer Parse(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var mediaList = new List<M3UMedia>();
                List<string> medias = content.Split('\n').ToList();
                medias = medias.Where(m => !string.IsNullOrEmpty(m)).ToList();
                if (medias.Count > 0 || medias[0].Equals(M3U8_TAG))
                {
                    for (int i = 1; i < medias.Count; i++)
                    {
                        var path = medias[i].Trim();
                        List<KeyValuePair<string, string>> attributes = null;
                        IEnumerable<string> adornments = null;
                        bool isStream = false;
                        TimeSpan? dur = null;
                        if (path.StartsWith("#EXTINF"))
                        {
                            string duration = path.Substring(8, path.IndexOf(',') - 8).Trim();
                            if (duration.Contains("="))
                            {
                                IEnumerable<KeyValuePair<string, string>> attrs = duration.Substring(duration.IndexOf(" ") + 1).ParseKeyValuePairs(' ');
                                if (attrs != null)
                                {
                                    attributes = new List<KeyValuePair<string, string>>();
                                    attributes.AddRange(attrs);
                                }
                                duration = duration.Substring(0, duration.IndexOf(" "));
                            }
                            if (duration.IsNumber())
                            {
                                if (Convert.ToDouble(duration) > 0)
                                {
                                    dur = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                                }
                                else
                                {
                                    isStream = true;
                                }
                            }
                            else
                            {
                                Regex regex = new Regex("^(?:(?:([01]?\\d|2[0-3]):)?([0-5]?\\d):)?([0-5]?\\d)(\\.(\\d{1,9}))?$");
                                Match match = regex.Match(duration);
                                if (match.Success)
                                {
                                    int hours = Convert.ToInt32(match.Groups[1].Value);
                                    int minutes = Convert.ToInt32(match.Groups[2].Value);
                                    int seconds = 0;
                                    if (match.Groups.Count > 2 && match.Groups[3].Success)
                                    {
                                        seconds = Convert.ToInt32(match.Groups[3].Value);
                                    }
                                    int milliseconds = 0;
                                    if (match.Groups.Count > 4 && match.Groups[5].Success)
                                    {
                                        milliseconds = Convert.ToInt32(match.Groups[5].Value);
                                    }
                                    dur = new TimeSpan(0, hours, minutes, seconds, milliseconds);
                                }
                            }
                            string title = path.IndexOf(',') != -1 ? path.Substring(path.IndexOf(',') + 1).Trim() : string.Empty;
                            if (title.Contains("="))
                            {
                                int comma = title.IndexOf(',');
                                IEnumerable<KeyValuePair<string, string>> attrs = title.Substring(0, comma != -1 ? comma : title.Length).ParseKeyValuePairs(' ');
                                if (attrs != null)
                                {
                                    if (attributes == null)
                                    {
                                        attributes = new List<KeyValuePair<string, string>>();
                                    }
                                    attributes.AddRange(attrs);
                                }
                                title = comma != -1 ? title.Substring(comma + 1) : string.Empty;
                            }
                            adornments = title.ParseAdornments(ref title);
                            i++;
                            path = medias[i].Trim();
                            mediaList.Add(new M3UMedia(title, dur, path, isStream, attributes, adornments));
                        }
                        else if (!string.IsNullOrEmpty(path) && !path.StartsWith("#"))
                        {
                            isStream = path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
                            mediaList.Add(new M3UMedia(System.IO.Path.GetFileNameWithoutExtension(path), dur, path, isStream, attributes, adornments));
                        }
                    }
                    var container = new M3UMediaContainer
                    {
                        Medias = mediaList,
                        Duration = mediaList.Sum(m => m.Duration.HasValue ? m.Duration.Value : new TimeSpan(0))
                    };
                    return container;
                }
            }
            return null;
        }
    }
}