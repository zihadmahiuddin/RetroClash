using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicDisbandLeague : Command
    {
        public LogicDisbandLeague(Device device, Reader reader) : base(device, reader)
        {
            Id = 534;
        }
    }
}