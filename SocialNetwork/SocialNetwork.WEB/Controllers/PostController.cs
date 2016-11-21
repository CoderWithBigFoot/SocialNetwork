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
            if (post.Hashtags == null) { post.Hashtags = new List<HashtagViewModel>(); }
            IEnumerable<HashtagDTO> hashtagsDto = Mapper.Map<IEnumerable<HashtagDTO>>(post.Hashtags);

            interaction.PostInteractionService.PublishPost(postDto, hashtagsDto);
        }

      
    }
}