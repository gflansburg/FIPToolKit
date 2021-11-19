using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models
{
    public class FullArtist : BasicModel
    {
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("follower_count")]
        [JsonIgnore]
        public int FollowerCount
        {
            get
            {
                if (Followers != null)
                {
                    return Followers.Total;
                }
                return 0;
            }
        }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("genre")]
        [JsonIgnore]
        public string Genre
        {
            get
            {
                string genre = String.Empty;
                if (Genres != null)
                {
                    foreach (string s in Genres)
                    {
                        if (!String.IsNullOrEmpty(genre))
                        {
                            genre += ",";
                        }
                        genre += s;
                    }
                }
                return genre;
            }
        }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }


        [JsonProperty("image")]
        public string Image
        {
            get
            {
                if(Images != null && Images.Count > 0)
                {
                    return Images[0].Url;
                }
                return null;
            }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}