using System;
using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;

namespace RetroClash
{
    public class Program
    {
        private Resources _resources;

        private static void Main(string[] args)
        {
           new Program().StartAsync().GetAwaiter().GetResult();
        }

        public async Task StartAsync()
        {
            Console.Title = "RetroClash Server v0.3";

            Console.WriteLine("\r\n________     _____             ______________             ______  \r\n___  __ \\______  /_______________  ____/__  /_____ __________  /_ \r\n__  /_/ /  _ \\  __/_  ___/  __ \\  /    __  /_  __ `/_  ___/_  __ \\\r\n_  _, _//  __/ /_ _  /   / /_/ / /___  _  / / /_/ /_(__  )_  / / /\r\n/_/ |_| \\___/\\__/ /_/    \\____/\\____/  /_/  \\__,_/ /____/ /_/ /_/ \r\n                                                                  \r\n");

            Console.SetOut(new Prefixed());

            Console.WriteLine("Starting...");

            _resources = new Resources();

            Console.ResetColor();

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                switch (key)
                {
                    case ConsoleKey.D:
                        {
                            Configuration.Debug = !Configuration.Debug;
                            Console.WriteLine("Debugging has been " + (Configuration.Debug ? "enabled." : "disabled."));
                            break;
                        }

                    case ConsoleKey.H:
                        {
                            Console.WriteLine("Commands: [D]ebug, [H]elp, [K]ey, [M]aintenance");
                            break;
                        }

                    case ConsoleKey.K:
                        {
                            Console.WriteLine($"Generated RC4 Key: {Utils.GenerateRc4Key}");
                            break;
                        }

                    case ConsoleKey.M:
                        {
                            Configuration.Maintenance = !Configuration.Maintenance;

                            if (Configuration.Maintenance)
                            {
                                try
                                {
                                    Console.WriteLine("Removing every Player in cache...");
                                    foreach (var player in Resources.Cache.Players.Values)
                                    {
                                        player.Device.Disconnect();
                                    }
                                    Console.WriteLine("Done!");
                                }
                                catch (Exception exception)
                                {
                                    if (Configuration.Debug)
                                        Console.WriteLine(exception);
                                }
                            }

                            Console.WriteLine("Maintenance has been " + (Configuration.Maintenance ? "enabled." : "disabled."));
                            break;
                        }

                    case ConsoleKey.S:
                    {
                        Console.WriteLine($"[STATUS] Online Players: {Resources.Cache.Players.Count}, Players Saved: {await MySQL.PlayerCount()}");
                        break;
                    }

                    case ConsoleKey.T:
                        {
                            Console.WriteLine("Do you want to create 1000 Test Accounts? If yes press enter. To Abort any key than enter.");

                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                Console.WriteLine("Creating 1000 Accounts.");
                                for (var i = 0; i < 1000; i++)
                                {
                                    await MySQL.CreatePlayer();
                                }
                                Console.WriteLine("1000 Accounts created.");
                            }
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid Key. Press 'H' for help.");
                            break;
                        }
                }

                Console.ResetColor();
            }
        }
    }
}