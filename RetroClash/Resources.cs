using System;
using RetroClash.Database;
using RetroClash.Files;
using RetroClash.Network;
using RetroClash.Protocol;

namespace RetroClash
{
    public class Resources : IDisposable
    {
        public static Gateway Gateway;
        public static Cache Cache;
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
            Fingerprint = new Fingerprint();;
            Levels = new Levels();
            Cache = new Cache();
            _mysql = new MySQL();
            _messagefactory = new MessageFactory();
            _commandfactory = new CommandFactory();
            _debugcommandfactory = new DebugCommandFactory();
            _apiService = new ApiService(); 

            Gateway = new Gateway();
        }

        public void Dispose()
        {
            //Csv = null;
            Gateway = null;
            Cache = null;
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
