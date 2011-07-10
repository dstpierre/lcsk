using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCSK.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
            ViewBag.notinstalled = cfg.ConnectionStrings.ConnectionStrings["LCSK"] != null;
            return View();
        }

		public ActionResult TestingReferrer()
		{
			return View();
		}

    }
}
