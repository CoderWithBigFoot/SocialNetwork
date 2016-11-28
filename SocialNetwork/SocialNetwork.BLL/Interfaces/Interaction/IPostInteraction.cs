using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IPostInteraction : IDisposable
    {
        //comments maybe here
        void Like(int postId,string identityName);
        void Repost(int postId, string identityName);
        void PublishPost(PostForPublicateDTO newPost,IEnumerable<HashtagDTO> hashtags);
    }
}
