using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.BLL.Services.Registration;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Registration;
namespace SocialNetwork.BLL.ServicesProviders
{
   public class RegistrationProvider : IRegistration
    {
        private ProfileRegistrationService registrationService;
        private IUnitOfWork uow;

        public RegistrationProvider(IUnitOfWork uow) {
            this.uow = uow;
        }

       public IRegistrator ProfileRegistrationService {
            get {
                if (registrationService == null) {
                    registrationService = new ProfileRegistrationService(uow);
                }
                return registrationService;
            }
        }

        public void Dispose() {
            uow.Dispose();
        }

    }
}
