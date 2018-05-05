using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Client
{
    public class SetDeviceTokenMessage : Message
    {
        public SetDeviceTokenMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadString();
        }
    }
}