using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using SocialNetwork.BLL.Interfaces.Registration;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Interfaces;
using AutoMapper;
namespace SocialNetwork.BLL.Services.Registration
{
    public class RegistrationService : IRegistration
    {
        private IUnitOfWork uow;
        public RegistrationService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public void UserRegistration(ProfileForRegistrationDTO profile) {
            IEnumerable<SocialNetwork.DAL.EF.Profile> allProfiles = uow.Profiles.GetAllItems();
            foreach (var currentProfile in allProfiles) {
                if (currentProfile.IdentityName == profile.IdentityName) { throw new IncorrectProfileConfigurationException("Such identity name already exist"); }
            }

            Mapper.Initialize(cfg => cfg.CreateMap<ProfileForRegistrationDTO, SocialNetwork.DAL.EF.Profile>());
            uow.Profiles.Create(Mapper.Map<SocialNetwork.DAL.EF.Profile>(profile));
            uow.Save();
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
