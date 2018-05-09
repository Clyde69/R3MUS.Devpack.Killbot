using R3MUS.Devpack.Core;
using R3MUS.DevPack.Killbot.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace R3MUS.DevPack.Killbot.Services
{
    public class KillboardService : IKillboardService
    {
        private const string _uri = "https://redisq.zkillboard.com/listen.php";

        public bool Listen { get; set; }

        public event EventHandler<KillReceivedEventArgs> CorporateKillEventHandler;

        public KillboardService()
        {
        }

        public void ListenForKills()
        {
            try
            {
                Listen = true;

                while (Listen)
                {
                    HandleResponse(PollRedisQ());
                }
            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText(DateTime.Now.ToString("Killbot_dd_MM_yy_HH:mm:ss.log"), 
                    string.Concat(ex.Message, "\n\n", ex.StackTrace)
                    );
            }
        }

        private void HandleResponse(RedisQObject response)
        {
            if (response.Package != null)
            {
                if(IsCorporateKill(response.Package.KillMail.Attackers))
                {
                    RaiseKillEvent(new KillReceivedEventArgs()
                    {
                        Received = response,
                        Type = Enums.MailType.Kill
                    });
                }
                else if(IsCorporateLoss(response.Package.KillMail.Victim))
                {
                    RaiseKillEvent(new KillReceivedEventArgs()
                    {
                        Received = response,
                        Type = Enums.MailType.Loss
                    });
                }
            }
        }

        private void RaiseKillEvent(KillReceivedEventArgs eventArgs)
        {
            CorporateKillEventHandler?.Invoke(this, eventArgs);
        }

        private bool IsCorporateLoss(Victim victim)
        {
            return victim.CorporationId.Equals(Properties.Settings.Default.CorporationId);
        }

        private bool IsCorporateKill(IEnumerable<Attacker> attackers)
        {
            return attackers.Select(s => s.CorporationId).Distinct().Contains(Properties.Settings.Default.CorporationId);
        }

        private RedisQObject PollRedisQ()
        {
            string msg = string.Empty;
            try
            {
                msg = Web.BaseRequest(_uri);
                return Web.BaseRequest(_uri).Deserialize<RedisQObject>();
            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText(DateTime.Now.ToString("Killbot_dd_MM_yy_HH:mm:ss.log"),
                    string.Concat(ex.Message, "\n\n", ex.StackTrace)
                    );
                System.IO.File.WriteAllText(DateTime.Now.ToString("Killbot_RedisQResponse_dd_MM_yy_HH:mm:ss.log"),
                    msg
                    );
                return PollRedisQ();
            }
        }
    }
}
