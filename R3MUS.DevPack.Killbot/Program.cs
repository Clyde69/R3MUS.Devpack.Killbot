using Ninject;
using R3MUS.DevPack.Killbot.Ninject;
using R3MUS.DevPack.Killbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace R3MUS.DevPack.Killbot
{
    public class Program
    {
        public static string NowString
        {
            get
            {
                return DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
            }
        }

        static void Main(string[] args)
        {
            CheckPaths();
            var kernel = new StandardKernel(new KillbotModule());
            var killbotService = kernel.Get<IKillbotService>();
            killbotService.Run();
        }

        static void CheckPaths()
        {
            if(!Directory.Exists(string.Concat(Directory.GetCurrentDirectory(), @"\logs\")))
            {
                Directory.CreateDirectory(string.Concat(Directory.GetCurrentDirectory(), @"\logs\"));
            };
        }
    }
}
