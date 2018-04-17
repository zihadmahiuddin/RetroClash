using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;

namespace RetroClash.Protocol.Messages.Server
{
    public class OwnHomeData : Message
    {
        public OwnHomeData(Device device) : base(device)
        {
            Id = 24101;
            Device.State = States.State.Home;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);

            await Device.Player.LogicClientHome(Stream);

            await Device.Player.LogicClientAvatar(Stream);
        }
    }
}