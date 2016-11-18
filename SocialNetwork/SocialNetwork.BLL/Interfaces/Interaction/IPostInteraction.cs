﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IPostInteraction : IDisposable
    {
        //comments maybe here
        bool Like(int postId,string identityName);
        bool Repost(int postId, string identityName);
        bool PublishPost(PostForPublicateDTO newPost,IEnumerable<HashtagDTO> hashtags);
        bool RemovePost(PostDTO toRemovePost);
    }
}
