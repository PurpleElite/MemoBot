using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace DiscordBot
{
    public static class Program
    {
        public static DiscordSocketClient Client { get; private set; }
        public static InputHandler inputHandler = new InputHandler();

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig());
            Client.Log += Logger;

            /* Load in the bot token here. This is a long random number that is the secret key for logging into your bot.
             * Emphasis on "secret". Don't commit this token to your git repository.*/
            string token = File.ReadAllText(Path.Combine("..", "..", "..", "Token.txt")); 

            Client.MessageReceived += MessageReceived;

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        /// <summary>
        /// This code gets called when a channel the bot is recieves a message.
        /// </summary>
        private static async Task MessageReceived(SocketMessage message)
        {
            // We ignore any messages that aren't from users, are written by this bot, or don't have the proper prefix.
            if (message.Source != MessageSource.User || message.Author.Id == Client.CurrentUser.Id || message.Content[0] != '+')
            {
                return;
            }

            inputHandler.Input(message);

            return;
        }

        /// <summary>
        /// Prints out debug info, warnings, and error messages to the console window.
        /// </summary>
        private static Task Logger(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();

            return Task.CompletedTask;
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