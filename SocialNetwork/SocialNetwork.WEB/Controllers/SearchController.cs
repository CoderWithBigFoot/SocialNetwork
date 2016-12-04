using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.WEB.Models.ProfileViewModels;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using AutoMapper;
using SocialNetwork.WEB.Models;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SocialNetwork.WEB.Controllers
{
    public class SearchController : Controller
    {
        private IInteraction interaction;
        private IBasicInfo basicInfo;
        public SearchController(IInteraction interaction,IBasicInfo basicInfo) {
            this.interaction = interaction;
            this.basicInfo = basicInfo;
        }

        [HttpPost]
        public PartialViewResult SearchPartial() {
            return PartialView("/Partials/Searching");
        }

        [HttpPost]
        public object FindPosts(IEnumerable<string> hashtags,string searchingType) { //default,marks
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<string, HashtagDTO>().ConvertUsing(opt => new HashtagDTO() { Name = opt }));
                IEnumerable<PostDTO> resultPosts = null;

                switch (searchingType) {
                    case "marks":
                        resultPosts = interaction.SearchingService.PostsByHashtags(
           Mapper.Map<IEnumerable<HashtagDTO>>(hashtags), 0, 100, ControllerContext.HttpContext.User.Identity.Name);break;

                    case "default":resultPosts = interaction.SearchingService.DefaultPostsSearching(0, 100, 3, ControllerContext.HttpContext.User.Identity.Name);break;
                }

                
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PostDTO, PostViewModel>().ForMember("PublisherId", opt => opt.MapFrom(x => x.ProfileId));
                    cfg.CreateMap<HashtagDTO, HashtagViewModel>();
                });

                List<PostViewModel> result = Mapper.Map<IEnumerable<PostViewModel>>(resultPosts).ToList();

                ProfileDTO publisher;
                IEnumerable<HashtagDTO> resultHashtags;
                int Reposts = 0;
                int Likes = 0;

                foreach (var currentPost in result)
                {
                    publisher = basicInfo.ProfileInfoService.GetProfile(currentPost.PublisherId);
                    resultHashtags = basicInfo.PostInfoService.GetHashtagCollection(currentPost.Id);
                    Reposts = basicInfo.PostInfoService.GetRepostsCount(currentPost.Id);
                    Likes = basicInfo.PostInfoService.GetLikesCount(currentPost.Id);

                    if (hashtags != null)
                    {
                        foreach (var currentHashtag in resultHashtags)
                        {
                            currentPost.Hashtags.Add(currentHashtag.Name);
                        }
                    }
                    currentPost.Reposts = Reposts;
                    currentPost.Likes = Likes;
                    currentPost.PublisherIdentityName = publisher.IdentityName;
                    currentPost.PublisherName = publisher.Name;
                    currentPost.PublisherSername = publisher.Sername;

                }

                return JArray.FromObject(result);
            }
            catch (ArgumentNullException ex) {
                return JObject.FromObject(new { errorMessage = ex.Message });
            }
            //return JArray.FromObject(hashtags);
        }


    }
}