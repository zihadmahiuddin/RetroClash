using System;
using Newtonsoft.Json;

namespace RetroClash.Logic.Manager
{
    public class LogicShield
    {
        [JsonProperty("shield_duration")]
        public int ShieldDuration { get; set; }

        [JsonProperty("shield_end_time")]
        public DateTime EndTime { get; set; }

        public void SetShield(int type)
        {
            if (!IsShieldActive)
            {
                ShieldDuration = GetShieldDurationByType(type);
                EndTime = DateTime.Now.AddHours(ShieldDuration);
            }
            else
            {
                var shieldDuration = GetShieldDurationByType(type);
                ShieldDuration += shieldDuration;
                EndTime = EndTime.AddHours(shieldDuration);
            }
        }

        public void RemoveShield()
        {
            ShieldDuration = 0;
            EndTime = DateTime.Now;
        }

        [JsonIgnore]
        public bool IsShieldActive => EndTime >= DateTime.Now;

        [JsonIgnore]
        public int ShieldSecondsLeft => (int)(EndTime - DateTime.Now).TotalSeconds;

        public int GetShieldDurationByType(int type)
        {
            switch (type)
            {
                case 20000000:
                    return 24;
                case 20000001:
                    return 48;
                case 20000002:
                    return 168;
                default:
                    return 0;
            }
        }
    }
}