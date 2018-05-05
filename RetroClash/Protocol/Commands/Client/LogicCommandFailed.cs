using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicCommandFailed : Command
    {
        public LogicCommandFailed(Device device, Reader reader) : base(device, reader)
        {
            Id = 701;
        }

        public int FailedCommandType { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // FailedCommandType
        }
    }
}