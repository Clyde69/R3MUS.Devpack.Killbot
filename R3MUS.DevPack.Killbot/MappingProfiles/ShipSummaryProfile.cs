using AutoMapper;
using R3MUS.Devpack.ESI.Models.Universe;
using R3MUS.DevPack.Killbot.Models;

namespace R3MUS.DevPack.Killbot.MappingProfiles
{
    public class ShipSummaryProfile : Profile
    {
        public ShipSummaryProfile()
        {
            CreateMap<ItemType, ItemSummary>();
        }
    }
}
