using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces
{
   public interface IGetStatistics
    {
        int PostedPostsCount(string identityName);

        ICollection<HashtagDTO> AllHashtags(string identityName);
        ICollection<HashtagDTO> MostPopularHashtags(string identityName,int count);

        Dictionary<HashtagDTO, int> EachHashtagCount(string identityName); // count of posts for each hashtag
        Dictionary<HashtagDTO, int> MostPopularHashtagsFrequency(string identityName);

    }
}
