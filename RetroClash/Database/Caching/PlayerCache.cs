using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Logic;

namespace RetroClash.Database.Caching
{
    public class PlayerCache
    {
        private readonly object _gate = new object();

        public Dictionary<long, Player> Players = new Dictionary<long, Player>();

        public Player Random
        {
            get
            {
                lock (_gate)
                {
                    if (Players.Count <= 1) return null;
                    return Players.ElementAt(new Random().Next(0, Players.Count - 1)).Value;
                }
            }
        }

        public void AddPlayer(Player player)
        {
            lock (_gate)
            {
                try
                {
                    if (Players.ContainsKey(player.AccountId))
                        Players.Remove(player.AccountId);

                    player.Timer.Elapsed += player.SaveCallback;
                    player.Timer.Start();

                    Players.Add(player.AccountId, player);
                }
                catch (Exception exception)
                {
                    if (Configuration.Debug)
                        Console.WriteLine(exception);
                }
            }
        }

        public async Task<Player> GetPlayer(long id)
        {
            lock (_gate)
            {
                if (Players.ContainsKey(id))
                    return Players[id];
            }

            return await MySQL.GetPlayer(id);
        }

        public void RemovePlayer(long id)
        {
            lock (_gate)
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
    }
}