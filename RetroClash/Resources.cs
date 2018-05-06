using System;
using RetroClash.Database;
using RetroClash.Database.Caching;
using RetroClash.Files;
using RetroClash.Network;
using RetroClash.Protocol;

namespace RetroClash
{
    public class Resources : IDisposable
    {
        public static Gateway Gateway;
        public static PlayerCache PlayerCache;
        public static LeaderboardCache LeaderboardCache;
        public static Configuration Configuration;
        public static Levels Levels;

        public static Fingerprint Fingerprint;
        //public static Csv Csv;

        private static MessageFactory _messagefactory;
        private static CommandFactory _commandfactory;
        private static DebugCommandFactory _debugcommandfactory;
        private static MySQL _mysql;
        private static ApiService _apiService;

        public Resources()
        {
            Configuration = new Configuration();
            Configuration.Initialize();

            //Csv = new Csv();
            Fingerprint = new Fingerprint();

            _mysql = new MySQL();
            _messagefactory = new MessageFactory();
            _commandfactory = new CommandFactory();
            _debugcommandfactory = new DebugCommandFactory();
            _apiService = new ApiService();

            Levels = new Levels();
            PlayerCache = new PlayerCache();
            LeaderboardCache = new LeaderboardCache();

            Gateway = new Gateway();
        }

        public void Dispose()
        {
            //Csv = null;
            Gateway = null;
            PlayerCache = null;
            Configuration = null;
            Levels = null;
            Fingerprint = null;
            _messagefactory = null;
            _commandfactory = null;
            _debugcommandfactory = null;
            _mysql = null;
            _apiService = null;
        }
    }
}