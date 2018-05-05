using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic.Slots;

namespace RetroClash.Logic
{
    public class Alliance
    {
        public Alliance()
        {
        }

        public Alliance(long id)
        {
            Id = id;
            Name = "RetroClash";
            Description = "RetroClash Clan";
            Badge = 13000000;
            Type = 0;
            RequiredScore = 0;

            Members = new Dictionary<long, AllianceMember>(50);
        }

        [JsonProperty("alliance_id")]
        public long Id { get; set; }

        [JsonProperty("alliance_name")]
        public string Name { get; set; }

        [JsonProperty("alliance_description")]
        public string Description { get; set; }

        [JsonProperty("alliance_badge")]
        public int Badge { get; set; }

        [JsonProperty("alliance_type")]
        public int Type { get; set; }

        [JsonProperty("alliance_required_score")]
        public int RequiredScore { get; set; }

        [JsonIgnore]
        public int Score => Members.Sum(m => m.Value.Score) / 2;

        [JsonProperty("members")]
        public Dictionary<long, AllianceMember> Members { get; set; }

        public async Task AllianceRankingEntry(MemoryStream stream)
        {
            await stream.WriteIntAsync(Badge); // Badge
            await stream.WriteIntAsync(0); // Member Count
        }

        public async Task AllianceFullEntry(MemoryStream stream)
        {
            await AllianceHeaderEntry(stream);

            await stream.WriteStringAsync(Description); // Description
            await stream.WriteLongAsync(0); // Donation Reset Time
            await stream.WriteLongAsync(0); // Ranking Check Time
        }

        public async Task AllianceHeaderEntry(MemoryStream stream)
        {
            await stream.WriteLongAsync(Id); // Id
            await stream.WriteStringAsync(Name); // Name
            await stream.WriteIntAsync(Badge); // Badge
            await stream.WriteIntAsync(Type); // Type
            await stream.WriteIntAsync(Members.Count); // Member Count
            await stream.WriteIntAsync(Score); // Score
            await stream.WriteIntAsync(RequiredScore); // Required Score
        }
    }
}