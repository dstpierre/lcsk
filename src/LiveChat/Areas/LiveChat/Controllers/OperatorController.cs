using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveChat.Core.SqlRepository;
using System.IO;

namespace LiveChat.Areas.LiveChat.Controllers
{
    public class OperatorController : Controller
    {
		SqlOperatorRepository opService = new SqlOperatorRepository();

        public ActionResult Index()
        {
            return View();
        }

    }
}
