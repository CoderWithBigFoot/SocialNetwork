using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.BLL.Interfaces.ServicesProviders;

namespace SocialNetwork.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IBasicInfo basicInfo;
       
        public HomeController(IBasicInfo basicInfo,IInteraction interaction,IRegistration registr,IStatistics stat) {
            this.basicInfo = basicInfo;
        }

        public ActionResult Index()
        {
           
            return View();
        }

        
    }
}