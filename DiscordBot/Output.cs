using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// Output objects are used to carry output info to the IOHandler. 
    /// Currently they only carry text, but in the future they will carry images as well.
    /// </summary>
    public class Output
    {
        public string Text { get; private set; }

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
