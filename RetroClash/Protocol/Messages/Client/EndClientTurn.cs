using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class EndClientTurn : Message
    {
        public EndClientTurn(Device device, Reader reader) : base(device, reader)
        {
        }

        public int Count { get; set; }

        public override void Decode()
        {
            Reader.ReadInt32(); // Tick
            Reader.ReadInt32(); // Checksum

            Count = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            if (Count <= 512 && Count >= 0)
            {
                if (Count > 0)
                    using (var reader =
                        new Reader(Reader.ReadBytes((int)(Reader.BaseStream.Length - Reader.BaseStream.Position))))
                    {
                        for (var index = 0; index < Count; index++)
                        {
                            var id = reader.ReadInt32();

                            if (CommandFactory.Commands.ContainsKey(id))
                            {
                                try
                                {
                                    if (Activator.CreateInstance(CommandFactory.Commands[id], Device, reader) is Command
                                        command)
                                    {
                                        command.Decode();
                                        await command.Process();

                                        command.Dispose();

                                        if (Configuration.Debug)
                                            Console.WriteLine($"Command {id} has been processed.");
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                }
                            }
                            else
                            {
                                if (Configuration.Debug)
                                    Console.WriteLine($"Command {id} is unhandled.");
                            }
                        }
                    }
            }
            else
            {
                await Resources.Gateway.Send(new OutOfSync(Device));
            }
        }
    }
}