using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCSK;
using System.Configuration;
using System.Net.Mail;
using System.IO;

namespace Demo.Controllers
{
    public class ChatController : Controller
    {
        ChatRepository repo = new ChatRepository(ConfigurationManager.ConnectionStrings["LCSK"].ToString());

        private Guid GetVisitorId
        {
            get
            {
                Guid id = Guid.NewGuid();
                if (Request.Cookies["lcsk_visitorid"] == null)
                {
                    var cookie = new HttpCookie("lcsk_visitorid");
                    cookie.Value = id.ToString();
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    if(!Guid.TryParse(Request.Cookies["lcsk_visitorid"].Value, out id))
                        id = Guid.NewGuid();
                }
                return id;
            }
        }

        private string GetIp()
        {
            try
            {
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    // sometimes HTTP_X_FORWARDED_FOR returns multiple IP's
                    string[] ipRange = ip.Split(',');
                    ip = ipRange[ipRange.Length - 1];
                }
                else
                    ip = Request.ServerVariables["REMOTE_ADDR"];

                return ip;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private bool SendMail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.Body = body;
            mail.From = new MailAddress(to);
            mail.IsBodyHtml = false;
            mail.Subject = subject;
            mail.To.Add(new MailAddress(to));

            try
            {
                new SmtpClient().Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetViewHtml(string viewName, object model)
        {
            var content = string.Empty;
            var view = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            using (var writer = new StringWriter())
            {
                ViewData.Model = model;
                var context = new ViewContext(ControllerContext, view.View, ViewData, TempData, writer);
                view.View.Render(context, writer);
                writer.Flush();
                content = writer.ToString();
            }
            return content;
        }

        [HttpPost]
        public ActionResult LogVisit(string referrer, string page)
        {
            return Json(repo.LogVisit(GetVisitorId, GetIp(), page, referrer));
        }

        [HttpPost]
        public ActionResult Ping(string page)
        {
            return Json(repo.VisitorPing(GetVisitorId, page));
        }

        [HttpPost]
        public ActionResult RequestChat()
        {
            Guid chatId = repo.RequestChat(GetVisitorId, GetIp(), Guid.Empty, false);
            return Json(new { status = chatId != Guid.Empty, chatId = chatId });
        }

        [HttpPost]
        public ActionResult AddMsg(Guid id, string from, string msg)
        {
            repo.AddMsg(id, from, msg);
            return Json("ok");
        }

        [HttpPost]
        public ActionResult PollMsgs(Guid id, long lastId)
        {
            var msgs = repo.GetMsgs(id, lastId);
            if (msgs == null)
                msgs = new List<ChatMessage>();

            return Json(new
            {
                lastId = msgs.Count() > 0 ? msgs.Max(x => x.Id) : 0,
                msgs = msgs,
                beep = msgs.Count(x => x.FromName == "me")
            });
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string opname, string oppass)
        {
            return RedirectToAction("Go", new { id = opname + "-" + LCSK.StringExtensions.ToBase64(oppass) });
        }

        public ActionResult Go(string id)
        {
            if (string.IsNullOrEmpty(id) || id.IndexOf("-") == -1)
                return null;

            string[] val = id.Split(new char[] { '-' });
            if (val != null && val.Length >= 2)
            {
                var op = repo.GetOperator(val[0], LCSK.StringExtensions.FromBase64(val[1]));
                if (op != null)
                {
                    if (val.Length == 3)
                    {
                        repo.ChangeStatus(op.Id, !op.IsOnline);
                        op.IsOnline = !op.IsOnline;
                    }
                    return View(op);
                }
            }
            return RedirectToAction("Index", new { id = "unbable to sign-in" });
        }

        [HttpPost]
        public ActionResult Go(Guid id, string data)
        {
            Dictionary<Guid, long> state = new Dictionary<Guid, long>();
            if (!string.IsNullOrEmpty(data))
            {
                foreach (var item in data.Split('\n'))
                {
                    var buffer = item.Split('|');
                    if (buffer != null && buffer.Length == 3)
                    {
                        state.Add(Guid.Parse(buffer[0]), long.Parse(buffer[1]));
                        if (buffer[2] == "1")
                            ViewBag.selectedId = buffer[0];
                    }
                }
            }

            ViewBag.state = state;
            var model = repo.GetVisitors(id, Request.Url.Host);
            return Json(new { list = GetViewHtml("ChatItem", model.PendingChats), visitors = GetViewHtml("Visitors", model.Visits) });
        }

        [HttpPost]
        public ActionResult ChangeStatus(Guid id, int status)
        {
            if (repo.ChangeStatus(id, status == 1))
                return Json("ok");
            else
                return Json("failed");
        }

        //[Authorize]
        public ActionResult ChatSession(Guid id, string ip, Guid opId, string opName, Guid visitorId)
        {
            if (id == Guid.Empty)
            {
                id = repo.RequestChat(visitorId, ip, opId, true);
                repo.AddMsg(id, opName, "Can I help you with something?");
            }
            else
                repo.Accept(id, opId);

            ViewBag.opname = opName;
            ViewBag.visitorIp = ip;
            return View(id);
        }

        [HttpPost]
        public ActionResult Close(Guid id)
        {
            if (repo.Close(id))
                return Json("ok");
            else
                return Json("failed");
        }

        [HttpPost]
        public ActionResult SendEmail(string email, string comment)
        {
            if (SendMail("your@email.com", "Contact from website", "Email: " + email + "\n\n" + comment))
                return Json("ok");
            else
                return Json("failed");
        }
    }
}
