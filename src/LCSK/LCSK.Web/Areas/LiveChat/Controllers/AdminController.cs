using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using LCSK.Core;

namespace LCSK.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
			if (ConfigurationManager.ConnectionStrings["LCSKDb"] == null)
			{
				return RedirectToAction("Index", "Install");
			}

            return View();
        }

		[HttpPost]
		public ActionResult Index(string name, string pass)
		{
			OperatorService svc = new OperatorService();
			var op = svc.SignIn(name, pass);
			return View();
		}
    }
}
