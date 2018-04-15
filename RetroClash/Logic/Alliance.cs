using System.Collections.Generic;
using System.IO;
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
            Badge = 0;
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

        [JsonProperty("members")]
        public Dictionary<long, AllianceMember> Members { get; set; }

        public async Task<byte[]> AllianceRankingEntry()
        {
            using (var stream = new MemoryStream())
            {
                await stream.WriteIntAsync(Badge); // Badge
                await stream.WriteIntAsync(0); // Member Count

                return stream.ToArray();
            }
        }

        public async Task<byte[]> AllianceFullEntry()
        {
            using (var stream = new MemoryStream())
            {
                await stream.WriteBufferAsync(await AllianceHeaderEntry());

                await stream.WriteStringAsync(Description); // Description
                await stream.WriteLongAsync(0); // Donation Reset Time
                await stream.WriteLongAsync(0); // Ranking Check Time

                return stream.ToArray();
            }
        }

        public async Task<byte[]> AllianceHeaderEntry()
        {
            using (var stream = new MemoryStream())
            {
                await stream.WriteLongAsync(Id); // Id
                await stream.WriteStringAsync(Name); // Name
                await stream.WriteIntAsync(Badge); // Badge
                await stream.WriteIntAsync(Type); // Type
                await stream.WriteIntAsync(0); // Member Count
                await stream.WriteIntAsync(0); // Score
                await stream.WriteIntAsync(RequiredScore); // Required Score

                return stream.ToArray();
            }
        }
    }
}