using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class PostForPublicateViewModel
    {
        public string Content { set; get; }
        public System.DateTime PublishDate { get; set; }
        public string PublisherIdentityName { set; get; }
    }
}