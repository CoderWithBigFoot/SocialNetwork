using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.Statistics;
using SocialNetwork.BLL.Services.Statistics;
using Ninject.Modules;
namespace SocialNetwork.BLL.Infrastructure.NinjectModules
{
   public class ProfileStatisticsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGetProfileStatistics>().To<GetProfileStatisticsService>();
        }
    }
}
