using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class BindGoogleServiceAccount : Message
    {
        public BindGoogleServiceAccount(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadString();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new GoogleServiceAccountBound(Device));
        }
    }
}