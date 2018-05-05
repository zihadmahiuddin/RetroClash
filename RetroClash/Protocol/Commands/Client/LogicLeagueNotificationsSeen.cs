using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicLeagueNotificationsSeen : Command
    {
        public LogicLeagueNotificationsSeen(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.LastLeagueRank = Reader.ReadInt32();
        }
    }
}