using Ninject.Modules;
using Ninject.Extensions.Conventions;
using AutoMapper;
using Ninject;
using System;
using R3MUS.Devpack.Core.AutoMapper;
using R3MUS.DevPack.Killbot.Services;

namespace R3MUS.DevPack.Killbot.Ninject
{
    public class KillbotModule : NinjectModule
    {
        private IKernel _kernel;

        public override void Load()
        {
            _kernel = this.Kernel;

            ConfigureBindings();
            ConfigureMappings();
        }

        private void ConfigureBindings()
        {
            _kernel.Load(new CoreMapperBindings());

            _kernel.Bind(x => {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface()
                    .Configure(y => y.InThreadScope());
            });

            _kernel.Bind<ICommunicationService>().To<DiscordService>();
        }

        private void ConfigureMappings()
        {
            var profiles = new AutoMapperProfiles();
            var types = profiles.LoadProfiles();

            Mapper.Initialize(cfg => {

                cfg.ConstructServicesUsing(t => _kernel.Get(t));

                foreach (var type in types)
                {
                    cfg.AddProfile((Profile)Activator.CreateInstance(type));
                }
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
