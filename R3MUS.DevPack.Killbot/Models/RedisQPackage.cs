using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class RedisQPackage
    {
        [JsonProperty(PropertyName = "killID")]
        public int KillId { get; set; }
        [JsonProperty(PropertyName = "killmail")]
        public Killmail KillMail { get; set; }
        [JsonProperty(PropertyName = "zkb")]
        public ZkbData ZKBData { get; set; }
    }
}
