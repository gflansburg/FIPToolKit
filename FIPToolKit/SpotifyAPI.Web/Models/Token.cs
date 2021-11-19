﻿using Newtonsoft.Json;
using System;

namespace SpotifyAPI.Web.Models
{
    [Serializable]
    public class Token
    {
        public Token()
        {
            CreateDate = DateTime.Now;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Checks if the token has expired
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            return CreateDate.Add(TimeSpan.FromSeconds(ExpiresIn)) <= DateTime.Now;
        }

        public bool HasError()
        {
            return Error != null;
        }
    }
}