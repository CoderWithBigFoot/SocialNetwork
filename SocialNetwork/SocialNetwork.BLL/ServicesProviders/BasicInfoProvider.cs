using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.BLL.Services.BasicInfo;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Interfaces.BasicInfo;
namespace SocialNetwork.BLL.ServicesProviders
{
   public class BasicInfoProvider : IBasicInfo
    {
        private GetPostInfoService postInfoService;
        private GetProfileInfoService profileInfoService;
        private IUnitOfWork uow;
        public BasicInfoProvider(IUnitOfWork uow) {
            this.uow = uow;
        }

        public IGetPostInfo PostInfoService {
            get {
                if (postInfoService == null) {
                    postInfoService = new GetPostInfoService(this.uow);
                }
                return postInfoService;
            }
        }

        public IGetProfileInfo ProfileInfoService {
            get {
                if (profileInfoService == null) {
                    profileInfoService = new GetProfileInfoService(uow);
                }
                return profileInfoService;
            }
        }
        




    }
}
