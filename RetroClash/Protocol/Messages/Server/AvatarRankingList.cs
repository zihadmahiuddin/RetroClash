using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarRankingList : Message
    {
        public AvatarRankingList(Device device) : base(device)
        {
            Id = 24403;
        }

        public override async Task Encode()
        {
            var count = 0;

            using (var buffer = new MemoryStream())
            {
                foreach (var player in (await MySQL.GetRandomPlayers(100)).OrderByDescending(x => x.Score))
                {
                    await buffer.WriteLongAsync(player.AccountId);
                    await buffer.WriteStringAsync(player.Name);

                    await buffer.WriteIntAsync(count + 1);
                    await buffer.WriteIntAsync(player.Score);
                    await buffer.WriteIntAsync(200);

                    await buffer.WriteBufferAsync(await player.AvatarRankingEntry());

                    if (count++ >= 199)
                        break;
                }

                await Stream.WriteIntAsync(count);
                await Stream.WriteBufferAsync(buffer.ToArray());
            }         
        }
    }
}
