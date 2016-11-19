using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;

namespace SocialNetwork.BLL.Interfaces.Registration
{
   public interface IRegistration : IDisposable
    {
        void UserRegistration(ProfileForRegistrationDTO profile);
    }
}
