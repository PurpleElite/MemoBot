using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Game
    {
        List<Contestant> contestants;

        public Game ()
        {
            contestants = new List<Contestant>();
        }
        

        public bool AddContestant( Contestant contestant )
        {
            contestants.Add(contestant);
            return true;
        }
    }
}
