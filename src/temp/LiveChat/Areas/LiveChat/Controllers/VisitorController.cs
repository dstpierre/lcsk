using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveChat.Core.SqlRepository;
using System.IO;

namespace LCSK
{
	[HandleError]
    public class VisitorController : Controller
    {
		SqlOperatorRepository opService = new SqlOperatorRepository();
		SqlVisitorRepository visitorService = new SqlVisitorRepository();

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
