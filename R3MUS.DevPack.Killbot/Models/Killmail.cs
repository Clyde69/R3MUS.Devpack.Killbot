using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class Killmail
    {
        [JsonProperty(PropertyName = "killmail_id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "killmail_time")]
        public DateTime KillTime { get; set; }
        [JsonProperty(PropertyName = "victim")]
        public Victim Victim { get; set; }
        [JsonProperty(PropertyName = "attackers")]
        public List<Attacker> Attackers { get; set; }
        [JsonProperty(PropertyName = "solar_system_id")]
        public int SolarSystemId { get; set; }
    }

}
