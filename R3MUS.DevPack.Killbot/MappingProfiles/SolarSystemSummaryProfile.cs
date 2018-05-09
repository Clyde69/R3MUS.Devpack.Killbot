using AutoMapper;
using R3MUS.Devpack.ESI.Models.Universe;
using R3MUS.DevPack.Killbot.Models;

namespace R3MUS.DevPack.Killbot.MappingProfiles
{
    public class SolarSystemSummaryProfile : Profile
    {
        public SolarSystemSummaryProfile()
        {
            CreateMap<SolarSystem, SolarSystemSummary>()
                .ForMember(dest => dest.RegionId, opt => opt.Ignore())
                .ForMember(dest => dest.RegionName, opt => opt.Ignore());
        }
    }
}
