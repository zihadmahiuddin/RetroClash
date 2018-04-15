using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicPlaceAttacker : Command
    {
        public LogicPlaceAttacker(Device device, Reader reader) : base(device, reader)
        {           
        }

        public int UnitId { get; set; }

        public override void Decode()
        {
            Reader.ReadInt32(); // X
            Reader.ReadInt32(); // Y

            UnitId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var index = Device.Player.Units.Troops.FindIndex(unit => unit.Id == UnitId);

            if (index > -1)
                Device.Player.Units.Troops[index].Count--;
        }
    }
}
