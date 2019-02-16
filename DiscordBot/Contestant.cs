using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Contestant object. Keeps track of a contestant's attributes and equipment.
    /// </summary>
    public class Contestant
    {
        public enum State : int { Alive, Dead };
        public string Name { get; private set; }
        public State Status;
        public List<string> Kills;


        public Contestant (string name)
        {
            Name = name;
            Status = State.Alive;
            Kills = new List<string>();
        }

        public void Die()
        {
            Status = State.Dead;
        }

        internal void AddKill(Contestant victim)
        {
            Kills.Add(victim.Name);
        }
    }
}
