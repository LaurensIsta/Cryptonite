using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;


namespace Cryptonite
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        //vars
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
          

        //run bot
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_client)
                .BuildServiceProvider();
            //SECRET TOKEN |REDACT FOR PUBLIC USE|
            string botToken = "REDACTED";


            //event subs
            _client.Log += log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync(); 

            await Task.Delay(-1);
        }

        //Message logger
        private Task log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }


        
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }


        //Command handler
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            
            //ignore certain messages
            if(message is null || message.Author.IsBot)
            {
                return;
            }
            
            
            //recognise beginning of message
            int argPos = 0;


            //Set prefix
            if (message.HasStringPrefix("$$", ref argPos) || message.HasMentionPrefix(_client.CurrentUser,ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
