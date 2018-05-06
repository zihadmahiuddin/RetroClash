using System.Timers;
using RetroClash.Logic;

namespace RetroClash.Database.Caching
{
    public class LeaderboardCache
    {
        private readonly Timer _timer = new Timer(10000);

        public Alliance[] GlobalAlliances = new Alliance[200];
        public Player[] GlobalPlayers = new Player[200];

        public LeaderboardCache()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += TimerCallback;
            _timer.Start();
        }

        public async void TimerCallback(object state, ElapsedEventArgs args)
        {
            var currentGlobalAllianceRanking = await MySQL.GetGlobalAllianceRanking();
            for (var i = 0; i < currentGlobalAllianceRanking.Count; i++)
                GlobalAlliances[i] = currentGlobalAllianceRanking[i];

            var currentGlobalPlayerRanking = await MySQL.GetGlobalPlayerRanking();
            for (var i = 0; i < currentGlobalPlayerRanking.Count; i++)
                GlobalPlayers[i] = currentGlobalPlayerRanking[i];
        }
    }
}