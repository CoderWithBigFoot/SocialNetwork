using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface ISearching : IDisposable
    {
        IEnumerable<ProfileDTO> ProfilesByPopularHashtags(IEnumerable<HashtagDTO> hashtags,int offset,int profilesCount,int hashtagsCount,string currentIdentityName);
        IEnumerable<PostDTO> PostsByHashtags(IEnumerable<HashtagDTO> hashtags,int offset,int postsCount,string currentIdentityName);
        IEnumerable<PostDTO> DefaultPostsSearching(int offset,int postsCount,int popularHashtagsCount,string identityName);
        IEnumerable<ProfileDTO> DefaultProfilesSearching(int offset,int profilesCount,int popularHashtagsCount,string identityName);
    }
}
