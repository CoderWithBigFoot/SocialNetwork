using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces.BasicInfo
{
   public interface IGetPostInfo : IDisposable
    {
        PostDTO GetPost(int postId);
        IEnumerable<HashtagDTO> GetHashtagCollection(int postId);
        IEnumerable<CommentDTO> GetComments(int postId,int offset,int count);
        int GetRepostsCount(int postId);
        int GetLikesCount(int postId);  
    }
}
