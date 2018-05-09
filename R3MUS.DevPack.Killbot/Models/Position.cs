using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class Position
    {
        [JsonProperty(PropertyName = "x")]
        public decimal X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public decimal Y { get; set; }
        [JsonProperty(PropertyName = "z")]
        public decimal Z { get; set; }
    }    
}
