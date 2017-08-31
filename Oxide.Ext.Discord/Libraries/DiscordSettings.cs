﻿using Oxide.Core.Libraries;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Libraries
{
    public class DiscordSettings : Library
    {
        [JsonProperty("API KEY/TOKEN")]
        public string ApiToken { get; set; }
        [JsonProperty("VERSION")]
        public string Version { get; set; }
    }
}
