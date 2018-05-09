namespace R3MUS.DevPack.Killbot.Models
{
    public class SolarSystemSummary : ItemSummary
    {
        public long ConstellationId { get; set; }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
