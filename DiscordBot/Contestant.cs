using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Contestant
    {
        public enum State : int { Alive, Dead };
        public string Name { get; private set; }
        public State Status;


        public Contestant (string name)
        {
            Name = name;
            Status = State.Alive;
        }
    }
}
