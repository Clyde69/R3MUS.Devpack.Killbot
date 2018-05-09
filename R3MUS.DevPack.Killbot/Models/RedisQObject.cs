using Newtonsoft.Json;

namespace R3MUS.DevPack.Killbot.Models
{
    public class RedisQObject
    {
        [JsonProperty(PropertyName = "package")]
        public RedisQPackage Package { get; set; }
    }
}
