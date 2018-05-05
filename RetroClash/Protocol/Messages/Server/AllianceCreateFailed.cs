using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceCreateFailed : Message
    {
        public AllianceCreateFailed(Device device) : base(device)
        {
            Id = 24332;
        }

        // Error Codes:
        // 1 = Invalid Name
        // 2 = Invalid Description
        // 3 = Name to short

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);
        }
    }
}