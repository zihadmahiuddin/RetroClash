using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicCastSpell : Command
    {
        public LogicCastSpell(Device device, Reader reader) : base(device, reader)
        {
        }

        public int SpellId { get; set; }

        public override void Decode()
        {
            Reader.ReadInt32(); // X
            Reader.ReadInt32(); // Y

            SpellId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var index = Device.Player.Units.Spells.FindIndex(spell => spell.Id == SpellId);

            if (index > -1)
                Device.Player.Units.Spells[index].Count--;
        }
    }
}