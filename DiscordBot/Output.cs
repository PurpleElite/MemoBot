using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Output
    {
        public string Text { get;  private set; }

        public Output(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
