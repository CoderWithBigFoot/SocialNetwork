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
    public class ProfileController : Controller
    {
        private IBasicInfo basicInfo;
        private IInteraction interaction;
        private IStatistics statistics;

        public ProfileController(IBasicInfo basicInfo, IInteraction interaction,IStatistics statistics) {
            this.basicInfo = basicInfo;
            this.interaction = interaction;
            this.statistics = statistics;
        }

       

    }
}