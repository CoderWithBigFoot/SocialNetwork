﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;

namespace SocialNetwork.BLL.Interfaces
{
   public interface IGetProfileInfoService
    {
        ProfileDTO GetProfile(int id);
        ProfileDTO GetProfile(string identityName);

        ICollection<ProfileDTO> GetFollowers(string identityName);
        ICollection<ProfileDTO> GetSubscriptions(string identityName);

        int GetCountOfFollowers(string identityName);
        int GetCountOfSubscriptions(string identityName);
    }
}