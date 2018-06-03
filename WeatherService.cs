﻿using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteamBrosBot2.Services.Weather
{
    public class WeatherService
    {
        private string weatherID;
        public WeatherService()
        {
            try
            {
                var configDict = ConfigService.getConfig();
                if (!configDict.TryGetValue("weather", out weatherID))
                {
                    Console.WriteLine("COULDN'T GET WEATHER API ID FROM CONFIG!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public async Task GetWeather(SocketCommandContext Context, string query)
        {
            try
            {
                var search = System.Net.WebUtility.UrlEncode(query);
                string response = "";
                using (var http = new HttpClient())
                {
                    response = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={search}&appid=753f64cfe23152e6b5267503d1b0ce7b&units=metric").ConfigureAwait(false);
                }
                var data = JsonConvert.DeserializeObject<WeatherData>(response);

                await Context.Channel.SendMessageAsync("", embed: data.GetEmbed());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Context.Channel.SendMessageAsync("I couldn't find the weather for your city. Maybe it doesn't exist");
            }
        }
    }
}
