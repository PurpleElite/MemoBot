using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// This class manages all the game logic.
    /// </summary>
    public class Game
    {
        private List<Contestant> contestants;
        private List<Contestant> casualties;
        public int Day { get; private set; }



        public Game()
        {
            contestants = new List<Contestant>();
            casualties = new List<Contestant>();
            Day = 0;
        }

        /// <summary>
        /// Add a new contestant to the game. Cannot be done while game is in progress.
        /// </summary>
        /// <param name="contestant"></param>
        /// <returns></returns>
        public Output AddContestant(Contestant contestant)
        {
            // Check if game hasn't started yet
            if (Day == 0)
            {
                contestants.Add(contestant);
                return new Output("Contestant " + contestant.Name + " Added");
            }
            else
            {
                return new Output("Cannot add contestant, game is already in progress.");
            }
        }

        /// <summary>
        /// Return a list of all the contestants in the current game and their statuses.
        /// </summary>
        /// <returns></returns>
        public Output ListContestants()
        {
            string output = "";
            int count = 0;
            // Increment through list of contestants
            foreach (var contestant in contestants)
            {
                count++;
                output += count + ". Name: " + contestant.Name + ", Status: " + contestant.Status + '\n';
            }
            // Increment through list of casualties
            foreach (var contestant in casualties)
            {
                count++;
                output += count + ". Name: " + contestant.Name + ", Status: " + contestant.Status + '\n';
            }
            return new Output(output);
        }

        /// <summary>
        /// Start the game. This function might be unecessary. Might bundle it with Continue()
        /// </summary>
        /// <returns></returns>
        public Output Start()
        {
            Day = 1;
            casualties = new List<Contestant>();
            return Continue();
        }

        /// <summary>
        /// Proceed the game by another day and return thh results;
        /// </summary>
        /// <returns></returns>
        public Output Continue()
        {
            string output = "";
            output += "Day " + Day + '\n';
            // Calculate the maximum number that can die today. It's at most half the contestants or 1 less than the total
            int maxDead = Math.Min(contestants.Count(), contestants.Count() / 2);
            // Randomize the number of deaths
            int numDead = Utility.Rand.Next(1, maxDead);
            int numEventSlots = 0;

            List<GameEvent> events = new List<GameEvent>();
            // Randomly choose lethal events corresponding with the number of deaths
            while (numDead > 0)
            {
                int numVictims = Utility.Rand.Next(1, numDead + 1);
                // Find an event with the right number of deaths --This function is a mess, clean it up a little later--
                var selectedEvent = (from gameEvent in GameEventLibrary.GameEvents
                                     where gameEvent.Killers.Length == numVictims && gameEvent.PlayerCount + numEventSlots <= contestants.Count()
                                     select gameEvent).First();
                events.Add(selectedEvent);
                numEventSlots += selectedEvent.PlayerCount;
                numDead -= numVictims;
            }

            // Randomly choose victims
            while (numDead > 0)
            {
                int victimNum = Utility.Rand.Next(0, contestants.Count());
                if (contestants[victimNum].Status == Contestant.State.Alive)
                {
                    contestants[victimNum].Status = Contestant.State.Dead;
                    numDead--;
                }
            }
            // Report on all the contestant's statuses
            List<Contestant> deaths = new List<Contestant>();
            foreach (var contestant in contestants)
            {
                if (contestant.Status == Contestant.State.Alive)
                {
                    output += contestant.Name + " is alive.\n";
                }
                else if (contestant.Status == Contestant.State.Dead)
                {
                    output += contestant.Name + " has died.\n";
                    deaths.Add(contestant);
                }
            }
            // Move dead contestants over to the casualties list
            foreach (var contestant in deaths)
            {
                casualties.Add(contestant);
                contestants.Remove(contestant);
            }
            // Check to see if there's a winner. If not, move on to the next day.
            if (contestants.Count() == 1)
            {
                output += contestants[0].Name + " is the last survivor, they win!";
                Day = 0;
            }
            else
            {
                Day++;
            }
            return new Output(output);
        }
    }
}
