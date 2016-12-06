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
    public class ProfileInteractionController : Controller
    {
        // GET: ProfileInteraction
        private IInteraction interaction;
        private IBasicInfo basicInfo;
        public ProfileInteractionController(IInteraction interaction, IBasicInfo basicInfo) {
            this.interaction = interaction;
            this.basicInfo = basicInfo;
        }

        [HttpPost]
        public object Subscribe(string targetIdentity) {
            try
            {
                int subscribers = basicInfo.ProfileInfoService.GetSubscriptions(ControllerContext.HttpContext.User.Identity.Name).Count;
                string result = "";

                interaction.ProfileInteractionService.Subscribe(ControllerContext.HttpContext.User.Identity.Name, targetIdentity);
                if (subscribers > basicInfo.ProfileInfoService.GetSubscriptions(ControllerContext.HttpContext.User.Identity.Name).Count)
                {
                    result = "Successfully unsubscribed";
                }
                if(subscribers < basicInfo.ProfileInfoService.GetSubscriptions(ControllerContext.HttpContext.User.Identity.Name).Count) {
                    result = "Successfully subscribed";
                }
                return JObject.FromObject(new { completionMessage = result });
            }
            catch (ProfileNotFoundException) {
                return JObject.FromObject(new { completionMessage = "Such profile dont exist." });
            }
        }

        [HttpPost]
        public JArray News(int offset, int count = 10)
        { //here is shet
            ICollection<ProfileDTO> subscriptions = basicInfo.ProfileInfoService.GetSubscriptions(HttpContext.User.Identity.Name);
            ICollection<PostDTO> allPosts = new List<PostDTO>();
            IEnumerable<PostDTO> postsToDisplay = null;

            foreach (var current in subscriptions)
            {
                foreach (var post in interaction.ProfileInteractionService.GetPublications(current.IdentityName, 0, 100))
                {
                    allPosts.Add(post);
                }
            }

            postsToDisplay = allPosts.OrderByDescending(x => x.PublishDate).Skip(offset).Take(count);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PostDTO, PostViewModel>().ForMember("PublisherId", opt => opt.MapFrom(x => x.ProfileId));
                cfg.CreateMap<HashtagDTO, HashtagViewModel>();
            });

            List<PostViewModel> result = Mapper.Map<IEnumerable<PostViewModel>>(postsToDisplay).ToList();

            ProfileDTO publisher;
            IEnumerable<HashtagDTO> hashtags;
            int Reposts = 0;
            int Likes = 0;

            foreach (var currentPost in result)
            {
                publisher = basicInfo.ProfileInfoService.GetProfile(currentPost.PublisherId);
                hashtags = basicInfo.PostInfoService.GetHashtagCollection(currentPost.Id);
                Reposts = basicInfo.PostInfoService.GetRepostsCount(currentPost.Id);
                Likes = basicInfo.PostInfoService.GetLikesCount(currentPost.Id);

                if (hashtags != null)
                {
                    foreach (var currentHashtag in hashtags)
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



    }
}