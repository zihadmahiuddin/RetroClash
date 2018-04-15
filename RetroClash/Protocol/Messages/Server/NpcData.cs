using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;

namespace RetroClash.Protocol.Messages.Server
{
    public class NpcData : Message
    {
        public NpcData(Device device) : base(device)
        {
            Id = 24133;
            Device.State = States.State.Battle;
        }

        public int NpcId { get; set; }

        public override async Task Encode()
        {          
            await Stream.WriteIntAsync(0);

            await Stream.WriteStringAsync(Resources.Levels.NpcLevels[NpcId - 17000000]);

            await Stream.WriteBufferAsync(await Device.Player.LogicClientAvatar());

            await Stream.WriteIntAsync(0);

            await Stream.WriteIntAsync(NpcId);
        }
    }
}
