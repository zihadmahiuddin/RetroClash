using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class GoHome : Message
    {
        public GoHome(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            if (Device.State != States.State.Home)
                await Resources.Gateway.Send(new OwnHomeData(Device));
            else
                await Resources.Gateway.Send(new OutOfSync(Device));
        }
    }
}