using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SteamBrosBot2.Services;
using SteamBrosBot2.Services.Weather;

namespace SteamBrosBot2
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(string[] args)
        {
            // Create a new instance of the config builder
            var builder = new ConfigurationBuilder()
                // Specify the default location for the config file
                .SetBasePath(AppContext.BaseDirectory)
                // Add this (json encoded) file to the configuration    
                .AddJsonFile("_configuration.json");
            // Build the configuration
            Configuration = builder.Build();                
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            // Create a new instance of a service collection
            var services = new ServiceCollection();             
            ConfigureServices(services);

            // Build the service provider
            var provider = services.BuildServiceProvider();
            // Start the logging service
            provider.GetRequiredService<LoggingService>();
            // Start the command handler service
            provider.GetRequiredService<CommandHandler>();

            // Start the startup service
            await provider.GetRequiredService<StartupService>().StartAsync();
            // Keep the program alive
            await Task.Delay(-1);                               
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            // Add discord to the collection
            {
                // Tell the logger to give Verbose amount of info
                LogLevel = LogSeverity.Verbose,
                // Cache 1,000 messages per channel
                MessageCacheSize = 1000
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            // Add the command service to the collection
            {
                // Tell the logger to give Verbose amount of info
                LogLevel = LogSeverity.Verbose,
                // Force all commands to run async by default
                DefaultRunMode = RunMode.Async,
                // Ignore case when executing commands
                CaseSensitiveCommands = false
            }))
            // Add startupservice to the collection
            .AddSingleton<StartupService>()
            // Add loggingservice to the collection
            .AddSingleton<LoggingService>()
            // Add random to the collection
            .AddSingleton<Random>()
            //Add the CommandHandler to the collection
            .AddSingleton<CommandHandler>()
            //Add GifService to the collection
            .AddSingleton<GifService>()
            //Add WeatherService to the collection
            .AddSingleton<WeatherService>()
            // Add the configuration to the collection
            .AddSingleton(Configuration);
        }
    }
}