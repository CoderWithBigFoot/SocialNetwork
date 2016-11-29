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
    [Authorize]
    public class CommonController : Controller
    {
        private IBasicInfo basicInfo;
        
        public CommonController(IBasicInfo basicInfo) {
            this.basicInfo = basicInfo;
            
        }

        [HttpGet]
        public ActionResult Homepage()
        {
            try
            {
                ProfileDTO profileInfo = basicInfo.ProfileInfoService.GetProfile(HttpContext.User.Identity.Name);
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>());
                ProfileViewModel result = Mapper.Map<ProfileViewModel>(profileInfo);

                result.Followers = basicInfo.ProfileInfoService.GetFollowers(HttpContext.User.Identity.Name);
                result.Subscriptions = basicInfo.ProfileInfoService.GetSubscriptions(HttpContext.User.Identity.Name);
                return View(result);
            }
            catch (ProfileNotFoundException ex) {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public PartialViewResult HomepagePartial() {
            ProfileDTO profileInfo = basicInfo.ProfileInfoService.GetProfile(HttpContext.User.Identity.Name);
            Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>());
            ProfileViewModel result = Mapper.Map<ProfileViewModel>(profileInfo);

            result.Followers = basicInfo.ProfileInfoService.GetFollowers(HttpContext.User.Identity.Name);
            result.Subscriptions = basicInfo.ProfileInfoService.GetSubscriptions(HttpContext.User.Identity.Name);

            return PartialView("../Partials/Home",result);
        }


        /*
        [HttpGet]
        public ActionResult ProfilePage(string identityName) {
            try
            {
                ProfileDTO profileInfo = basicInfo.ProfileInfoService.GetProfile(identityName);
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>());
                ProfileViewModel result = Mapper.Map <ProfileViewModel>(profileInfo);

                result.Followers = basicInfo.ProfileInfoService.GetFollowers(identityName);
                result.Subscriptions = basicInfo.ProfileInfoService.GetSubscriptions(identityName);

                return PartialView("~/Views/Partials/HomepagePartial",result);
            }
            catch (ProfileNotFoundException ex)
            {
                return PartialView(ex.Message);
            }
        }
        */
       
        ~CommonController() {
            basicInfo.Dispose();
        }
    }
}