using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Slot for string interpolation in GameEvent flavortext
    /// </summary>
    public class Slot
    {
        public Contestant Contestant;
        public int Index;

        public Slot(int index)
        {
            Index = index;
        }

        public Slot()
        {
            Index = 0;
        }

        public override string ToString()
        {
            return Contestant.Name;
        }
    }
}
