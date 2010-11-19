using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCSK.Core;

namespace LCSK.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			OperatorService svc = new OperatorService();
            return View(svc.OperatorCount());
        }

    }
}
