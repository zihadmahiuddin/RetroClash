using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class SendGlobalChatLine : Message
    {
        public SendGlobalChatLine(Device device, Reader reader) : base(device, reader)
        {        
        }

        public string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadString();
        }

        public override async Task Process()
        {
            if (Message.StartsWith("/"))
            {
                switch (Message.Split(' ')[0])
                {
                    case "/help":
                    {
                        await Resources.Gateway.Send(new GlobalChatLine(Device)
                        {
                            Message = "Available commands:\n\n/help → List of all commands.\n/wall [level] → Set the level of all walls.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16,
                            AccountId = 0,
                            HomeId = 0
                        });
                        break;
                    }

                    case "/wall":
                    {
                        var lvl = Convert.ToInt32(Message.Split(' ')[1]);
                        if (lvl > 0 && lvl < 12)
                        {
                            foreach (var building in Device.Player.Objects.Buildings)
                            {
                                if (building.Data != 1000010) continue;
                                building.Level = lvl - 1;
                            }

                            await Resources.Gateway.Send(new OwnHomeData(Device));

                            await Resources.Gateway.Send(new GlobalChatLine(Device)
                            {
                                Message = $"Wall level set to {lvl}.",
                                Name = "DebugManager",
                                ExpLevel = 100,
                                League = 16,
                                AccountId = 0,
                                HomeId = 0
                            });
                        }
                        break;
                    }

                    default:
                    {
                        await Resources.Gateway.Send(new GlobalChatLine(Device)
                        {
                            Message = "Invalid Command. Type '/help' for a list of all commands.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16,
                            AccountId = 0,
                            HomeId = 0
                        });
                        break;
                    }
                }
            }
            else
            {
                await Resources.Gateway.Send(new GlobalChatLine(Device)
                {
                    Message = Message,
                    Name = Device.Player.Name,
                    ExpLevel = Device.Player.ExpLevel,
                    League = LogicUtils.GetLeagueByScore(Device.Player.Score),
                    AccountId = Device.Player.AccountId,
                    HomeId = Device.Player.AccountId
                });
            }
        }
    }
}
