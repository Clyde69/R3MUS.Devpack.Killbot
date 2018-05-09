﻿using Ninject;
using R3MUS.DevPack.Killbot.Ninject;
using R3MUS.DevPack.Killbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.DevPack.Killbot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new KillbotModule());
            var killbotService = kernel.Get<IKillbotService>();
            killbotService.Run();
        }
    }
}
