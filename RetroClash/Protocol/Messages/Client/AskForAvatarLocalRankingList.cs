using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Client
{
    public class AskForAvatarLocalRankingList : Message
    {
        public AskForAvatarLocalRankingList(Device device, Reader reader) : base(device, reader)
        {            
        }

        public override async Task Process()
        {
           
        }
    }
}
