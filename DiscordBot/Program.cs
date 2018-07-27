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
    }
}