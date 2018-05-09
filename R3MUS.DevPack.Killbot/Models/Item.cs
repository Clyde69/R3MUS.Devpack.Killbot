using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "item_type_id")]
        public int ItemTypeId { get; set; }
        [JsonProperty(PropertyName = "singleton")]
        public int Singleton { get; set; }
        [JsonProperty(PropertyName = "flag")]
        public int Flag { get; set; }
        [JsonProperty(PropertyName = "quantity_dropped")]
        public int QuantityDropped { get; set; }
        [JsonProperty(PropertyName = "quantity_destroyed")]
        public int QuantityDestroyed { get; set; }
    }
}
