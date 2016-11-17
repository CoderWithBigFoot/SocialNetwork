using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IProfileInteraction : IDisposable
    {
        bool Subscribe(string identityName); // for who
        bool RemoveSubscription(string identityName);
        bool RemoveFollower(string identityName);
    }
}
