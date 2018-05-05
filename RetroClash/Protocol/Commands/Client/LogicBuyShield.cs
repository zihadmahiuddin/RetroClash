using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicBuyShield : Command
    {
        public LogicBuyShield(Device device, Reader reader) : base(device, reader)
        {
        }

        public int ShieldType { get; set; }

        public override void Decode()
        {
            ShieldType = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.Shield.SetShield(ShieldType);
        }
    }
}