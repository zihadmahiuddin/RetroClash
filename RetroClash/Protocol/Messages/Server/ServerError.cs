using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class ServerError : Message
    {
        public ServerError(Device device) : base(device)
        {
            Id = 24115;
        }

        public string Reason { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteStringAsync(Reason);
        }
    }
}