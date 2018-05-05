using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicBuyDeco : Command
    {
        public LogicBuyDeco(Device device, Reader reader) : base(device, reader)
        {
        }

        public int DecoId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32();
            Y = Reader.ReadInt32();
            DecoId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.AddDeco(DecoId, X, Y);
        }
    }
}