using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class LoginOk : Message
    {
        public LoginOk(Device device) : base(device)
        {
            Id = 20104;
            Version = 1;
        }

        public override async Task Encode()
        {
            await Stream.WriteLongAsync(Device.Player.AccountId); // Account Id
            await Stream.WriteLongAsync(Device.Player.AccountId); // Home Id
            await Stream.WriteStringAsync(Device.Player.PassToken); // Pass Token

            await Stream.WriteStringAsync(null); // Facebook Id
            await Stream.WriteStringAsync(null); // Gamecenter Id

            await Stream.WriteIntAsync(0); // Server Major
            await Stream.WriteIntAsync(0); // Server Build
            await Stream.WriteIntAsync(0); // Content Version

            await Stream.WriteStringAsync("stage-content"); // Server Env

            await Stream.WriteIntAsync(0); // PlayTimeSeconds
            await Stream.WriteStringAsync("297484437009394"); // Facebook App Id

            await Stream.WriteIntAsync(0); // Session Count
            await Stream.WriteIntAsync(0); // Days since started playing

            await Stream.WriteStringAsync(null); // Server Time
            await Stream.WriteStringAsync(null); // Account Creation Date

            await Stream.WriteIntAsync(0); // Startup Cooldown Seconds

            await Stream.WriteStringAsync(null); // Google Service Id
        }
    }
}