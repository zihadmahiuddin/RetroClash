using System.Collections.Generic;
using RetroClash.Logic.Slots.Items;

namespace RetroClash.Logic.Slots
{
    public class Achievements : List<Achievement>
    {
        public new void Add(Achievement achievement)
        {
            if (!Contains(achievement))
                base.Add(achievement);
        }
    }
}
