using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class PostForPublicateViewModel
    {
        public string Content { set; get; }
        public IEnumerable<HashtagViewModel> Hashtags { set; get; }
    }
}