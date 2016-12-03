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
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
namespace SocialNetwork.WEB.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        private IStatistics statistics;
        public StatisticsController(IStatistics statistics) {
            this.statistics = statistics;
        }

        [HttpPost]
        public PartialViewResult StatisticsPartial(string identityName)
        {
            StatisticsViewModel model = new StatisticsViewModel();

            //Mapper.Initialize(cfg => cfg.CreateMap<KeyValuePair<HashtagDTO, int>, KeyValuePair<string, int>>().ConvertUsing(opt=>new KeyValuePair<string, int>(opt.Key.Name,opt.Value)));
            switch (identityName)
            {
                case "authorizedProfile": identityName = HttpContext.User.Identity.Name; break;
            }
            //here is try PublicationsNotFound
            model.PublishedPostsCount = statistics.GetProfileStatisticsService.PublishedPostsCount(identityName);

            foreach (var current in statistics.GetProfileStatisticsService.EachHashtagCount(identityName)) {
                model.EachHashtagCount.Add(new KeyValuePair<string,int>(current.Key.Name,current.Value));
            }
                
            return PartialView("../Partials/Statistics",model);
        }

    }
}