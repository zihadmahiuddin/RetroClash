using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Slots;
using RetroClash.Protocol.Commands.Server;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class CreateAlliance : Message
    {
        public CreateAlliance(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Badge { get; set; }
        public int Type { get; set; }
        public int RequiredScore { get; set; }

        public override void Decode()
        {
            Name = Reader.ReadString();
            Description = Reader.ReadString();
            Badge = Reader.ReadInt32();
            Type = Reader.ReadInt32();
            RequiredScore = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            /*if (await MySQL.CreateAlliance() is Alliance alliance)
            {
                alliance.Name = Name;
                alliance.Description = Description;
                alliance.Badge = Badge;
                alliance.Type = Type;
                alliance.RequiredScore = RequiredScore;

                alliance.Members.Add(Device.Player.AccountId,
                    new AllianceMember(Device.Player.AccountId, 2, Device.Player.Score));

                await Resources.Gateway.Send(new AvailableServerCommand(Device)
                {
                    Command = await new LogicJoinAlliance(Device)
                    {
                        AllianceId = alliance.Id,
                        AllianceName = Name,
                        AllianceBadge = Badge,
                        AllianceRole = true
                    }.Handle()
                });

                Device.Player.AllianceId = alliance.Id;

                await MySQL.SaveAlliance(alliance);
            }
            else
            {*/
                await Resources.Gateway.Send(new AllianceCreateFailed(Device));
            //}
        }
    }
}