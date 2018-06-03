using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SteamBrosBot2
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}