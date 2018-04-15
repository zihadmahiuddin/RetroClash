using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicNewsSeen : Command
    {
        public LogicNewsSeen(Device device, Reader reader) : base(device, reader)
        {         
        }

        public override async Task Process()
        {
            Device.Player.Objects.LastNewsSeen = Reader.ReadInt32();
        }
    }
}
