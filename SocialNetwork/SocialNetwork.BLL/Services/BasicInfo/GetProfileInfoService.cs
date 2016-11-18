using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Interfaces.BasicInfo;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Infrastructure.Exceptions;
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
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.Get(id);
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

            Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
            return Mapper.Map<ProfileDTO>(profile);
        }

        public ProfileDTO GetProfile(string identityName) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.Get(id);
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

            Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
            return Mapper.Map<ProfileDTO>(profile);
        }



        public ICollection<ProfileDTO> GetFollowers(string identityName) {
            
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
                if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

                    ICollection<SocialNetwork.DAL.EF.Profile> followers = profile.Followers;
                    Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
                    return Mapper.Map<ICollection<ProfileDTO>>(followers);                
                   
        }
        public ICollection<ProfileDTO> GetSubscriptions(string identityName) {
            
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
                if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }    

                    ICollection<SocialNetwork.DAL.EF.Profile> subscriptions = profile.SubscribedOn;
                    Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
                    return Mapper.Map<ICollection<ProfileDTO>>(subscriptions);   
           
            
        }

        public void Dispose() {
            uow.Dispose();
        }


    }
}
