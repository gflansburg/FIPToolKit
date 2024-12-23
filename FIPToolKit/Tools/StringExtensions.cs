﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FIPToolKit.Tools
{
    public static class StringExtensions
    {
        public static bool IsIPAddress(this string str)
        {
            IPAddress iPAddress;
            return IPAddress.TryParse(str, out iPAddress);
        }

        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static bool IsStream(this string str)
        {
            return str.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || str.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsPlaylist(this string str)
        {
            return Path.GetExtension(str).Equals(".m3u", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(str).Equals(".m3u8", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAudio(this string str)
        {
            if (str.Contains(".mp3", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".wma", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".m4a", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".wav", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".ogg", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".flac", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".aac", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static bool IsVideo(this string str)
        {
            if (str.Contains(".mp4", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".wmv", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".mov", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".wav", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".avi", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".flv", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".ogm", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".3gp", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".asf", StringComparison.OrdinalIgnoreCase)
                || str.Contains(".mkv", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static string Escape(this string s)
        {
            if (s.Contains(QUOTE))
            {
                s = s.Replace(QUOTE, ESCAPED_QUOTE);
            }
            if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
            {
                s = QUOTE + s + QUOTE;
            }
            return s;
        }

        public static string Unescape(this string s)
        {
            if (s.StartsWith(QUOTE) && s.EndsWith(QUOTE))
            {
                s = s.Substring(1, s.Length - 2);
                if (s.Contains(ESCAPED_QUOTE))
                {
                    s = s.Replace(ESCAPED_QUOTE, QUOTE);
                }
            }
            return s;
        }


        private const string QUOTE = "\"";
        private const string ESCAPED_QUOTE = "\"\"";
        private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };
        
        static public string MakeKey(this string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToCamelCase().Replace(" ", string.Empty).Replace("M3u", "M3U");
            }
            return key;
        }

        public static IEnumerable<KeyValuePair<string, string>> ParseKeyValuePairs(this string attrs, char delimeter = ';')
        {
            List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
            IEnumerable<string> keyvaluepairs = (attrs.Replace(" =", "=").Replace("= ", "=")).ParseText(delimeter, new char[] { '\"',  '\'' });
            foreach(string keyvaluepair in keyvaluepairs)
            {
                string[] parts = keyvaluepair.Split('=');
                string key = parts[0].Trim();
                string value = string.Join("=", parts.Skip(1)).Replace("\\\"", "\"").Trim();
                attributes.Add(new KeyValuePair<string, string>(key.MakeKey(), value));
            }
            return attributes;
        }

        public static IEnumerable<string> ParseText(this string line, char delimiter, char[] textQualifiers)
        {

            if (line == null)
            {
                yield break;
            }
            else
            {
                char prevChar = '\0';
                char nextChar = '\0';
                char currentChar = '\0';
                char currentTextQualifier = '\0';
                bool inString = false;
                StringBuilder token = new StringBuilder();

                for (int i = 0; i < line.Length; i++)
                {
                    currentChar = line[i];
                    if (i > 0)
                    {
                        prevChar = line[i - 1];
                    }
                    else
                    {
                        prevChar = '\0';
                    }
                    if (i + 1 < line.Length)
                    {
                        nextChar = line[i + 1];
                    }
                    else
                    {
                        nextChar = '\0';
                    }   
                    if (textQualifiers.Contains(currentChar) && !inString)
                    {
                        currentTextQualifier = currentChar;
                        inString = true;
                        continue;
                    }
                    if (currentChar == currentTextQualifier && inString)
                    {
                        currentTextQualifier = '\0';
                        inString = false;
                        continue;
                    }
                    if (currentChar == delimiter && !inString)
                    {
                        yield return token.ToString();
                        token = token.Remove(0, token.Length);
                        continue;
                    }
                    token = token.Append(currentChar);
                }
                yield return token.ToString();
            }
        }

        public static IEnumerable<string> ParseAdornments(this string text, ref string title)
        {
            List<string> adornments = new List<string>();
            int index = text.IndexOf("[");
            while (index != -1)
            {
                int begin = text.IndexOf("[", index);
                int end = text.IndexOf("]", begin);
                if (end != -1)
                {
                    string adornment = text.Substring(begin + 1, (end - 1) - begin).Trim();
                    if (adornment.Contains('['))
                    {
                        end = text.IndexOf("[", begin + 1);
                        adornment = text.Substring(begin + 1, (end - 1) - begin).Trim();
                        end--;
                    }
                    int space = adornment.IndexOf(' ');
                    string tag = adornment;
                    if (space != -1)
                    {
                        tag = adornment.Substring(0, space);
                    }
                    end++;
                    if (!adornment.StartsWith("/") && (text.Contains(string.Format("[/{0}]", tag), StringComparison.OrdinalIgnoreCase) || text.Contains(string.Format("[/ {0}]", tag), StringComparison.OrdinalIgnoreCase)))
                    {
                        adornments.Add(adornment);
                        text = text.Substring(0, begin).Trim() + text.Substring(end).Trim();
                        end -= (end - begin);
                    }
                    else if (adornment.StartsWith("/"))
                    {
                        text = text.Substring(0, begin).Trim() + text.Substring(end).Trim();
                        end -= (end - begin);
                    }
                }
                else
                {
                    end = begin + 1;
                }
                index = text.IndexOf("[", end);
            }
            title = text;
            return adornments;
        }

        public static string ToTitleCase(this string s, string language = "en-US")
        {
            var fromSnakeCase = s.Replace("_", " ");
            var lowerToUpper = Regex.Replace(fromSnakeCase, @"(\p{Ll})(\p{Lu})", "$1 $2");
            var sentenceCase = Regex.Replace(lowerToUpper, @"(\p{Lu}+)(\p{Lu}\p{Ll})", "$1 $2");
            TextInfo tInfo = new CultureInfo(string.IsNullOrEmpty(language) ? "en-US" : language.Split(',')[0], false).TextInfo;
            return tInfo.ToTitleCase(sentenceCase).Replace(" A ", " a ", StringComparison.OrdinalIgnoreCase).Replace(" And ", " and ", StringComparison.OrdinalIgnoreCase).Replace("Atc", "ATC", StringComparison.OrdinalIgnoreCase).Replace("Ap", "AP").Replace("Adf", "ADF", StringComparison.OrdinalIgnoreCase).Replace("Egt", "EGT", StringComparison.OrdinalIgnoreCase).Replace("Dme", "DME", StringComparison.OrdinalIgnoreCase).Replace("G 1000", "G-1000", StringComparison.OrdinalIgnoreCase).Replace("G1000", "G-1000", StringComparison.OrdinalIgnoreCase).Replace("Gps", "GPS", StringComparison.OrdinalIgnoreCase).Replace("Mp", "MP").Replace("Vor", "VOR", StringComparison.OrdinalIgnoreCase).Replace("Vsi", "VSI", StringComparison.OrdinalIgnoreCase).Replace("Apu", "APU", StringComparison.OrdinalIgnoreCase).Replace("Pfd", "PFD", StringComparison.OrdinalIgnoreCase).Replace("Obs", "OBS", StringComparison.OrdinalIgnoreCase).Replace("Obi", "OBI", StringComparison.OrdinalIgnoreCase).Replace(" Vs ", " Vertical Speed ", StringComparison.OrdinalIgnoreCase).Replace("Fsuipc", "FSUIPC", StringComparison.OrdinalIgnoreCase).Replace("Fsuipcspeed", "FSUIPC Speed", StringComparison.OrdinalIgnoreCase).Replace("Lyp", "LYP", StringComparison.OrdinalIgnoreCase).Replace("Lua", "LUA", StringComparison.OrdinalIgnoreCase).Replace("Ptt", "PTT", StringComparison.OrdinalIgnoreCase).Replace("Pvt", "PVT", StringComparison.OrdinalIgnoreCase).Replace("APr ", "Apr ").Replace("OBServer", "Observer").Replace("Efis", "EFIS", StringComparison.OrdinalIgnoreCase).Replace("APaltChange", "AP Alt Change", StringComparison.OrdinalIgnoreCase).Replace("Nd ", "ND ").Replace("Iyp", "IYP", StringComparison.OrdinalIgnoreCase).Replace("Airlinetraffic", "Airline Traffic").Replace("Asnweathebroadcast", "Answer The Broadcast").Replace("Autodeleteai", "Auto Delete AI").Replace("Followme", "Follow Me").Replace("Postoggle", "Pos Toggle").Replace("Ndscale", "ND Scale").Replace("Cloudcover", "Cloud Cover").Replace("Mapitem", "Map Item").Replace("Inhg", "inHg").Replace("Comefly", "Come Fly").Replace("Keysend", "Key Send").Replace("Wideclients", "Wide Clients").Replace("Gatraffic Densityset", "GA Traffic Density Set").Replace("Logset", "Log Set").Replace("Mouselook", "Mouse Look").Replace("Mousemove Optiontoggle", "Mouse Move Option Toggle").Replace("Remotetextmenutoggle", "Remote Text Menu Toggle").Replace("Resimconnect", "Re-Sim Connect").Replace("Complexclouds", "Complex Clouds").Replace("Nninc", "Nn Inc").Replace("Toddle", "Toggle").Replace("Fracinc", "Frac Inc").Replace("Ils", "ILS").Replace("Hpa", "hPa").Replace("Mousebuttonswap", "Mouse Button Swap").Replace("Holdtoggle", "Hold Toggle").Replace("Ailerontrim", "Aileron Trim").Replace("Cowlflaps", "Cowl Flaps").Replace("Elevatortrim", "Elevator Trim").Replace("Leftbrake", "Left Brake").Replace("Rightbrake", "Right Brake").Replace("Ruddertrim", "Rudder Trim").Replace("Proppitch", "Prop Pitch").Replace("Panheading", "Pan Heading").Replace("Pantilt", "Pan Tilt").Replace("Panpitch", "Pan Pitch").Replace("Steeringtiller", "Steering Tiller").Replace("Slewahead", "Slew Ahead").Replace("Slewalt", "Slew Alt").Replace("Slewheading", "Slew Heading").Replace("Slewside", "Slew Side").Replace("VORADF", "VOR ADF").Replace("Antidetonation", "Anti-Detonation").Replace("Nt361", "NT361").Replace("Mfd", "MFD").Replace(" Of ", " of ").Replace("Antiskid", "Anti-Skid").Replace("Anti Ice", "Anti-Ice").Replace(" Bc ", " BC ").Replace(" Hdg ", " Heading ").Replace(" Alt ", " Altitude ").Replace(" Spd ", " Speed ").Replace("Zoomin", "Zoom In").Replace("Zoomout", "Zoom Out").Replace("Flightplan", "Flight Plan").Replace("Directto", "Direct To").Replace("Vnav", "VNav").Replace("Msl", "MSL").Replace(" Hud ", " HUD ");
        }

        public static string SplitCamelCase(this string s)
        {
            return Regex.Replace(
                Regex.Replace(
                    s,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static string ToCamelCase(this string s, string language = "en-US")
        {
            string str = s.Replace("_", " ").Replace("-", " ");
            if (str.Length == 0)
            {
                return string.Empty;
            }
            TextInfo tInfo = new CultureInfo(string.IsNullOrEmpty(language) ? "en-US" : language.Split(',')[0], false).TextInfo;
            return tInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another 
        /// specified string according the type of search to use for the specified string.
        /// </summary>
        /// <param name="str">The string performing the replace method.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string replace all occurrences of <paramref name="oldValue"/>. 
        /// If value is equal to <c>null</c>, than all occurrences of <paramref name="oldValue"/> will be removed from the <paramref name="str"/>.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue"/> are replaced with <paramref name="newValue"/>. 
        /// If <paramref name="oldValue"/> is not found in the current instance, the method returns the current instance unchanged.</returns>
        [DebuggerStepThrough]
        public static string Replace(this string str, string oldValue, string @newValue, StringComparison comparisonType)
        {
            // Check inputs.
            if (str == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(str));
            }
            if (str.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                return str;
            }
            if (oldValue == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(oldValue));
            }
            if (oldValue.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentException("String cannot be of zero length.");
            }

            //if (oldValue.Equals(newValue, comparisonType))
            //{
            //This condition has no sense
            //It will prevent method from replacesing: "Example", "ExAmPlE", "EXAMPLE" to "example"
            //return str;
            //}

            // Prepare string builder for storing the processed string.
            // Note: StringBuilder has a better performance than String by 30-40%.
            StringBuilder resultStringBuilder = new StringBuilder(str.Length);

            // Analyze the replacement: replace or remove.
            bool isReplacementNullOrEmpty = string.IsNullOrEmpty(@newValue);

            // Replace all values.
            const int valueNotFound = -1;
            int foundAt;
            int startSearchFromIndex = 0;
            while ((foundAt = str.IndexOf(oldValue, startSearchFromIndex, comparisonType)) != valueNotFound)
            {
                // Append all characters until the found replacement.
                int @charsUntilReplacment = foundAt - startSearchFromIndex;
                bool isNothingToAppend = @charsUntilReplacment == 0;
                if (!isNothingToAppend)
                {
                    resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilReplacment);
                }

                // Process the replacement.
                if (!isReplacementNullOrEmpty)
                {
                    resultStringBuilder.Append(@newValue);
                }

                // Prepare start index for the next search.
                // This needed to prevent infinite loop, otherwise method always start search 
                // from the start of the string. For example: if an oldValue == "EXAMPLE", newValue == "example"
                // and comparisonType == "any ignore case" will conquer to replacing:
                // "EXAMPLE" to "example" to "example" to "example" … infinite loop.
                startSearchFromIndex = foundAt + oldValue.Length;
                if (startSearchFromIndex == str.Length)
                {
                    // It is end of the input string: no more space for the next search.
                    // The input string ends with a value that has already been replaced. 
                    // Therefore, the string builder with the result is complete and no further action is required.
                    return resultStringBuilder.ToString();
                }
            }

            // Append the last part to the result.
            int @charsUntilStringEnd = str.Length - startSearchFromIndex;
            resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilStringEnd);

            return resultStringBuilder.ToString();
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static string Left(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }

        public static string Right(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
        }

        public static string Mid(this string input, int start)
        {
            return input.Substring(Math.Min(start, input.Length));
        }

        public static string Mid(this string input, int start, int count)
        {
            return input.Substring(Math.Min(start, input.Length), Math.Min(count, Math.Max(input.Length - start, 0)));
        }

        public static bool IsNumber(this string input)
        {
            try
            {
                double result;
                return double.TryParse(input, out result);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            return source.Select(selector).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);
        }

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(this string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            // We actually have no idea what the encoding is if we reach this point, so
            // you may wish to return null instead of defaulting to ASCII
            return Encoding.ASCII;
        }

        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        public static string ToMinimalReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} d, ", span.Days) : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} h, ", span.Hours) : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} m, ", span.Minutes) : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} s", span.Seconds) : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 s";

            return formatted;
        }
    }
}
