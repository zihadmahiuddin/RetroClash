using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicMatchmaking : Command
    {
        public LogicMatchmaking(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new EnemyHomeData(Device)
            {
                Enemy = Resources.PlayerCache.Random
            });

            if (Device.Player.Shield.IsShieldActive)
                Device.Player.Shield.RemoveShield();
        }
    }
}
