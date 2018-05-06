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
        public string Language { get; set; }
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

            Language = Reader.ReadString(); // Country

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
                    await Resources.Gateway.Send(new LoginFailed(Device) {ErrorCode = 10});
                else
                {
                    if (AccountId == 0)
                    {
                        Device.Player = await MySQL.CreatePlayer();

                        if (Device.Player != null)
                        {
                            Device.Player.Language = Language;
                            Device.Player.DeviceName = DeviceName;
                            Device.Player.IpAddress = ((IPEndPoint) Device.Socket.RemoteEndPoint).Address.ToString();
                            Device.Player.Device = Device;

                            await Resources.Gateway.Send(new LoginOk(Device));                          

                            Resources.PlayerCache.AddPlayer(Device.Player);                          

                            await Resources.Gateway.Send(new OwnHomeData(Device));
                        }
                        else
                            await Resources.Gateway.Send(new OutOfSync(Device));
                    }
                    else
                    {
                        Device.Player = await MySQL.GetPlayer(AccountId);

                        if (Device.Player != null && Device.Player.PassToken == Token)
                        {
                            Device.Player.Device = Device;

                            await Resources.Gateway.Send(new LoginOk(Device));

                            Resources.PlayerCache.AddPlayer(Device.Player);                           

                            await Resources.Gateway.Send(new OwnHomeData(Device));
                        }
                        else
                            await Resources.Gateway.Send(new OutOfSync(Device));
                    }
                }
            }
        }
    }
}