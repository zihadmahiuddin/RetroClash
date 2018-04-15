using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class VisitHome : Message
    {
        public VisitHome(Device device, Reader reader) : base(device, reader)
        {           
        }

        public long UserId { get; set; }

        public override void Decode()
        {
            UserId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new VisitedHomeData(Device)
            {
                AvatarId = UserId
            });
        }
    }
}
