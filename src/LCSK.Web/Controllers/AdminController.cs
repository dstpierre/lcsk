using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCSK.Services;
using LCSK.Core;

namespace LCSK.Web.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pass)
        {
            var op = OperatorService.LogIn("admin", pass);
            if (op != null)
            {
                ViewBag.isadmin = HttpContext.Session["lcsk_isadmin"] = true;
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            if (!IsAdmin)
            {
                return RedirectToAction("Index");
            }

            var vm = new DashboardViewModel();

            vm.Visitors = RequestService.GetRequest(DateTime.Now.AddMinutes(-10));
            vm.CurrentChat = ChatService.GetCurrentSessions();
            vm.PendingRequests = ChatService.GetPendingRequests();
            vm.PendingInvitations = ChatService.GetPendingInvitations();

            return View(vm);
        }
    }
}
