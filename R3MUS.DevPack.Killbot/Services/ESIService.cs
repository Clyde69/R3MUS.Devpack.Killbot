using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using R3MUS.DevPack.Killbot.Models;
using R3MUS.Devpack.ESI.Models.Universe;

namespace R3MUS.DevPack.Killbot.Services
{
    public class ESIService : IESIService
    {
        private readonly IMapper _mapper;

        public List<Devpack.ESI.Models.Character.Summary> Characters { get; set; }
        public List<Devpack.ESI.Models.Corporation.Summary> Corporations { get; set; }
        public List<ItemSummary> Ships { get; set; }

        public List<SolarSystemSummary> Systems { get; set; }

        public Devpack.ESI.Models.Corporation.Detail ParentCorporation { get; set; }

        public ESIService(IMapper mapper)
        {
            _mapper = mapper;

            Characters = new List<Devpack.ESI.Models.Character.Summary>();
            Corporations = new List<Devpack.ESI.Models.Corporation.Summary>();
            Ships = new List<ItemSummary>() { new ItemSummary() { Id = 0, Name = "Unknown Ship" } };
            Systems = new List<SolarSystemSummary>();
            
            AddParentCorporation();
        }

        public void AddParentCorporation()
        {
            ParentCorporation = new Devpack.ESI.Models.Corporation.Detail(Properties.Settings.Default.CorporationId);

            Corporations.Add(_mapper.Map<Devpack.ESI.Models.Corporation.Summary>(ParentCorporation));
        }

        public void LoadShips(IEnumerable<long> shipIds)
        {
            shipIds = shipIds.Distinct().Where(w => !Characters.Select(s => s.Id).ToList().Contains(w));
            shipIds.ToList().ForEach(f =>
                Ships.Add(_mapper.Map<ItemSummary>(
                    new Devpack.ESI.Models.Universe.ItemType(f))));
        }

        public void LoadCharacters(IEnumerable<long> characterIds)
        {
            characterIds = characterIds.Distinct().Where(w => !Characters.Select(s => s.Id).ToList().Contains(w));
            characterIds.ToList().ForEach(f => 
                Characters.Add(_mapper.Map<Devpack.ESI.Models.Character.Summary>(
                    new Devpack.ESI.Models.Character.Detail(f))));
        }

        public void LoadCorporations(IEnumerable<long> corporationIds)
        {
            corporationIds = corporationIds.Distinct().Where(w => !Corporations.Select(s => s.Id).ToList().Contains(w));
            corporationIds.ToList().ForEach(f =>
                Corporations.Add(_mapper.Map<Devpack.ESI.Models.Corporation.Summary>(
                    new Devpack.ESI.Models.Corporation.Detail(f))));
        }

        public void LoadSystem (int solarSystemId)
        {
            if(!Systems.Select(s => s.Id).Contains(solarSystemId))
            {
                var system = _mapper.Map<SolarSystemSummary>(new SolarSystem(solarSystemId));
                LoadRegion(system);                
                Systems.Add(system);
            }
        }

        private SolarSystemSummary LoadRegion(SolarSystemSummary source)
        {
            if(Systems.Any(w => w.ConstellationId == source.ConstellationId))
            {
                source.RegionId = Systems.FirstOrDefault(w => w.ConstellationId == source.ConstellationId).RegionId;
                source.RegionName = Systems.FirstOrDefault(w => w.ConstellationId == source.ConstellationId).RegionName;
            }
            else
            {
                source.RegionId = new Constellation(source.ConstellationId).RegionId;
                source.RegionName = new Region(source.RegionId).Name;
            }
            return source;
        }
    }
}
