using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SteamBrosBot2.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        // DiscordSocketClient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public StartupService(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config)
        {

            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            //Get the discord token from the config file
            string discordToken = _config["tokens:discord"];
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Please enter your bot's token into the `_configuration.json` found in the applications root directory");

            //Login to Discord
            await _discord.LoginAsync(TokenType.Bot, discordToken);
            //Connect to the websocket
            await _discord.StartAsync();

            //Load commands and modules into the command service
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            //Display what video game the Bot is "playing"
            await _discord.SetGameAsync(Utilities.GetAlert("GAMESTATUS"));
        }
    }
}
