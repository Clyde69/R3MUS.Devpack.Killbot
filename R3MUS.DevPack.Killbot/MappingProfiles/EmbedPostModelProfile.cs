using AutoMapper;
using R3MUS.Devpack.Discord;
using R3MUS.Devpack.Discord.Models.Embeds;
using R3MUS.DevPack.Killbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.MappingProfiles
{
    public class EmbedPostModelProfile : Profile
    {
        public EmbedPostModelProfile()
        {
            CreateMap<KillmailMessageDetails, EmbedPostModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => Properties.Settings.Default.KillbotName))
                .ForMember(dest => dest.Embeds, opt => opt.MapFrom(src => new List<Embed>()));

            CreateMap<KillmailMessageDetails, Embed>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.GetMessageText()))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => new Thumbnail() { url = src.ThumbUrl }))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
