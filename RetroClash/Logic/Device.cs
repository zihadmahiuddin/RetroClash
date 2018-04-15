using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClash.Crypto;
using RetroClash.Extensions;
using RetroClash.Network;
using RetroClash.Protocol;
using static RetroClash.Logic.Enums.States;

namespace RetroClash.Logic
{
    public class Device : IDisposable
    {
        public Rc4Core Rc4 = new Rc4Core();
        public State State = State.Login;
        public Token Token { get; set; }
        public Player Player { get; set; }
        public Socket Socket { get; set; }      

        public async Task ProcessPacket(byte[] buffer)
        {
            if (buffer.Length >= 7)
                using (var reader = new Reader(buffer))
                {
                    var identifier = reader.ReadUInt16();

                    reader.Seek(1, SeekOrigin.Current);

                    var length = reader.ReadUInt16();

                    if (buffer.Length - 7 < length) return;

                    if (!MessageFactory.Messages.ContainsKey(identifier))
                    {
                        if (Configuration.Debug)
                        {
                            Socket.Shutdown(SocketShutdown.Both);
                            Socket.Close();

                            Console.WriteLine($"PACKET {identifier} is not known.");
                        }
                    }
                    else
                    {
                        if (Activator.CreateInstance(MessageFactory.Messages[identifier], this, reader) is Message message)
                            try
                            {
                                message.Id = identifier;
                                message.Length = length;
                                message.Version = reader.ReadUInt16();

                                message.Decrypt();
                                message.Decode();

                                if (Configuration.Debug)
                                    Console.WriteLine(message.ToString());

                                await message.Process();

                                message.Dispose();
                            }
                            catch (Exception exception)
                            {
                                if (Configuration.Debug)
                                    Console.WriteLine(exception);
                            }
                    }

                    if (buffer.Length - 7 - length >= 7)
                        await ProcessPacket(reader.ReadBytes(buffer.Length - 7 - length));
                    else
                        Token.Reset();
                }
        }

        public void Disconnect()
        {
            try
            {
                Resources.Cache.RemovePlayer(Player.AccountId);

                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }

        public void Dispose()
        {
            Rc4 = null;
            Token = null;
            Player = null;
            Socket = null;
        }
    }
}