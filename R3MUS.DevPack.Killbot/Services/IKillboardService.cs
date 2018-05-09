using System;
using R3MUS.DevPack.Killbot.Models;

namespace R3MUS.DevPack.Killbot.Services
{
    public interface IKillboardService
    {
        bool Listen { get; set; }

        event EventHandler<KillReceivedEventArgs> CorporateKillEventHandler;

        void ListenForKills();
    }
}