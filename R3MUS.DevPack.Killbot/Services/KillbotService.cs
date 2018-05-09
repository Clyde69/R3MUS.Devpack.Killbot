using R3MUS.DevPack.Killbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Services
{
    public class KillbotService : IKillbotService
    {
        private readonly IESIService _esiService;
        private readonly IKillboardService _killboardService;
        private readonly ICommunicationService _communicationsService;

        public KillbotService(IESIService esiService, IKillboardService killboardService, 
            ICommunicationService communicationsService)
        {
            _esiService = esiService;
            _killboardService = killboardService;
            _communicationsService = communicationsService;

            Console.Title = string.Concat(_esiService.ParentCorporation.Name, " Killbot");

            if (!System.IO.Directory.Exists("logs"))
            {
                System.IO.Directory.CreateDirectory("logs");
            }

            _killboardService.CorporateKillEventHandler += CorporateKillEventHandler;
        }

        public void Run()
        {
            Console.WriteLine("Listening for kills & losses...");

            _killboardService.ListenForKills();

            Console.ReadLine();
        }

        private void CorporateKillEventHandler(object sender, KillReceivedEventArgs eventArgs)
        {
            LoadCharacters(eventArgs.Received.Package.KillMail);
            LoadCorporations(eventArgs.Received.Package.KillMail);
            LoadShips(eventArgs.Received.Package.KillMail);
            LoadSystem(eventArgs.Received.Package.KillMail);

            if (eventArgs.Received.Package.KillMail.Victim.CharacterId.HasValue)
            {
                try
                {
                    _communicationsService.PostMessage(CreateSummary(eventArgs));
                }
                catch { }
            }
        }

        private KillmailMessageDetails CreateSummary(KillReceivedEventArgs eventArgs)
        {
            var victim = eventArgs.Received.Package.KillMail.Victim;
            var attackers = eventArgs.Received.Package.KillMail.Attackers;

            return new KillmailMessageDetails(eventArgs, _esiService.Characters, _esiService.Corporations,
                _esiService.Ships, _esiService.Systems.First(w => w.Id == eventArgs.Received.Package.KillMail.SolarSystemId));
        }

        private void LoadSystem(Killmail killMail)
        {
            _esiService.LoadSystem(killMail.SolarSystemId);
        }

        private void LoadShips(Killmail killMail)
        {
            var shipIds = killMail.Attackers.Where(w => w.ShipTypeId.HasValue && w.ShipTypeId.Value > 0)
                .Select(s => s.ShipTypeId.Value).ToList();
            if (killMail.Victim.ShipTypeId.HasValue && killMail.Victim.ShipTypeId > 0)
            {
                shipIds.Add(killMail.Victim.ShipTypeId.Value);
            }
            _esiService.LoadShips(shipIds);
        }

        private void LoadCharacters(Killmail killMail)
        {
            var characterIds = killMail.Attackers.Where(w => w.CharacterId.HasValue)
                .Select(s => s.CharacterId.Value).ToList();
            if (killMail.Victim.CharacterId.HasValue)
            {
                characterIds.Add(killMail.Victim.CharacterId.Value);
            }
            _esiService.LoadCharacters(characterIds);
        }

        private void LoadCorporations(Killmail killMail)
        {
            var corporationIds = killMail.Attackers.Where(w => !w.CharacterId.HasValue && w.CorporationId.HasValue)
                .Select(s => s.CorporationId.Value).ToList();
            if (!killMail.Victim.CharacterId.HasValue)
            {
                corporationIds.Add(killMail.Victim.CorporationId.Value);
            }
            _esiService.LoadCorporations(corporationIds);
        }
    }
}
