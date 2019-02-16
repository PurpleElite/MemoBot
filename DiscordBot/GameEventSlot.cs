using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class GameEventSlot
    {
        public Contestant Contestant;
        public int Index;

        public GameEventSlot(int index)
        {
            Index = index;
        }

        public GameEventSlot()
        {
            Index = 0;
        }

        public override string ToString()
        {
            return Contestant.Name;
        }
    }
}
