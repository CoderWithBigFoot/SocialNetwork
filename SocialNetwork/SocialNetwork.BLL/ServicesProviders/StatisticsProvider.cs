using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.Statistics;
using SocialNetwork.BLL.Services.Statistics;
using SocialNetwork.BLL.ServicesProviders;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.DAL.Interfaces;

namespace SocialNetwork.BLL.ServicesProviders
{
   public class StatisticsProvider : IStatistics
    {
        private GetProfileStatisticsService profileStatisticsService;
        private IUnitOfWork uow;

        public StatisticsProvider(IUnitOfWork uow) {
            this.uow = uow;
        }

        public IGetProfileStatistics GetProfileStatisticsService {
            get {
                if (profileStatisticsService == null) {
                    profileStatisticsService = new GetProfileStatisticsService(uow);
                }
                return profileStatisticsService;
            }
        }
    }
}
