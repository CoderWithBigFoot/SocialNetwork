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
        IEnumerable<ProfileDTO> ProfilesByPopularHashtags(IEnumerable<HashtagDTO> hashtags,int offset,int hashtagsCount);
        IEnumerable<PostDTO> PostsByHashtags(IEnumerable<HashtagDTO> hashtags,int offset);

        IEnumerable<PostDTO> DefaultPostsSearching(int offset);
        IEnumerable<ProfileDTO> DefaultProfilesSearching(int offset);

    }
}
