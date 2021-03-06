﻿using System;
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
        public ICollection<KeyValuePair<string, int>> EachHashtagCount { set; get; } = new List<KeyValuePair<string, int>>();
        public ICollection<KeyValuePair<string, int>> MostPopularHashtags { set; get; } = new List<KeyValuePair<string, int>>();
        public ICollection<KeyValuePair<string, double>> MostPopularHashtagsFrequencies { set; get; } = new List<KeyValuePair<string, double>>();

    }
}