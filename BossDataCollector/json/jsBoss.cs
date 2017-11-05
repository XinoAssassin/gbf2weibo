using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BossDataCollector
{

    public class Name
    {

        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("jp")]
        public string Jp { get; set; }
    }

    public class Boss
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("pic_url")]
        public string PicUrl { get; set; }

        [JsonProperty("lv")]
        public int Lv { get; set; }
    }

    public class JsBoss
    {

        [JsonProperty("Bosses")]
        public Boss[] Bosses { get; set; }
    }

}
