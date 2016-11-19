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
        public ActionResult Index(IBasicInfo info)
        {
           
            return View();
        }

        
    }
}