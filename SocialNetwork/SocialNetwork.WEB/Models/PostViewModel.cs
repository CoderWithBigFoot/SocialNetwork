using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class PostViewModel
    {
        public int Id { set; get; }
        public string Content { get; set; }
        public System.DateTime PublishDate { get; set; }

        public int PublisherId { set; get; }
        public string PublisherName { set; get; }
        public string PublisherSername { set; get; }
        public string PublisherIdentityName { set; get; }

        public IEnumerable<HashtagViewModel> Hashtags { set; get; }
        public int Reposts { set; get; }
        public int Likes { set; get; }

    }
}