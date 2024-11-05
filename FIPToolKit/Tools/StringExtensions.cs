using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Tools
{
    public static class StringExtensions
    {
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
                string value = parts[1].Trim();
                attributes.Add(new KeyValuePair<string, string>(key, value));
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
                    if (!adornment.StartsWith("/") && text.Contains(string.Format("[/{0}]", tag)))
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
    }
}
