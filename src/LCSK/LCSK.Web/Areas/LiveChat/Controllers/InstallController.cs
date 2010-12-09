using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using LCSK.Core;

namespace LCSK.Controllers
{
	[HandleError]
    public class InstallController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(string id)
		{
			var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
			if (cfg.ConnectionStrings.ConnectionStrings["LCSKDb"] == null)
			{
				cfg.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("LCSKDb",
					"Data Source=|DataDirectory|\\livLCSKDb.sdf;", "System.Data.SqlServerCe.4.0"));

				cfg.Save();
			}

			return RedirectToAction("Init");
		}

		public ActionResult Init()
		{
			return View(new Operator());
		}

		[HttpPost]
		public ActionResult Init(Operator viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			viewModel.Admin = true;
			viewModel.Created = DateTime.Now;
			viewModel.OperatorId = Guid.NewGuid();
			viewModel.LastLogin = DateTime.Now;
			viewModel.Manager = true;
			viewModel.Created = DateTime.Now;
			viewModel.Online = true;

			OperatorService svc = new OperatorService();
			if (svc.Create(viewModel))
			{
				FormsAuthentication.SetAuthCookie(viewModel.Username, false);
				return RedirectToAction("Index", "Admin");
			}
			else
			{
				ModelState.AddModelError("_FORM", "Unable to create this operator at the moment.");
				return View(viewModel);
			}
		}
    }
}
