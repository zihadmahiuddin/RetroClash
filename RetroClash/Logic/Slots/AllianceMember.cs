using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;

namespace RetroClash.Logic.Slots
{
    public class AllianceMember
    {
        public AllianceMember()
        {
        }

        public AllianceMember(long id, int role)
        {
            AccountId = id;
            Role = role;
        }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        public async Task<byte[]> AllianceMemberEntry(int order)
        {
            using (var stream = new MemoryStream())
            {
                var player = await Resources.Cache.GetPlayer(AccountId);

                await stream.WriteLongAsync(AccountId); // Avatar Id
                await stream.WriteStringAsync(player.Name); // Name
                await stream.WriteIntAsync(Role); // Role
                await stream.WriteIntAsync(player.ExpLevel); // Exp Level
                await stream.WriteIntAsync(LogicUtils.GetLeagueByScore(player.Score)); // League Type
                await stream.WriteIntAsync(player.Score); // Score
                await stream.WriteIntAsync(0); // Donations
                await stream.WriteIntAsync(0); // Donations Received
                await stream.WriteIntAsync(order); // Order
                await stream.WriteIntAsync(order); // Previous Order

                stream.WriteByte(0); // IsNewMember

                stream.WriteByte(1); // HomeId
                await stream.WriteLongAsync(AccountId); // Home Id

                return stream.ToArray();
            }
        }
    }
}