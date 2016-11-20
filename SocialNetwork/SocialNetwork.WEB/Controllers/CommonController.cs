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
namespace SocialNetwork.WEB.Controllers
{
    //[Authorize]
    public class CommonController : Controller
    {
        private IBasicInfo basicInfo;
        private IInteraction interaction;
        
        public CommonController(IBasicInfo basicInfo,IInteraction interaction) {
            this.basicInfo = basicInfo;
            this.interaction = interaction;
        }

        public ActionResult MainPage()
        {
            try
            {
                ProfileDTO profileInfo = basicInfo.ProfileInfoService.GetProfile(HttpContext.User.Identity.Name);
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>());
                //ViewBag.currentUserInfo = Mapper.Map<ProfileViewModel>(profileInfo);
                return View(Mapper.Map<ProfileViewModel>(profileInfo));
            }
            catch (ProfileNotFoundException ex) {
                return PartialView(ex.Message);
            }
        }

        /*public IEnumerable<PostDTO> GetPublications() {
            
        }*/
    }
}