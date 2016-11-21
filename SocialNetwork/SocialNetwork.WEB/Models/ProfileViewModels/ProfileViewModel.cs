using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.BLL.DTO;
namespace SocialNetwork.WEB.Models.ProfileViewModels { 
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sername { get; set; }
        public string IdentityName { get; set; }
        public string CustomInfo { get; set; }
        public System.DateTime DateOfBirth { get; set; }

        public IEnumerable<ProfileDTO> Followers { set; get; }
        public IEnumerable<ProfileDTO> Subscriptions { set; get; }

    }
}