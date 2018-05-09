using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class Attacker : BaseCharacter
    {
        [JsonProperty(PropertyName = "security_status")]
        public decimal SecurityStatus { get; set; }
        [JsonProperty(PropertyName = "final_blow")]
        public bool HadFinalBlow { get; set; }
        [JsonProperty(PropertyName = "damage_done")]
        public int DamageDone { get; set; }
        [JsonProperty(PropertyName = "faction_id")]
        public int? FactionId { get; set; }
        [JsonProperty(PropertyName = "weapon_type_id")]
        public int WeaponTypeId { get; set; }
    }
}
