using R3MUS.DevPack.Killbot.Models;

namespace R3MUS.DevPack.Killbot.Services
{
    public interface ICommunicationService
    {
        void PostMessage(KillmailMessageDetails killmail);
    }
}
