using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;

namespace RetroClash.Protocol.Messages.Server
{
    public class VisitedHomeData : Message
    { 
        public VisitedHomeData(Device device) : base(device)
        {
            Id = 24113;
            Device.State = States.State.Visiting;
        }

        public long AvatarId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);

            var player = await Resources.Cache.GetPlayer(AvatarId);

            await Stream.WriteBufferAsync(await player.LogicClientHome());
            await Stream.WriteBufferAsync(await player.LogicClientAvatar());

            Stream.WriteByte(1);
            await Stream.WriteBufferAsync(await Device.Player.LogicClientAvatar());
        }
    }
}
