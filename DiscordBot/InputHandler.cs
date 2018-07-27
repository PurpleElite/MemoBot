using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class InputHandler
    {
        /// <summary>
        /// Takes user input and interprets it.
        /// </summary>
        /// <param name="message"></param>
        public void Input(SocketMessage message)
        {
            // Break the message into words seperated by spaces
            string[] args = message.Content.Split(' ');
            switch (args[0].ToLower())
            {
                // sample command
                case "+hi":
                    message.Channel.SendMessageAsync("Hi " + message.Author.Username + "!");
                    break;
                // some example commands
                case "+info":
                    // check to see if there is an argument provided
                    if (args.Length > 1 )
                    {
                        int index;
                        // if it's a number give the user info on whatever corresponds to that index
                        if (Int32.TryParse(args[1], out index))
                        {
                            // e.g. give info on a soldier
                        }
                    }
                    else
                    {
                        //give general info on the party
                    }
                    break;
                case "+gachapull":
                    break;
                case "+help":
                    message.Channel.SendMessageAsync("There is no helping you.");
                    break;
                default:
                    message.Channel.SendMessageAsync("Unrecognized command, try +help.");
                    break;
            }
        }
    }
}
