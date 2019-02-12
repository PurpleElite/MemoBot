using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Handles instances of active games. Currently it doesn't do much because there is only ever one active game.
    /// </summary>
    public class InstanceHandler
    {
        public Game ActiveGame { get; private set; }

        public InstanceHandler()
        {
            ActiveGame = new Game();
        }

        public Output AddContestant(Contestant contestant)
        {
            return ActiveGame.AddContestant(contestant);
        }

        public Output Proceed()
        {
            if (ActiveGame.Day == 0)
            {
                return ActiveGame.Start();
            }
            else
            {
                return ActiveGame.Continue();
            }
        }

        public Output ListContestants()
        {
            return ActiveGame.ListContestants();
        }
    }
}
