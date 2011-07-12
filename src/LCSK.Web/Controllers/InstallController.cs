using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using LCSK.Services;

namespace LCSK.Web.Controllers
{
    public class InstallController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string server, string dbname, string username, string password, string adminPassword)
        {
            if (!IsDatabaseCreated)
            {
                var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
                cfg.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("LCSK",
                    string.Format("Data Source={0};Initial Catalog={1};{2}", server, dbname,
                        (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) ? "User id:" + username + ";password: " + password : "Integrated Security=True;")
                    ), "System.Data.SqlClient"));

                cfg.Save();
            }

            return RedirectToAction("InstallDatabase", new { id = adminPassword });
        }

        public ActionResult InstallDatabase(string id)
        {
            if (OperatorService.CreateDatabase(id))
            {
                return RedirectToAction("InstallCompleted");
            }
            return RedirectToAction("InstallError");
        }

        public ActionResult InstallCompleted()
        {
            return View();
        }
    }
}
