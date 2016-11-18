using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Interaction;
using SocialNetwork.BLL.Infrastructure.Exceptions;

namespace SocialNetwork.BLL.Services.Interaction
{
   public class ProfileConfiguration : IProfileConfiguration
    {
        private IUnitOfWork uow;
        public ProfileConfiguration(IUnitOfWork uow) {
            this.uow = uow;
        }

        private bool CheckStringForLettersOnly(string str) {
            foreach (var symbol in str) {
                if (!char.IsLetter(symbol)) {
                    return false;
                }
            }
            return true;
                
        }

        private bool IsNullOrEmptyOrWhiteSpace(string str) {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str)) { return true; }
            return false;
        }


        public void ChangeName(string identityName, string newName) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            if (IsNullOrEmptyOrWhiteSpace(newName)) { throw new IncorrectProfileConfigurationException("Name cannot be a null or empty"); }
            if (!this.CheckStringForLettersOnly(newName)) { throw new IncorrectProfileConfigurationException("Name must be consist of letters only"); }
        

            profile.Name = newName;
            uow.Save();
        }

        public void ChangeSername(string identityName, string newSername) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            if (IsNullOrEmptyOrWhiteSpace(newSername)) { throw new IncorrectProfileConfigurationException("Sername cannot be a null or empty"); }
            if (!this.CheckStringForLettersOnly(newSername)) { throw new IncorrectProfileConfigurationException("Sername must be consist of letters only"); }

            profile.Sername = newSername;
            uow.Save();
        }

        public void ChangeDateOfBirth(string identityName,DateTime newDateOfBirth) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

            profile.DateOfBirth = newDateOfBirth;
            uow.Save();
        }

        public void ChangeStatus(string identityName, string newStatus) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            if (IsNullOrEmptyOrWhiteSpace(newStatus)) { throw new IncorrectProfileConfigurationException("Status cannot be a null or empty"); }

            profile.CustomInfo = newStatus;
            uow.Save();
        }


        public void Dispose() {
            this.uow.Dispose();
        }
    }
}
