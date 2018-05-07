using System;

namespace RetroClash.Extensions
{
    public class Utils
    {
        public static string GenerateToken
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 40; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static string GenerateRc4Key
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 38; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static int GetSecondsUntilNextMonth
        {
            get
            {
                var now = DateTime.UtcNow;

                return (int) (new DateTime(now.Year, now.Month + 1, 1, now.Hour,
                                  now.Minute, now.Second) - now).TotalSeconds;
            }
        }
    }
}