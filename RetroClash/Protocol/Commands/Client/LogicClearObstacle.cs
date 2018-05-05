using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicClearObstacle : Command
    {
        public LogicClearObstacle(Device device, Reader reader) : base(device, reader)
        {           
        }

        public int ObstacleId { get; set; }

        public override void Decode()
        {
            ObstacleId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.RemoveObstacle(ObstacleId);
        }
    }
}
