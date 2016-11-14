using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;

namespace SocialNetwork.BLL.Interfaces
{
   public interface IGetProfileInfoService
    {
        ProfileDTO GetProfileById(int id);
        ProfileDTO GetProfileByIdentityName(string identityName);

        ProfileDTO GetFollowers(int id);
        ProfileDTO GetSubscriptions(int id);


    }
}
