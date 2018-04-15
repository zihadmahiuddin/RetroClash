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

            await Stream.WriteBufferAsync(await Enemy.LogicClientHome());
            await Stream.WriteBufferAsync(await Enemy.LogicClientAvatar());

            await Stream.WriteBufferAsync(await Device.Player.LogicClientAvatar());

            await Stream.WriteIntAsync(3);
            Stream.WriteByte(0);
        }
    }
}
