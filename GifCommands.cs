using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using SteamBrosBot2.Services;

namespace SteamBrosBot2.Modules
{
    public class GifCommands : ModuleBase<SocketCommandContext>
    {

        private GifService _gifService;
        public GifCommands(GifService ser)
        {
            _gifService = ser;
        }

        [Command("gif", RunMode = RunMode.Async)]
        [Summary("Gives a random Gif with a specified search query")]
        public async Task GetRandomGif([Summary("Name of gif to search"), Remainder]string query)
        {
            await _gifService.GetGifBySearch(Context, query);
        }
    }
}
