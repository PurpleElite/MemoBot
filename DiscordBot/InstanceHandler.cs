using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Handles instances of active games.
    /// </summary>
    public class InstanceHandler
    {
        public Game activeGame { get; private set; }

        public InstanceHandler()
        {
            activeGame = new Game();
        }

        public bool AddContestant (Contestant contestant)
        {
            return activeGame.AddContestant(contestant);
        }
    }
}
