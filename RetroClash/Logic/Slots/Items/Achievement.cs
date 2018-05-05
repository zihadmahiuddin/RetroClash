using Newtonsoft.Json;

namespace RetroClash.Logic.Slots.Items
{
    public class Achievement
    {
        [JsonProperty("data")]
        public int Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}