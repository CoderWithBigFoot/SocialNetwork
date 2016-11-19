using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.BasicInfo;
namespace SocialNetwork.BLL.Interfaces.ServicesProviders
{
   public interface IBasicInfo
    {
        IGetPostInfo PostInfoService {  get; }
        IGetProfileInfo ProfileInfoService { get; }
    }
}
