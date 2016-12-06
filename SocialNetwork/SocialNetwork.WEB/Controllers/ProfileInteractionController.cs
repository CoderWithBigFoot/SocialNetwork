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
                else {
                    result = "Successfully subscibed";
                }
                return JObject.FromObject(new { completionMessage = result });
            }
            catch (ProfileNotFoundException) {
                return JObject.FromObject(new { completionMessage = "Such profile dont exist." });
            }
        }

       


    }
}