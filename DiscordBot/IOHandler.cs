using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class IOHandler
    {
        /// <summary>
        /// Takes user input and interprets it.
        /// </summary>
        /// <param name="message"></param>
        public void Input(SocketMessage message)
        {
            // Break the message into words seperated by spaces
            string[] args = message.Content.Split(' ');
            Output output;
            switch (args[0].ToLower())
            {
                // sample command
                case "+hi":
                    message.Channel.SendMessageAsync("Hi " + message.Author.Username + "!");
                    break;
                // some example commands
                case "+enter":
                    // check to see if there is an argument provided
                    if (args.Length > 1 )
                    {
                        string name = args[1];
                        for (int i = 2; i < args.Length; i++)
                        {
                            name += " " + args[i];
                        }
                        Contestant contestant = new Contestant(name);
                        output = Program.instanceHandler.AddContestant(contestant);
                        message.Channel.SendMessageAsync(output.ToString());
                    }
                    else
                    {
                        message.Channel.SendMessageAsync("Syntax: +enter name");
                    }
                    break;
                case "+proceed":
                    output = Program.instanceHandler.Proceed();
                    message.Channel.SendMessageAsync(output.ToString());
                    break;
                case "+roster":
                    output = Program.instanceHandler.ListContestants();
                    message.Channel.SendMessageAsync(output.ToString());
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
