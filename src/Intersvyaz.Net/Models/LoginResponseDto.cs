using System;
using Newtonsoft.Json;

namespace Intersvyaz.Net.Models
{
    public class LoginResponseDto
    {
        [JsonProperty("USER_ID")]
        public long UserId { get; set; }

        [JsonProperty("PROFILE_ID")]
        public long? ProfileId { get; set; }

        [JsonProperty("APP")]
        public string Application { get; set; }

        [JsonProperty("ACCESS_BEGIN")]
        public DateTime AccessBegin { get; set; }

        [JsonProperty("ACCESS_END")]
        public DateTime AccessEnd { get; set; }

        [JsonProperty("TOKEN")]
        public string Token { get; set; }

        [JsonProperty("AUTH_TYPE")]
        public string AuthenticationType { get; set; }

        [JsonProperty("UNIQUE_DEVICE_ID")]
        public string UniqueDeviceId { get; set; }

        [JsonProperty("PHONE")]
        public int Phone { get; set; }

        [JsonProperty("LAST_CHECK_DATE")]
        public DateTime LastCheckTime { get; set; }

        [JsonProperty("ACCESS_END_LEFT")]
        public long AccessEndLeft { get; set; }
    }
}
