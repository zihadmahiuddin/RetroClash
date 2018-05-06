using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceData : Message
    {
        public AllianceData(Device device) : base(device)
        {
            Id = 24301;
        }

        public long AllianceId { get; set; }

        public override async Task Encode()
        {
            var alliance = await MySQL.GetAlliance(AllianceId);

            await alliance.AllianceFullEntry(Stream);
        }
    }
}
