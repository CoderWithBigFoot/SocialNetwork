using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces.Statistics
{
   public interface IGetProfileStatistics : IDisposable
    {
        int PublishedPostsCount(string identityName);
        IEnumerable<HashtagDTO> AllHashtags(string identityName);
        IEnumerable<KeyValuePair<HashtagDTO,int>>MostPopularHashtags(string identityName,int count=3);
        ICollection<KeyValuePair<HashtagDTO, int>> EachHashtagCount(string identityName); // count of posts for each hashtag
        Dictionary<HashtagDTO, double> MostPopularHashtagsFrequency(string identityName,int count=3,TimeInterval interval=TimeInterval.Day);
        // how often a user publishes posts on these hashtags(calculate from )
        //DateTime.Now and date of first post publication
        //per one day or another interval
        //result int must be rounded to up

        //Dictionary<HashtagDTO, int> SelectedHashtagsCount(string identityName, IEnumerable<HashtagDTO> hashtags);                                                                   
    }
}
