using AutoMapper;
using R3MUS.Devpack.ESI.Models.Corporation;

namespace R3MUS.DevPack.Killbot.MappingProfiles
{
    public class CorporationProfile : Profile
    {
        public CorporationProfile()
        {
            CreateMap<Detail, Summary>();
        }
    }
}
