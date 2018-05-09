using System.Collections.Generic;
using R3MUS.DevPack.Killbot.Models;

namespace R3MUS.DevPack.Killbot.Services
{
    public interface IESIService
    {
        List<Devpack.ESI.Models.Character.Summary> Characters { get; set; }
        List<Devpack.ESI.Models.Corporation.Summary> Corporations { get; set; }
        List<ItemSummary> Ships { get; set; }
        Devpack.ESI.Models.Corporation.Detail ParentCorporation { get; set; }
        List<SolarSystemSummary> Systems { get; set; }

        void AddParentCorporation();
        void LoadCharacters(IEnumerable<long> characterIds);
        void LoadCorporations(IEnumerable<long> corporationIds);
        void LoadShips(IEnumerable<long> shipIds);
        void LoadSystem(int solarSystemId);
    }
}