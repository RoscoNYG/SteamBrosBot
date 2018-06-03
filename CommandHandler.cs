using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

namespace SteamBrosBot2.Services
{
    class CommandHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;

        //DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from IServiceProvider
        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            //Ensure the message is from a User or Bot
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            //Ignore self when checking commands
            if (msg.Author.Id == _discord.CurrentUser.Id) return;

            //Create the command context
            var context = new SocketCommandContext(_discord, msg);

            //Check if the message has a valid command prefix
            int argPos = 0;
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                //Execute the Command
                var result = await _commands.ExecuteAsync(context, argPos, _provider);

                //If not successful, reply with Error
                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
