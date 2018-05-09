using R3MUS.DevPack.Killbot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class KillmailMessageDetails
    {
        private const string _green = "#00FF00";
        private const string _red = "#FF0000";

        private const string _title = "R3MUS {0} at {1}";
        private const string _victimLine = "Victim: {0} lost a {1} in {2} ({3}).";
        private const string _lastBlowLine = "Last Blow: {0} in a {1}.";
        private const string _wolfListLine = "Wolf {0} got on the board in a {1}!";
        private const string _valueLine = "Loss value: {0} ISK.";

        private const string _thumbUrl = "https://image.eveonline.com/Render/{0}_64.png";
        private const string _zkbUrl = "https://zkillboard.com/kill/{0}/";

        public MailType Type { get; set; }
        public DateTime Time { get; set; }
        public string SystemName { get; set; }
        public string RegionName { get; set; }
        public KillEntitySummary VictimSummary { get; set; }
        public KillEntitySummary FinalAttackerSummary { get; set; }
        public List<KillEntitySummary> AttackersSummary { get; set; }
        public decimal LossValue { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }

        public string PostColour
        {
            get
            {
                return Type == MailType.Kill ? _green : _red;
            }
        }

        public string GetTitle()
        {
            return string.Format(_title, Enum.GetName(typeof(MailType), Type), Time.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public string GetMessageText()
        {
            List<string> messageLines = new List<string>();

            messageLines.Add(string.Format(_victimLine, VictimSummary.EntityName, VictimSummary.ShipTypeName,
                SystemName, RegionName));

            if(FinalAttackerSummary.EntityName != string.Empty)
            {
                messageLines.Add(string.Format(_lastBlowLine, FinalAttackerSummary.EntityName, FinalAttackerSummary.ShipTypeName));
            }

            if (Type == MailType.Kill)
            {
                AttackersSummary.ForEach(attacker => messageLines.Add(string.Format(_wolfListLine, attacker.EntityName, attacker.ShipTypeName)));
            }

            messageLines.Add(string.Format(_valueLine, LossValue.ToString("N2")));

            return String.Join("\n", messageLines.ToArray());
        }
        
        public KillmailMessageDetails(KillReceivedEventArgs eventArgs, List<Devpack.ESI.Models.Character.Summary> characters,
            List<Devpack.ESI.Models.Corporation.Summary> corporations, List<ItemSummary> ships, SolarSystemSummary system)
        {
            Type = eventArgs.Type;

            Time = eventArgs.Received.Package.KillMail.KillTime;
            LossValue = eventArgs.Received.Package.ZKBData.TotalValue;
            Url = string.Format(_zkbUrl, eventArgs.Received.Package.KillId.ToString());
            SystemName = system.Name;
            RegionName = system.RegionName;
            ThumbUrl = string.Format(_thumbUrl, eventArgs.Received.Package.KillMail.Victim.ShipTypeId);

            VictimSummary = GenerateEntityDetails(eventArgs.Received.Package.KillMail.Victim, 
                characters, corporations, ships);

            var attackers = eventArgs.Received.Package.KillMail.Attackers;
            
            FinalAttackerSummary = GenerateEntityDetails(attackers.First(w => w.HadFinalBlow),
                characters, corporations, ships);

            if(Type == MailType.Kill)
            {
                AttackersSummary = new List<KillEntitySummary>();
                attackers.Where(w => !w.HadFinalBlow && w.CorporationId == Properties.Settings.Default.CorporationId).ToList()
                    .ForEach(f => AttackersSummary.Add(GenerateEntityDetails(f, characters, corporations, ships))
                );
            }

            OutputToConsole();
        }

        private void OutputToConsole()
        {
            Console.WriteLine(GetTitle());
            Console.WriteLine(GetMessageText());
            Console.WriteLine();
        }
        
        private KillEntitySummary GenerateEntityDetails(BaseCharacter character, List<Devpack.ESI.Models.Character.Summary> characters,
            List<Devpack.ESI.Models.Corporation.Summary> corporations, List<ItemSummary> ships)
        {
            if(!character.CharacterId.HasValue && !character.CorporationId.HasValue)
            {
                return new KillEntitySummary();
            }
            return new KillEntitySummary()
            {
                EntityName = character.CharacterId.HasValue
                        ? characters.First(w => w.Id == character.CharacterId.Value).Name
                        : corporations.FirstOrDefault(w => w.Id == character.CorporationId.Value).Name,
                ShipTypeName = ships.First(w => w.Id == character.ShipTypeId).Name
            };
        }
    }
}
