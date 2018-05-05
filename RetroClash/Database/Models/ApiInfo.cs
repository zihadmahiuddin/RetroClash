using Newtonsoft.Json;

namespace RetroClash.Database.Models
{
    public class ApiInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("Online")]
        public int Online { get; set; }

        [JsonProperty("PlayersInDb")]
        public long PlayersInDb { get; set; }

        [JsonProperty("AlliancesInDb")]
        public long AlliancesInDb { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("CurrentDateTime")]
        public string CurrentDateTime { get; set; }
    }
}