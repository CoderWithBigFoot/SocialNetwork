using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using SocialNetwork.BLL.ServicesProviders;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.BLL.Interfaces.Statistics;
using SocialNetwork.BLL.Services.Statistics;
namespace SocialNetwork.BLL.Infrastructure.NinjectModules
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBasicInfo>().To<BasicInfoProvider>();
            Bind<IInteraction>().To<InteractionProvider>();
            Bind<IRegistration>().To<RegistrationProvider>();
            Bind<IStatistics>().To<StatisticsProvider>();
        }
    }
}