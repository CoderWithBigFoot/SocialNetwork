using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class StatisticsViewModel
    {
        public int PublishedPostsCount { set; get; }
        //public IEnumerable<string> AllHashtags { set; get; } = new List<string>();
        public Dictionary<string, int> EachHashtagCount { set; get; } = new Dictionary<string, int>();
        public Dictionary<string, int> PostsCountByMostPopularHasthags { set; get; } = new Dictionary<string, int>();

    }
}