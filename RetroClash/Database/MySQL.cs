using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RetroClash.Database.Models;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Manager;

namespace RetroClash.Database
{
    public class MySQL
    {
        private static string _connectionString;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public MySQL()
        {
            _connectionString = new MySqlConnectionStringBuilder
            {
                Server = Resources.Configuration.MySqlServer,
                Database = Resources.Configuration.MySqlDatabase,
                UserID = Resources.Configuration.MySqlUserId,
                Password = Resources.Configuration.MySqlPassword,
                SslMode = MySqlSslMode.None
            }.ToString();
        }

        public static async Task<long> MaxPlayerId()
        {
            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(PlayerId), 0) FROM player", connection))
                    {
                        cmd.Prepare();
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return 0;
            }
        }

        public static async Task<long> PlayerCount()
        {
            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM player", connection))
                    {
                        cmd.Prepare();
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return 0;
            }
        }

        public static async Task<long> AllianceCount()
        {
            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM clan", connection))
                    {
                        cmd.Prepare();
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return 0;
            }
        }

        public static async Task<long> MaxApiId()
        {
            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(Id), 0) FROM api", connection))
                    {
                        cmd.Prepare();
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return 0;
            }
        }

        public static async Task<long> MaxAllianceId()
        {
            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(ClanId), 0) FROM clan", connection))
                    {
                        cmd.Prepare();
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return 0;
            }
        }

        public static async Task<Player> CreatePlayer()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var player = new Player(await MaxPlayerId() + 1, Utils.GenerateToken);

                    using (var cmd =
                        new MySqlCommand(
                            $"INSERT INTO `player`(`PlayerId`, `Score`, `Language`, `Avatar`, `GameObjects`) VALUES ({player.AccountId}, {player.Score}, @language, @avatar, @objects)",
                            connection))
                    {
#pragma warning disable 618
                        cmd.Parameters?.Add("@language", player.Language);
                        cmd.Parameters?.Add("@avatar", JsonConvert.SerializeObject(player, Settings));
                        cmd.Parameters?.Add("@objects", player.LogicGameObjectManager.Json);
#pragma warning restore 618

                        cmd.Prepare();
                        await cmd.ExecuteReaderAsync();
                    }

                    await connection.CloseAsync();

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

        public static async Task<Alliance> CreateAlliance()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var alliance = new Alliance(await MaxAllianceId() + 1);

                    using (var cmd = new MySqlCommand($"INSERT INTO `clan`(`ClanId`, `Data`) VALUES ({alliance.Id}, @data)",
                        connection))
                    {
#pragma warning disable 618
                        cmd.Parameters?.Add("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                        cmd.Prepare();
                        await cmd.ExecuteReaderAsync();
                    }

                    await connection.CloseAsync();

                    return alliance;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return null;
            }
        }

        public static async Task CreateApiInfo()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var info = new ApiInfo
                    {
                        Id = await MaxApiId() + 1,
                        Online = Resources.Cache.Players.Count,
                        PlayersInDb = await PlayerCount(),
                        AlliancesInDb = await AllianceCount(),
                        Status = Configuration.Maintenance ? "Maintenance" : "Online",
                        CurrentDateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
                    };

                    using (var cmd =
                        new MySqlCommand($"INSERT INTO `api`(`Id`, `Info`) VALUES ({info.Id}, @info)",
                            connection))
                    {
#pragma warning disable 618
                        cmd.Parameters?.Add("@info", JsonConvert.SerializeObject(info));
#pragma warning restore 618

                        cmd.Prepare();
                        await cmd.ExecuteReaderAsync();
                    }

                    await connection.CloseAsync();
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }

        public static async Task<List<Player>> GetGlobalPlayerRanking()
        {
            var list = new List<Player>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();            

                    using (var cmd = new MySqlCommand("SELECT * FROM `player` ORDER BY `Score` DESC LIMIT 200", connection))
                    {
                        cmd.Prepare();
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var player = JsonConvert.DeserializeObject<Player>((string)reader["Avatar"], Settings);
                            player.Score = Convert.ToInt32(reader["Score"]);
                            player.Language = reader["Language"].ToString();
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string)reader["GameObjects"], Settings);

                            list.Add(player);
                        }
                    }

                    await connection.CloseAsync();

                    return list;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return list;
            }
        }

        public static async Task<List<Player>> GetLocalPlayerRanking(string language)
        {
            var list = new List<Player>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand($"SELECT * FROM `player` WHERE Language = '{language}' ORDER BY `Score` DESC LIMIT 200", connection))
                    {
                        cmd.Prepare();
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var player = JsonConvert.DeserializeObject<Player>((string)reader["Avatar"], Settings);
                            player.Score = Convert.ToInt32(reader["Score"]);
                            player.Language = reader["Language"].ToString();
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string)reader["GameObjects"], Settings);

                            list.Add(player);
                        }
                    }

                    await connection.CloseAsync();

                    return list;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return list;
            }
        }

        public static async Task<List<Alliance>> GetRandomAlliances(int limit)
        {
            var list = new List<Alliance>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();                   

                    using (var cmd = new MySqlCommand($"SELECT * FROM `clan` order by RAND() limit {limit}", connection))
                    {
                        cmd.Prepare();
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            list.Add(JsonConvert.DeserializeObject<Alliance>((string)reader["Data"], Settings));
                    }

                    await connection.CloseAsync();

                    return list;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return list;
            }
        }

        public static async Task<Player> GetPlayer(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Player player = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `player` WHERE PlayerId = '{id}'", connection))
                    {
                        cmd.Prepare();
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            player = JsonConvert.DeserializeObject<Player>((string)reader["Avatar"], Settings);
                            player.Score = Convert.ToInt32(reader["Score"]);
                            player.Language = reader["Language"].ToString();
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string)reader["GameObjects"], Settings);
                        }
                    }

                    await connection.CloseAsync();

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

        public static async Task<Alliance> GetAlliance(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Alliance alliance = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `clan` WHERE ClanId = '{id}'", connection))
                    {
                        cmd.Prepare();
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync()) { 
                            alliance = JsonConvert.DeserializeObject<Alliance>((string)reader["Data"], Settings);}
                    }

                    await connection.CloseAsync();

                    return alliance;
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);

                return null;
            }
        }

        public static async Task SavePlayer(Player player)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd =
                        new MySqlCommand(
                            $"UPDATE `player` SET `Score`='{player.Score}', `Language`='{player.Language}', `Avatar`=@avatar, `GameObjects`=@objects WHERE PlayerId = '{player.AccountId}'",
                            connection))
                    {
#pragma warning disable 618
                        cmd.Parameters?.Add("@avatar", JsonConvert.SerializeObject(player, Settings));
                        cmd.Parameters?.Add("@objects", player.LogicGameObjectManager.Json);
#pragma warning restore 618

                        cmd.Prepare();
                        await cmd.ExecuteReaderAsync();
                    }


                    await connection.CloseAsync();
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }

        public static async Task SaveAlliance(Alliance alliance)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand($"UPDATE `player` SET `Data`=@data WHERE ClanId = '{alliance.Id}'",
                        connection))
                    {
#pragma warning disable 618
                        cmd.Parameters?.Add("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                        cmd.Prepare();
                        await cmd.ExecuteReaderAsync();
                    }


                    await connection.CloseAsync();
                }
            }
            catch (Exception exception)
            {
                if (Configuration.Debug)
                    Console.WriteLine(exception);
            }
        }
    }
}