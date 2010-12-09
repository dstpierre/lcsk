using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LCSK.Core;

namespace LCSK.Controllers
{
    public class OperatorController : Controller
    {
		OperatorService opService = new OperatorService();

        public ActionResult Index()
        {
            return View();
        }

    }
}
