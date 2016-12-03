using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
namespace SocialNetwork.WEB.Models
{
    public class StatisticsViewModel
    {
        public int PublishedPostsCount { set; get; }
        public List<KeyValuePair<string, int>> EachHashtagCount { set; get; } = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, int>> MostPopularHasthags { set; get; } = new List<KeyValuePair<string, int>>();
        

    }
}