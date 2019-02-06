using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Contestant
    {
        public String Name { get; private set; }

        public Contestant (String name)
        {
            Name = name;
        }
    }
}
