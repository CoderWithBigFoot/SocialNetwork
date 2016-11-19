using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.Registration;
namespace SocialNetwork.BLL.Interfaces.ServicesProviders
{
   public interface IRegistration : IDisposable
    {
        IRegistrator ProfileRegistrationService { get; }
    }
}
