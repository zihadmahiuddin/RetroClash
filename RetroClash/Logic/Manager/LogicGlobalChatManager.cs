using System.Threading.Tasks;
using RetroClash.Logic.Manager.Items;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Logic.Manager
{
    public class LogicGlobalChatManager
    {
        public async Task Process(GlobalChatEntry entry)
        {
                foreach (var player in Resources.PlayerCache.Players.Values)
                {
                    await Resources.Gateway.Send(new GlobalChatLine(player.Device)
                    {
                        AccountId = entry.SenderId,
                        Message = entry.Message,
                        ExpLevel = entry.SenderExpLevel,
                        League = entry.SenderLeague,
                        Name = entry.SenderName
                    });
                }           
        }
    }
}