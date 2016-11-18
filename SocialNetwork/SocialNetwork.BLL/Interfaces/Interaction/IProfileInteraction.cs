using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IProfileInteraction : IDisposable
    {
        void Subscribe(string callerIdentityName,string targetIdentityName); 
        void RemoveSubscription(string callerIdentityName, string targetIdentityName);
        void RemoveFollower(string callerIdentityName, string targetIdentityName);
    }
}
