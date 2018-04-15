using System.Timers;

namespace RetroClash.Database
{
    internal class ApiService
    {
        public Timer Timer = new Timer(60000);

        public ApiService()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += TimerCallback;
            Timer.Start();
        }

        public async void TimerCallback(object state, ElapsedEventArgs args)
        {
            await MySQL.CreateApiInfo();
        }
    }
}