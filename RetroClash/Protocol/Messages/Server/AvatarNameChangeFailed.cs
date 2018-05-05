using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarNameChangeFailed : Message
    {
        public AvatarNameChangeFailed(Device device) : base(device)
        {
            Id = 20205;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);
        }
    }
}