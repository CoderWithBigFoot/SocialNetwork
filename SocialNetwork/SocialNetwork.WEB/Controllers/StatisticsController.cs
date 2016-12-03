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
            switch (identityName)
            {
                case "authorizedProfile": identityName = HttpContext.User.Identity.Name; break;
            }     
            return PartialView("../Partials/Statistics",identityName);
        }

        [HttpPost]
        public JObject ProfileStatisticsData(string identityName) {
            StatisticsViewModel model = new StatisticsViewModel();

            model.PublishedPostsCount = statistics.GetProfileStatisticsService.PublishedPostsCount(identityName);
            foreach (var current in statistics.GetProfileStatisticsService.EachHashtagCount(identityName))
            {
                model.EachHashtagCount.Add(new KeyValuePair<string, int>(current.Key.Name, current.Value));
            }
            foreach (var current in statistics.GetProfileStatisticsService.MostPopularHashtags(identityName)) {
                model.MostPopularHasthags.Add(new KeyValuePair<string, int>(current.Key.Name, current.Value));
            }
            

            return JObject.FromObject(model);
        }


    }
}