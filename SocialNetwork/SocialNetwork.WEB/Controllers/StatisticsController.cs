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

            /*Mapper.Initialize(cfg => {
                cfg.CreateMap<HashtagDTO, string>().ConvertUsing(x => x.Name);
                cfg.CreateMap<Dictionary<HashtagDTO,int>,Dictionary<string,int>> ().());
            });
            */
            int publishedPostsCount = statistics.GetProfileStatisticsService.PublishedPostsCount(identityName);
            Dictionary<string, int> EachHashtagCount = Mapper.Map<Dictionary<string,int>>(statistics.GetProfileStatisticsService.EachHashtagCount(identityName));
            Dictionary<string, int> PostsCountByMostPopularHashtags = Mapper.Map<Dictionary<string, int>>(statistics.GetProfileStatisticsService.MostPopularHashtags(identityName));
            
            StatisticsViewModel model = new StatisticsViewModel();
            /*model.PublishedPostsCount = publishedPostsCount;
            //PublishedPostsNotFoundException
            model.EachHashtagCount = EachHashtagCount;
            model.PostsCountByMostPopularHasthags = PostsCountByMostPopularHashtags;
            //*/


            return PartialView("../Partials/Statistics",model);
        }

       

    }
}