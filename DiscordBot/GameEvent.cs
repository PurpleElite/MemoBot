using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Events that happens within a game. It takes a list of characters and then changes their states and outputs flavor text
    /// such as "Player1 and Player2 beat up Player3".
    /// </summary>
    public class GameEvent
    {
        private FormattableString flavorText;
        public int PlayerCount { get; private set; }
        private List<int> killers;
        private List<int> killed;

        public GameEvent(FormattableString flavorText, int playerCount)
        {
            this.flavorText = flavorText;
            PlayerCount = playerCount;
        }

        public GameEvent(FormattableString flavorText)
        {
            this.flavorText = flavorText;
            PlayerCount = 1;
        }


        /// <summary>
        /// Activate the event with a given list of participants. The participant count must match the events expected playerCount.
        /// </summary>
        /// <param name="participants"></param>
        /// <returns></returns>
        public string Trigger(List<Contestant> participants)
        {
            if (participants.Count != PlayerCount)
            {
                return "Error: Incorrect number of participants in Event";
            }

            // Insert participants into the flavor text
            foreach (GameEventSlot slot in flavorText.GetArguments())
            {
                slot.Contestant = participants[slot.Index];
            }

            // Kill off the victims (if any) and attribute the kills to any killers
            int i = 0;
            foreach (var killedID in killed)
            {
                var victim = participants[killedID];
                victim.Die();
                if (killers.Count != 0)
                { 
                    participants[killers[i]].AddKill(victim);
                    i++;
                    i %= killers.Count;
                }
            }

            return flavorText.ToString();
        }
    }
}
