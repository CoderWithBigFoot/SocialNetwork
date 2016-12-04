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
    public class SearchController : Controller
    {
        private IInteraction interaction;
        public SearchController(IInteraction interaction) {
            this.interaction = interaction;
        }
        


    }
}