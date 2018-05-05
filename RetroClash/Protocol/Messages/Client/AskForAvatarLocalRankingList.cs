using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class AskForAvatarLocalRankingList : Message
    {
        public AskForAvatarLocalRankingList(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarLocalRankingList(Device));
        }
    }
}