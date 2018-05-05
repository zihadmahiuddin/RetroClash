using System;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;

namespace RetroClash.Protocol.Messages.Server
{
    public class EnemyHomeData : Message
    {
        public EnemyHomeData(Device device) : base(device)
        {
            Id = 24107;
            Device.State = States.State.Battle;
        }

        public Player Enemy { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(10);

            if (Enemy != null)
            {
                await Enemy.LogicClientHome(Stream);
                await Enemy.LogicClientAvatar(Stream);
            }
            else
            {
                await Stream.WriteIntAsync(0);

                await Stream.WriteLongAsync(Device.Player.AccountId);
                await Stream.WriteStringAsync(
                    Resources.Levels.NpcLevels[new Random().Next(Resources.Levels.NpcLevels.Count - 1)]);

                await Stream.WriteIntAsync(0); // Defense Rating
                await Stream.WriteIntAsync(0); // Defense Factor
                await Stream.WriteIntAsync(0);

                await Device.Player.LogicClientAvatar(Stream);
            }

            await Device.Player.LogicClientAvatar(Stream);

            await Stream.WriteIntAsync(3);
            Stream.WriteByte(0);
        }
    }
}