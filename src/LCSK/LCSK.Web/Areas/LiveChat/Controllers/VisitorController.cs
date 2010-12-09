using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LCSK.Core;

namespace LCSK.Controllers
{
	[HandleError]
    public class VisitorController : Controller
    {
		OperatorService opService = new OperatorService();
		VisitorService visitorService = new VisitorService();

		public FileResult ChatImage()
		{
			string file = Server.MapPath("/Content/LCSK/" + (opService.ChatOnline() ? "online.jpg" : "offline.jpg"));
			return new FileStreamResult(new FileStream(file, FileMode.Open), "image/jpg");
		}

        public ActionResult Index()
        {
            return View(visitorService.Init());
        }

    }
}
