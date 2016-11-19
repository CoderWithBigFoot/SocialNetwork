using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO
{
   public class ProfileForRegistrationDTO
    {
        public string Name { get; set; }
        public string Sername { get; set; }
        public string IdentityName { get; set; }
        public string CustomInfo { get; set; }
        public System.DateTime DateOfBirth { get; set; }
    }
}
