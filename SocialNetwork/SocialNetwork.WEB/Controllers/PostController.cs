using System;
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
using System.Threading.Tasks;
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

        /*
         <div id="publicationsContainer">
             <div class="post-container common-info-block-text">
                    <div>Zheka Korsakas (identity name)</div>
                    <div> Publication's date</div>
                     <div class="hashtags-container">
                         <div class="hashtag">hashtaghashtaghashtaghashtaghashtag</div>
                         <div class="hashtag">hashtag</div>
                     </div>
                    <hr/>
                    <div>Content</div>
                    <hr />
                    <div>Reposts Likes</div>
                    
            </div>
           </div>
             */

        [HttpPost]
        public void PublicateNewPost(PostForPublicateViewModel post) {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PostForPublicateViewModel,PostForPublicateDTO>();
                //cfg.CreateMap<HashtagViewModel, HashtagDTO>();
            });
            PostForPublicateDTO postDto = Mapper.Map<PostForPublicateDTO>(post);
            postDto.PublisherIdentityName = HttpContext.User.Identity.Name;
            postDto.PublishDate = DateTime.Now;
            ICollection<HashtagDTO> hashtagsDto = new List<HashtagDTO>();

            if (post.Hashtags != null) {
                foreach (var hashtag in post.Hashtags) {
                    hashtagsDto.Add(new HashtagDTO() { Name = hashtag });
                }
            }

            interaction.PostInteractionService.PublishPost(postDto, hashtagsDto);
        }
        
        [HttpPost]
        public object Publications(int offset,string identityName,int count=10) {
            //return JArray.FromObject(new List<string>() { "first", "second", "third" });
            try
            {
                IEnumerable<PostDTO> publishedPosts = interaction.ProfileInteractionService.GetPublications(identityName, offset, count);

                Mapper.Initialize(cfg => {
                    cfg.CreateMap<PostDTO, PostViewModel>().ForMember("PublisherId", opt => opt.MapFrom(x => x.ProfileId));
                    cfg.CreateMap<HashtagDTO, HashtagViewModel>();
                    });
                
                List<PostViewModel> result = Mapper.Map<IEnumerable<PostViewModel>>(publishedPosts).ToList();
              
                ProfileDTO publisher;
                IEnumerable<HashtagDTO> hashtags;
                int Reposts = 0;
                int Likes = 0;

                foreach (var currentPost in result) {
                    publisher = basicInfo.ProfileInfoService.GetProfile(currentPost.PublisherId);
                    hashtags = basicInfo.PostInfoService.GetHashtagCollection(currentPost.Id);
                    Reposts = basicInfo.PostInfoService.GetRepostsCount(currentPost.Id);
                    Likes = basicInfo.PostInfoService.GetLikesCount(currentPost.Id);

                    if (hashtags != null) {
                        foreach (var currentHashtag in hashtags) {
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
            catch (ProfileNotFoundException ex)
            {               
                return JObject.FromObject(new { errorMessage = ex.Message });
            }
            catch (PublishedPostsNotFoundException ex) {           
                return JObject.FromObject(new { errorMessage = ex.Message });
            }
            

        }




        [HttpPost]
        public JObject Like(int postId,string identityName) {
            interaction.PostInteractionService.Like(postId, identityName);
            int newCount = basicInfo.PostInfoService.GetLikesCount(postId);
            return JObject.FromObject(new{ newLikesCount = newCount});
        }

    }
}