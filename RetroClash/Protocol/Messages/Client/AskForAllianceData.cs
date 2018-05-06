using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class AskForAllianceData : Message
    {
        public AskForAllianceData(Device device, Reader reader) : base(device, reader)
        {            
        }

        public long AllianceId { get; set; }

        public override void Decode()
        {
            AllianceId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AllianceData(Device)
            {
                AllianceId = AllianceId
            });
        }
    }
}
