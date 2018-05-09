using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class ZkbData
    {
        [JsonProperty(PropertyName = "locationID")]
        public int LocationId { get; set; }
        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }
        [JsonProperty(PropertyName = "fittedValue")]
        public decimal FittedValue { get; set; }
        [JsonProperty(PropertyName = "totalValue")]
        public decimal TotalValue { get; set; }
        [JsonProperty(PropertyName = "points")]
        public int Points { get; set; }
        [JsonProperty(PropertyName = "npc")]
        public bool WasNpc { get; set; }
        [JsonProperty(PropertyName = "solo")]
        public bool WasSolo { get; set; }
        [JsonProperty(PropertyName = "awox")]
        public bool WasAwox { get; set; }
        [JsonProperty(PropertyName = "href")]
        public string Url { get; set; }
    }
}
