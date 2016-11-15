using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces
{
   public interface IGetStatistics : IDisposable
    {
        int PublishedPostsCount(string identityName);

        ICollection<HashtagDTO> AllHashtags(string identityName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityName">Identity name of user</param>
        /// <param name="count">Count of posts to select</param>
        /// <returns></returns>
        IEnumerable<HashtagDTO> MostPopularHashtags(string identityName,int count);

        Dictionary<HashtagDTO, int> EachHashtagCount(string identityName); // count of posts for each hashtag
        Dictionary<HashtagDTO, int> MostPopularHashtagsFrequency(string identityName,int count); // how often a user publishes posts on these hashtags(calculate from )
                                                                                    //DateTime.Now and date of first post publication
                                                                                        //per one day or another time span
                                                                                        //result int must be rounded to up
        
                                                                                 

    }
}
