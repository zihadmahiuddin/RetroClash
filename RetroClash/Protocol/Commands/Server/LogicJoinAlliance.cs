using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Server
{
    public class LogicJoinAlliance : Command
    {
        public LogicJoinAlliance(Device device) : base(device)
        {
            Id = 1;
        }

        public long AllianceId { get; set; }
        public string AllianceName { get; set; }
        public int AllianceBadge { get; set; }
        public bool AllianceRole { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteLongAsync(AllianceId);
            await Stream.WriteStringAsync(AllianceName);
            await Stream.WriteIntAsync(AllianceBadge);
            Stream.WriteByte((byte)(AllianceRole ? 2 : 1));
        }
    }
}