using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Interfaces.BasicInfo;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;

using AutoMapper;
namespace SocialNetwork.BLL.Services.BasicInfo
{
   public class GetProfileInfoService : IGetProfileInfo
    {
        private IUnitOfWork uow;

        public GetProfileInfoService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public ProfileDTO GetProfile(int id) {
            Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
            return Mapper.Map<ProfileDTO>(uow.Profiles.Get(id));
        }

        public ProfileDTO GetProfile(string identityName) {
            Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
            return Mapper.Map<ProfileDTO>(uow.Profiles.FindByIdentityName(identityName));
        }



        public ICollection<ProfileDTO> GetFollowers(string identityName) {
            try
            {
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

                    ICollection<SocialNetwork.DAL.EF.Profile> followers = profile.Followers;
                    Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
                    return Mapper.Map<ICollection<ProfileDTO>>(followers);                
            }
            catch (NullReferenceException) {
                return null;
            }           
        }
        public ICollection<ProfileDTO> GetSubscriptions(string identityName) {
            try
            {
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
                
                    ICollection<SocialNetwork.DAL.EF.Profile> subscriptions = profile.SubscribedOn;
                    Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
                    return Mapper.Map<ICollection<ProfileDTO>>(subscriptions);   
            }
            catch (NullReferenceException) {
                return null;
            }
            
        }

        public void Dispose() {
            uow.Dispose();
        }


    }
}
