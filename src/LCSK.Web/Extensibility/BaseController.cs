using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCSK.Web
{
    public class BaseController : Controller
    {
        protected bool IsAdmin
        {
            get { return ViewBag.isadmin != null && (bool)ViewBag.isadmin; }
        }

        protected bool IsDatabaseCreated
        {
            get
            {
                var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
                return cfg.ConnectionStrings.ConnectionStrings["LCSK"] != null;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.isadmin = filterContext.HttpContext.Session["lcsk_isadmin"];
            base.OnActionExecuting(filterContext);
        }
    }
}