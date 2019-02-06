using Discord.WebSocket;
using System;
using System.Collections.Generic;
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
                        Contestant contestant = new Contestant(name);
                        if (Program.instanceHandler.AddContestant(contestant))
                        {

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

        static void SampleCode(string[] args)
        {
            // Some hard coded image file path. This obviously won't work on your computer so pick a new image.
            System.Drawing.Image contestant = System.Drawing.Image.FromFile(@"E:\Dropbox\Personal\Pictures\ANIME IS REAL.jpg");

            string contestantName = "EARTH";

            // Our drawing canvas. This is what we'll draw everything to.
            Bitmap canvas = new Bitmap(600, 800);

            // Load in a font to use for text.
            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            var solidColorBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));

            // Our random number generator for picking outcomes.
            Random random = new Random();

            // Create a list of possible outcomes.
            string[] outcomes = new string[]
            {
                "{0} died under mysterious circumstances.",
                "Poor {0}, thought of ants and died.",
                "{0} sat around enjoying the weather."
            };

            using (Graphics g = Graphics.FromImage(canvas))
            {
                // Draw a background color.
                g.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, canvas.Width, canvas.Height));

                // Draw our contestant.
                int contestantX = 100;
                int contestantY = 100;
                g.DrawImage(contestant, contestantX, contestantY, contestant.Width, contestant.Height);

                // Draw the text describing what happened to them.
                int outcomeIndex = random.Next() % outcomes.Length;
                int textX = contestantX;
                int textY = contestantY + contestant.Height;
                // string.Format lets us swap out {0} for the contestant's name.
                string text = string.Format(outcomes[outcomeIndex], contestantName);
                g.DrawString(text, font, solidColorBrush, textX, textY);
            }

            // Save the image to the working directory of the application.
            // Aka "{wherever you saved this project}\HungerGamesRandomizer\bin\Debug"
            canvas.Save("Output.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
