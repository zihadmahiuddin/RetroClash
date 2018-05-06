using System.IO;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceRankingList : Message
    {
        public AllianceRankingList(Device device) : base(device)
        {
            Id = 24401;
        }

        public override async Task Encode()
        {
            var count = 0;

            using (var buffer = new MemoryStream())
            {
                foreach (var alliance in Resources.LeaderboardCache.GlobalAlliances)
                {
                    if (alliance == null) continue;
                    await buffer.WriteLongAsync(alliance.Id);
                    await buffer.WriteStringAsync(alliance.Name);
                    await buffer.WriteIntAsync(count + 1);
                    await buffer.WriteIntAsync(alliance.Score);
                    await buffer.WriteIntAsync(200);

                    await alliance.AllianceRankingEntry(buffer);

                    if (count++ >= 199)
                        break;
                }

                await Stream.WriteIntAsync(count);
                await Stream.WriteBufferAsync(buffer.ToArray());

                await Stream.WriteIntAsync(604800);
                await Stream.WriteIntAsync(0);
                await Stream.WriteIntAsync(1);
            }
        }
    }
}
