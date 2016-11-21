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

        public void PublicateNewPost(PostForPublicateViewModel post,IEnumerable<HashtagViewModel> hashtags) {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PostForPublicateViewModel,PostForPublicateDTO>();
                cfg.CreateMap<HashtagViewModel, HashtagDTO>();
            });
            PostForPublicateDTO postDto = Mapper.Map<PostForPublicateDTO>(post);
            IEnumerable<HashtagDTO> hashtagsDto = Mapper.Map<IEnumerable<HashtagDTO>>(hashtags);
                interaction.PostInteractionService.PublishPost(postDto, hashtagsDto);
        }

      
    }
}