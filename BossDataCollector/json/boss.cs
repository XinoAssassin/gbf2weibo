using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BossDataCollector
{

    public class Image
    {

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class TranslatedName
    {

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class RaidBoss
    {

        [JsonProperty("lastSeen")]
        public object LastSeen { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("translatedName")]
        public TranslatedName TranslatedName { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
    }

    public class Bosses
    {

        [JsonProperty("raidBosses")]
        public RaidBoss[] RaidBosses { get; set; }
    }

}
