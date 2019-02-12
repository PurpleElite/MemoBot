using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
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


        public Output AddContestant(Contestant contestant)
        {
            contestants.Add(contestant);
            return new Output("Contestant " + contestant.Name + " Added");
        }

        public Output ListContestants()
        {
            string output = "";
            int count = 0;
            foreach (var contestant in contestants)
            {
                count++;
                output += count + ". Name: " + contestant.Name + ", Status: " + contestant.Status + '\n';
            }
            foreach (var contestant in casualties)
            {
                count++;
                output += count + ". Name: " + contestant.Name + ", Status: " + contestant.Status + '\n';
            }
            return new Output(output);
        }

        public Output Start()
        {
            Day = 1;
            casualties = new List<Contestant>();
            return Continue();
        }

        public Output Continue()
        {
            string output = "";
            output += "Day " + Day + '\n';
            int maxDead = Math.Min(contestants.Count(), contestants.Count() / 2);
            int numDead = Utility.Rand.Next(1, maxDead);
            while (numDead > 0)
            {
                int victimNum = Utility.Rand.Next(0, contestants.Count());
                if (contestants[victimNum].Status == Contestant.State.Alive)
                {
                    contestants[victimNum].Status = Contestant.State.Dead;
                    numDead--;
                }
            }
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
                    casualties.Add(contestant);
                    deaths.Add(contestant);
                }
            }
            foreach (var contestant in deaths)
            {
                contestants.Remove(contestant);
            }
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
