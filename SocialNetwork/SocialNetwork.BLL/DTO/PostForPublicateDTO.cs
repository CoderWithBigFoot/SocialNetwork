using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO
{
   public class PostForPublicateDTO
    {
       public string Contenet { set; get; }
       public System.DateTime PublishDate { get; set; }
       public string PublisherIdentityName { set; get; }
    }
}
