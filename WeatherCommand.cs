using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using SteamBrosBot2.Services;
using SteamBrosBot2.Services.Weather;
using System.Threading.Tasks;

namespace SteamBrosBot2.Modules
{
    public class WeatherCommand : ModuleBase<SocketCommandContext>
    {
        private WeatherService _weatherService;

        public WeatherCommand(WeatherService ser)
        {
            _weatherService = ser;
        }

        [Command("weather", RunMode = RunMode.Async)]
        [Summary("Gets the weather of the specified city")]
        public async Task GetWeather([Summary("City to get the weather for"), Remainder]string query)
        {
            await _weatherService.GetWeather(Context, query);
        }
    }
}
