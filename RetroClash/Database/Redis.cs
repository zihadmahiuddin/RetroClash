using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Logic;
using RetroClash.Logic.Manager;
using StackExchange.Redis;

namespace RetroClash.Database
{
    public class Redis
    {
        private static IDatabase _players;
        private static IDatabase _clans;
        private static IServer _server;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public Redis()
        {
            try
            {
                var config = new ConfigurationOptions {AllowAdmin = true, ConnectTimeout = 10000, ConnectRetry = 10};

                config.EndPoints.Add(Resources.Configuration.RedisServer, 6379);

                config.Password = Resources.Configuration.RedisPassword;

                _players = ConnectionMultiplexer.Connect(config).GetDatabase(0);
                _clans = ConnectionMultiplexer.Connect(config).GetDatabase(1);
                _server = ConnectionMultiplexer.Connect(config).GetServer(Resources.Configuration.RedisServer, 6379);
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }

        public static async Task CachePlayer(Player player)
        {
            try
            {
                await _players.StringSetAsync(player.AccountId.ToString(),
                    JsonConvert.SerializeObject(player, Settings) + "#:#:#:#" + player.LogicGameObjectManager.Json,
                    TimeSpan.FromHours(4));
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }

        public static async Task<Player> GetCachedPlayer(long id)
        {
            try
            {
                var data = (await _players.StringGetAsync(id.ToString())).ToString().Split("#:#:#:#".ToCharArray());

                using (var player = JsonConvert.DeserializeObject<Player>(data[0]))
                {
                    player.LogicGameObjectManager = JsonConvert.DeserializeObject<LogicGameObjectManager>(data[1]);

                    return player;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return null;
            }
        }
    }
}