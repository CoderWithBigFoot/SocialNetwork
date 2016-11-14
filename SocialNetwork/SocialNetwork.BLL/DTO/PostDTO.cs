using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO
{
   public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public System.DateTime PublishDate { get; set; }
        public int ProfileId { get; set; }
    }
}
