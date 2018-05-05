using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Server
{
    public class LogicChangeAvatarName : Command
    {
        public LogicChangeAvatarName(Device device) : base(device)
        {
            Id = 3;
        }

        public string AvatarName { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteStringAsync(AvatarName);
        }
    }
}