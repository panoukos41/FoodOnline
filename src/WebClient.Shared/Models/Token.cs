using Newtonsoft.Json;
using System;

namespace FoodOnline.WebClient.Models
{
    /// <summary>
    /// Custom token saved in localstorage.
    /// </summary>
    public class Token
    {
        public Token()
        {
        }

        public Token(AuthToken authToken)
        {
            AuthToken = authToken.AccessToken;
            IdToken = authToken.IdToken;
            RefreshToken = authToken.RefreshToken;
            ExpiresIn = authToken.ExpiresIn;
            ExpireDateUtc = DateTime.UtcNow.AddSeconds(authToken.ExpiresIn);
        }

        [JsonProperty("access_token")]
        public string AuthToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("expire_date_utc")]
        public DateTime ExpireDateUtc { get; set; }
    }
}