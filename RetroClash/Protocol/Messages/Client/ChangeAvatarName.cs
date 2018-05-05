using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Commands.Server;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class ChangeAvatarName : Message
    {
        public ChangeAvatarName(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Name { get; set; }

        public override void Decode()
        {
            Name = Reader.ReadString();
        }

        public override async Task Process()
        {
            Device.Player.Name = Name;
            Device.Player.TutorialSteps = 13;
            Device.Player.ExpLevel = 100;

            await Resources.Gateway.Send(new AvailableServerCommand(Device)
            {
                Command = await new LogicChangeAvatarName(Device)
                {
                    AvatarName = Name
                }.Handle()
            });
        }
    }
}