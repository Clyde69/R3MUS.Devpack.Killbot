using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public abstract class BaseCharacter
    {
        [JsonProperty(PropertyName = "character_id")]
        public long? CharacterId { get; set; }
        [JsonProperty(PropertyName = "corporation_id")]
        public long? CorporationId { get; set; }
        [JsonProperty(PropertyName = "alliance_id")]
        public long? AllianceId { get; set; }
        [JsonProperty(PropertyName = "ship_type_id")]
        public long? ShipTypeId { get; set; }
    }
}
