using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicEndCombat : Command
    {
        public LogicEndCombat(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}