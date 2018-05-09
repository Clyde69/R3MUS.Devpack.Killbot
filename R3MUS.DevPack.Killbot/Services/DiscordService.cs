using R3MUS.DevPack.Killbot.Models;
using R3MUS.Devpack.Discord;
using AutoMapper;
using R3MUS.Devpack.Discord.Models.Embeds;
using System.Drawing;

namespace R3MUS.DevPack.Killbot.Services
{
    public class DiscordService : ICommunicationService
    {
        private readonly IMapper _mapper;

        public DiscordService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void PostMessage(KillmailMessageDetails killmail)
        {
            var embedPostModel = _mapper.Map<EmbedPostModel>(killmail);
            var embed = _mapper.Map<Embed>(killmail);
            embed.SetColour(ColorTranslator.FromHtml(killmail.PostColour));

            embedPostModel.Embeds.Add(embed);

            Client.PostEmbed(embedPostModel, killmail.Type == Enums.MailType.Kill 
                ? Properties.Settings.Default.KillWebhook : Properties.Settings.Default.LossWebhook);
        }
    }
}
