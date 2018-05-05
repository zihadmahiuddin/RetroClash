using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Files;
using RetroClash.Files.Logic;
using RetroClash.Logic;
using RetroClash.Logic.Slots.Items;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicClaimAchievementReward : Command
    {
        public LogicClaimAchievementReward(Device device, Reader reader) : base(device, reader)
        {
        }

        public int AchievementId { get; set; }

        public override void Decode()
        {
            AchievementId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.Achievements.Add(new Achievement
            {
                Id = AchievementId,
                Data = ((Achievements) Csv.Tables.Get(1).GetDataWithId(Id)).ActionCount
            });
        }
    }
}