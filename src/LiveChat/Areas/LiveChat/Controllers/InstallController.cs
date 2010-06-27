using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using LiveChat.Core.SqlRepository;
using LiveChat.Core.Entities;
using System.Web.Security;

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
		public ActionResult Index(string server, string dbname, string username, string password)
		{
			var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
			if (cfg.ConnectionStrings.ConnectionStrings["LCSK"] == null)
			{
				cfg.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("LCSK",
					string.Format("Data Source={0};Initial Catalog={1};{2}", server, dbname,
						(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) ? "User id:" + username + ";password: " + password : "Integrated Security=True")
					), "System.Data.SqlClient"));

				cfg.Save();
			}

			return RedirectToAction("InstallDatabase");
		}

		public ActionResult InstallDatabase()
		{
			SqlAdminRepository svc = new SqlAdminRepository();
			if (svc.Install())
			{
				return RedirectToAction("Init");
			}
			throw new Exception("Unable to create the database");
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
			viewModel.Id = Guid.NewGuid();
			viewModel.LastLogin = DateTime.Now;
			viewModel.Manager = true;
			viewModel.Modified = DateTime.Now;
			viewModel.Online = true;

			SqlOperatorRepository svc = new SqlOperatorRepository();
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
