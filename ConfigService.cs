using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;

namespace SteamBrosBot2.Services
{
    public static class ConfigService
    {
        private static JsonSerializer jSerializer = new JsonSerializer();
        private static ConcurrentDictionary<string, string> configDict = new ConcurrentDictionary<string, string>();


        public static void InitializeLoader()
        {
            jSerializer.Converters.Add(new JavaScriptDateTimeConverter());
            jSerializer.NullValueHandling = NullValueHandling.Ignore;
        }


        public static void LoadConfig()
        {
            if (!File.Exists("_configuration.json"))
            {
                throw new IOException("COULDNT FIND CONFIG FILE!");
            }

            using (StreamReader sr = File.OpenText("_configuration.json"))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                configDict = jSerializer.Deserialize<ConcurrentDictionary<string, string>>(reader);
            }
        }

        public static ConcurrentDictionary<string, string> getConfig()
        {
            return configDict;
        }
    }
}
