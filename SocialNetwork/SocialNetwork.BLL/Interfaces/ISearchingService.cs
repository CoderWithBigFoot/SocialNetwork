﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.BLL.Interfaces
{
   public interface ISearchingService : IDisposable
    {
        IEnumerable<ProfileDTO> ProfilesByHashtags(IEnumerable<HashtagDTO> hashtags,int offset);
        IEnumerable<PostDTO> PostsByHashtags(IEnumerable<HashtagDTO> hashtags,int offset);

        IEnumerable<PostDTO> DefaultPostsSearching(int offset);
        IEnumerable<ProfileDTO> DefaultProfilesSearching(int offset);

    }
}
