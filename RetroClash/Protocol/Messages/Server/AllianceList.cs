using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceList : Message
    {
        public AllianceList(Device device) : base(device)
        {
            Id = 24310;
        }
    }
}