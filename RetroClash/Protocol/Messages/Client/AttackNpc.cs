using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class AttackNpc : Message
    {
        public AttackNpc(Device device, Reader reader) : base(device, reader)
        {
        }

        public int LevelId { get; set; }

        public override void Decode()
        {
            LevelId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new NpcData(Device)
            {
                NpcId = LevelId
            });
        }
    }
}