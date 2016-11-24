﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.WEB.Models.ProfileViewModels;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.WEB.Models;
using AutoMapper;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SocialNetwork.WEB.Controllers
{

    public class PostController : Controller
    {
        private IBasicInfo basicInfo;
        private IInteraction interaction;

        public PostController(IBasicInfo basicInfo, IInteraction interaction) {
            this.basicInfo = basicInfo;
            this.interaction = interaction;
        }
       
        [HttpPost]
        public void PublicateNewPost(PostForPublicateViewModel post) {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PostForPublicateViewModel,PostForPublicateDTO>();
                cfg.CreateMap<HashtagViewModel, HashtagDTO>();
            });
            PostForPublicateDTO postDto = Mapper.Map<PostForPublicateDTO>(post);
            postDto.PublisherIdentityName = HttpContext.User.Identity.Name;
            postDto.PublishDate = DateTime.Now;
            
            IEnumerable<HashtagDTO> hashtagsDto = Mapper.Map<IEnumerable<HashtagDTO>>(post.Hashtags);
            interaction.PostInteractionService.PublishPost(postDto, hashtagsDto);
        }

        [HttpPost]
        public object GetPublications(int offset,string identityName,int count=10) {
            try
            {
                IEnumerable<PostDTO> publishedPosts = interaction.ProfileInteractionService.GetPublications(identityName, offset, count);

                Mapper.Initialize(cfg => cfg.CreateMap<PostDTO,PostViewModel>().ForMember("PublisherId",opt=>opt.MapFrom(x=>x.ProfileId)));
                Mapper.Initialize(cfg => cfg.CreateMap<HashtagDTO, HashtagViewModel>());
                IEnumerable<PostViewModel> result = Mapper.Map<IEnumerable<PostViewModel>>(publishedPosts);

                ProfileDTO publisher;
                IEnumerable<HashtagDTO> hashtags;
                int Reposts = 0;
                int Likes = 0;

                foreach (var currentPost in result) {
                    publisher = basicInfo.ProfileInfoService.GetProfile(currentPost.PublisherId);
                    hashtags = basicInfo.PostInfoService.GetHashtagCollection(currentPost.Id);
                    Reposts = basicInfo.PostInfoService.GetRepostsCount(currentPost.Id);
                    Likes = basicInfo.PostInfoService.GetLikesCount(currentPost.Id);

                    currentPost.Hashtags = Mapper.Map<IEnumerable<HashtagViewModel>>(hashtags);
                    currentPost.Reposts = Reposts;
                    currentPost.Likes = Likes;
                    currentPost.PublisherIdentityName = publisher.IdentityName;
                    currentPost.PublisherName = publisher.Name;
                    currentPost.PublisherSername = publisher.Sername;
                }

                return JArray.FromObject(result);
            }
            catch (ProfileNotFoundException ex)
            {
                return JObject.FromObject(new { errorMessage = ex.Message });
            }
            catch (PublishedPostsNotFoundException ex) {
                return JObject.FromObject(new { errorMessage = ex.Message });
            }


        }

    }
}