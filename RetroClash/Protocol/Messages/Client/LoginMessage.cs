using System;
using System.Net;
using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Enums;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class LoginMessage : Message
    {
        public LoginMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AccountId { get; set; }
        public string Token { get; set; }
        public string Country { get; set; }
        public string DeviceName { get; set; }

        public override void Decode()
        {
            AccountId = Reader.ReadInt64(); // ACCOUNT ID
            Token = Reader.ReadString(); // PASS TOKEN

            Reader.ReadInt32(); // Major Version
            Reader.ReadInt32(); // Minor Version
            Reader.ReadInt32(); // Build 

            Reader.ReadString(); // Masterhash

            Reader.ReadString(); // UDID

            Reader.ReadString(); // OpenUDID

            Reader.ReadString(); // MacAddress
            DeviceName = Reader.ReadString(); // Device

            Reader.ReadInt32(); // Unknown

            Country = Reader.ReadString(); // Country

            Reader.ReadInt32(); // Unknown

            Reader.ReadString(); // OS Version

            Reader.ReadByte(); // Unknown
            Reader.ReadInt32(); // Unknown

            Reader.ReadString(); // Unknown
        }

        public override async Task Process()
        {
            if (Device.State != States.State.Home)
            {
                if (Configuration.Maintenance)
                {
                    await Resources.Gateway.Send(new LoginFailed(Device) {ErrorCode = 10});
                }
                else
                {
                    if (AccountId == 0)
                    {
                        Device.Player = await MySQL.CreatePlayer();
                        Device.Player.Country = Country;
                        Device.Player.DeviceName = DeviceName;
                        Device.Player.IpAddress = ((IPEndPoint) Device.Socket.RemoteEndPoint).Address.ToString();

                        Resources.Cache.AddPlayer(Device.Player, Device);

                        await Resources.Gateway.Send(new LoginOk(Device));

                        await Resources.Gateway.Send(new OwnHomeData(Device));
                    }
                    else
                    {
                        Device.Player = await MySQL.GetPlayer(AccountId);

                        if (Device.Player.PassToken == Token)
                        {
                            Resources.Cache.AddPlayer(Device.Player, Device);

                            await Resources.Gateway.Send(new LoginOk(Device));

                            await Resources.Gateway.Send(new OwnHomeData(Device));
                        }
                        else
                        {
                            await Resources.Gateway.Send(new OutOfSync(Device));
                        }
                    }
                }
            }
        }
    }
}