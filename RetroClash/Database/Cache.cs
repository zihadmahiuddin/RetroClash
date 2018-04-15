using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Logic;

namespace RetroClash.Database
{
    public class Cache
    {
        public Dictionary<long, Player> Players = new Dictionary<long, Player>();
        public Dictionary<long, Alliance> Alliances = new Dictionary<long, Alliance>();

        public readonly object Gate = new object();

        public void AddPlayer(Player player, Device device)
        {
            lock (Gate)
            {
                try
                {
                    if (Players.ContainsKey(player.AccountId))
                    {
                        Players.Remove(player.AccountId);
                    }

                    player.Device = device;

                    player.Timer.Elapsed += player.SaveCallback;
                    player.Timer.Start();

                    Players.Add(player.AccountId, player);
                }
                catch (Exception exception)
                {
                    if(Configuration.Debug)
                        Console.WriteLine(exception);
                }
            }
        }

        public async Task<Player> GetPlayer(long id)
        {
            lock (Gate)
            {
                if (Players.ContainsKey(id))
                {
                    return Players[id];
                }
            }

            return await MySQL.GetPlayer(id);
        }

        public async Task<Alliance> GetAlliance(long id)
        {
            lock (Gate)
            {
                if (Alliances.ContainsKey(id))
                {
                    return Alliances[id];
                }
            }

            return await MySQL.GetAlliance(id);
        }

        public void RemovePlayer(long id)
        {
            lock (Gate)
            {
                try
                {
                    if (!Players.ContainsKey(id)) return;

                    var player = Players[id];

                    player.Timer.Stop();
                    MySQL.SavePlayer(player).Wait();    
                    
                    Players.Remove(id);
                }
                catch (Exception exception)
                {
                    if (Configuration.Debug)
                        Console.WriteLine(exception);
                }
            }
        }

        public Player Random
        {
            get
            {
                var random = new Random();

                lock (Gate)
                {
                    return Players.ElementAt(random.Next(Players.Count - 1)).Value;
                }
            }
        }
    }
}