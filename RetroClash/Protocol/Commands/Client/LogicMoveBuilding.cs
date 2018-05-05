using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicMoveBuilding : Command
    {
        public LogicMoveBuilding(Device device, Reader reader) : base(device, reader)
        {
        }

        public int BuildingId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32();
            Y = Reader.ReadInt32();
            BuildingId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.Move(BuildingId, X, Y);
        }
    }
}