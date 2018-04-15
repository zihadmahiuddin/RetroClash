using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class AskForAvatarProfileMessage : Message
    {
        public AskForAvatarProfileMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AvatarId { get; set; }

        public override void Decode()
        {
            AvatarId = Reader.ReadInt64();
            Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarProfile(Device)
            {
                UserId = AvatarId
            });
        }
    }
}