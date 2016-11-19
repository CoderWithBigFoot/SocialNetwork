using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.Statistics;
namespace SocialNetwork.BLL.Interfaces.ServicesProviders
{
   public interface IStatistics
    {
        IGetProfileStatistics GetProfileStatisticsService { get; }
    }
}
