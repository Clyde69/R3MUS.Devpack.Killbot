using R3MUS.DevPack.Killbot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot.Models
{
    public class KillReceivedEventArgs : EventArgs
    {
        public RedisQObject Received { get; set; }
        public MailType Type { get; set; }
    }
}
