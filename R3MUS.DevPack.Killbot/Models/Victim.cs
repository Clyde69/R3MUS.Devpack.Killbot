using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class Victim : BaseCharacter
    {
        [JsonProperty(PropertyName = "damage_taken")]
        public int DamageTaken { get; set; }
        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }
        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }
    }

}
