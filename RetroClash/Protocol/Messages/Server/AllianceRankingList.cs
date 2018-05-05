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
            await Stream.WriteIntAsync(1);

            await Stream.WriteLongAsync(1);
            await Stream.WriteStringAsync("RetroClash");
            await Stream.WriteIntAsync(1);
            await Stream.WriteIntAsync(5000);
            await Stream.WriteIntAsync(200);
            await Stream.WriteIntAsync(13000022);
            await Stream.WriteIntAsync(1);

            await Stream.WriteIntAsync(604800);
            await Stream.WriteIntAsync(0);
            await Stream.WriteIntAsync(1);
        }
    }
}
