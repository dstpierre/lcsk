using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace LCSK.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
			if (ConfigurationManager.ConnectionStrings["lcsk"] == null)
			{
				return RedirectToAction("Index", "Install");
			}

            return View();
        }

    }
}
