using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class StatisticsViewModel
    {
        public int PublishedPostsCount { set; get; }
        public ICollection<KeyValuePair<string, int>> EachHashtagCount { set; get; } = new List<KeyValuePair<string, int>>();
        public ICollection<KeyValuePair<string, int>> PostsCountByMostPopularHasthags { set; get; } = new List<KeyValuePair<string, int>>();

    }
}