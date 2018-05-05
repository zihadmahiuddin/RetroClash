using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class PersonalBreakStarted : Message
    {
        public PersonalBreakStarted(Device device) : base(device)
        {
            Id = 20171;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(10); // Seconds?
        }
    }
}