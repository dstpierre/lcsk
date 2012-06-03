using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCSK;
using System.Configuration;
using System.Net.Mail;

namespace $rootnamespace$.Controllers
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
            return RedirectToAction("Go", new { id = opname + "-" + oppass });
        }

        public ActionResult Go(string id)
        {
            if (string.IsNullOrEmpty(id) || id.IndexOf("-") == -1)
                return null;

            string[] val = id.Split(new char[] { '-' });
            if (val != null && val.Length >= 2)
            {
                var op = repo.GetOperator(val[0], val[1]);
                if (op != null)
                {
                    if (val.Length == 3)
                    {
                        repo.ChangeStatus(op.Id, !op.IsOnline);
                        op.IsOnline = !op.IsOnline;
                    }
                    ViewBag.op = op;
                    return View(repo.GetVisitors(op.Id, Request.Url.Host));
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult Go(string id, string action)
        {
            repo.DeleteUnclosedChat();

            if (string.IsNullOrEmpty(id) || id.IndexOf("-") == -1)
                return null;

            string[] val = id.Split(new char[] { '-' });
            if (val != null && val.Length >= 2)
            {
                var op = repo.GetOperator(val[0], val[1]);
                if (op != null)
                {
                    ViewBag.op = op;
                    return View(repo.GetVisitors(op.Id, Request.Url.Host));
                }
            }
            return null;
        }

        //[Authorize]
        public ActionResult ChatSession(Guid id)
        {
            Guid opId = Guid.Empty;
            string opName = "";
            string ip = "";
            Guid vid = Guid.Empty;


            if (Request.QueryString["ip"] != null)
                ip = LCSK.StringExtensions.FromBase64(Request.QueryString["ip"].ToString());

            if (Request.QueryString["vid"] != null)
                vid = Guid.Parse(LCSK.StringExtensions.FromBase64(Request.QueryString["vid"].ToString()));

            if (Request.QueryString["opname"] != null)
                opName = Request.QueryString["opname"].ToString();
            else
                opName = "Operator";

            if (Request.QueryString["opId"] != null)
                opId = Guid.Parse(Request.QueryString["opId"].ToString());
            else
                return null;


            if (id == Guid.Empty)
            {
                id = repo.RequestChat(vid, ip, opId, true);
                repo.AddMsg(id, opName, "Can I help you with something?");
            }
            else
                repo.Accept(id, opId);

            ViewBag.opname = opName;
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
