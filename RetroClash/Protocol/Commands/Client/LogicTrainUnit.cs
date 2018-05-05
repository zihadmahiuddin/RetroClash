using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicTrainUnit : Command
    {
        public LogicTrainUnit(Device device, Reader reader) : base(device, reader)
        {
        }

        public int UnitId { get; set; }
        public int IsSpell { get; set; }
        public int Count { get; set; }

        public override void Decode()
        {
            Reader.ReadInt32();

            IsSpell = Reader.ReadInt32();
            UnitId = Reader.ReadInt32();
            Count = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.Units.Train(UnitId, IsSpell, Count);
        }
    }
}