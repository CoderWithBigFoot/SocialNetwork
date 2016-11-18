using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IPostInteraction : IDisposable
    {
        //comments maybe here
        bool Like(int postId,string identityName);
        bool Repost(int postId, string identityName);
    }
}
