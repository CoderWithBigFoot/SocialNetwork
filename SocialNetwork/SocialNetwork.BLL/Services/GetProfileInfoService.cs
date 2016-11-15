using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using AutoMapper;
namespace SocialNetwork.BLL.Services
{
   public class GetProfileInfoService : IGetProfileInfoService
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
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            if (profile != null) {
                ICollection<SocialNetwork.DAL.EF.Profile> followers = profile.Followers;
                Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>());
                return Mapper.Map<ICollection<SocialNetwork.DAL.EF.Profile>, ICollection<ProfileDTO>>(followers);
            }
            return null;
        }

        public ICollection<ProfileDTO> GetSubscriptions(string identityName) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            if (profile != null) {
                ICollection<SocialNetwork.DAL.EF.Profile> subscriptions = profile.SubscribedOn;
                Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Profile,ProfileDTO>());
                return Mapper.Map<ICollection<SocialNetwork.DAL.EF.Profile>, ICollection<ProfileDTO>>(subscriptions);
            }
            return null;
        }




    }
}
