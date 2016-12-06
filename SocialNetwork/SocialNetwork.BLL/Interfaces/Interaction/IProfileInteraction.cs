using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IProfileInteraction : IDisposable
    {
        void Subscribe(string callerIdentityName,string targetIdentityName); 
        void RemoveFollower(string callerIdentityName, string targetIdentityName);

        IEnumerable<PostDTO> GetPublications(string identityName,int offset,int count);
        IEnumerable<PostDTO> GetReposts(string identityName,int offset,int count);
        void RemoveRepost(int postId,string identityName);
    }
}
