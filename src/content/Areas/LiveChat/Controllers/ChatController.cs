using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCSK.Core;
using LCSK.Services;
using System.Net.Mail;

namespace LCSK.Web.Areas.LiveChat.Controllers
{
    public class ChatController : Controller
    {
		public FileResult ChatImage()
		{
			RequestService.LogRequest(new WebRequest()
			{
				DomainName = Request.Url.Host,
				PageRequested = Request.UrlReferrer.AbsolutePath,
				Referrer = Request.QueryString["r"] != null ? Server.UrlDecode(Request.QueryString["r"].ToString()) : "",
				Requested = DateTime.Now,
				VisitorIp = Request.UserHostAddress,
				VisitorUserAgent = Request.UserAgent
			});

			string file = Server.MapPath("/Content/LCSK/" + (OperatorService.GetOperatorStatus() ? "online.jpg" : "offline.jpg"));
			return new FileStreamResult(new FileStream(file, FileMode.Open), "image/jpg");
		}

		private VisitorInitViewModel InitRequest()
		{
			VisitorInitViewModel vm = new VisitorInitViewModel();

			var onlineOps = OperatorService.GetOnlineOperator();

			List<string> departments = new List<string>();
			foreach (var op in onlineOps)
			{
				foreach (string d in op.Department.Split(','))
				{
					if (!departments.Contains(d.ToLower()))
						departments.Add(d.ToLower());
				}
			}

			vm.Departments = departments;
			vm.ChatOnline = onlineOps.Count() > 0;
			vm.NewChatRequest = new ChatRequest();

			return vm;
		}
		public ActionResult Session()
		{
			var vm = InitRequest();

			return View(vm);
		}

		[HttpPost]
		public ActionResult Session(VisitorInitViewModel data)
		{
			if (!ModelState.IsValid)
			{
				var vm = InitRequest();
				vm.NewChatRequest = data.NewChatRequest;

				return View(vm);
			}

			data.NewChatRequest.ChatId = Guid.NewGuid();
			data.NewChatRequest.Requested = DateTime.Now;
			data.NewChatRequest.VisitorIp = Request.UserHostAddress;
			data.NewChatRequest.VisitorUserAgent = Request.UserAgent;
			data.NewChatRequest.WasAccepted = false;

			ChatService.RequestChat(data.NewChatRequest);

			HttpCookie ck = new HttpCookie("lcsk_name");
			ck.Value = data.NewChatRequest.VisitorName;
			Response.Cookies.Add(ck);

			return RedirectToAction("Chat", new { id = data.NewChatRequest.ChatId });
		}

		[HttpPost]
		public ActionResult SendMail(string name, string email, string message)
		{
			MailMessage mail = new MailMessage();
			mail.To.Add("info@" + Request.Url.Host);
			mail.From = new MailAddress("info@" + Request.Url.Host);
			mail.Subject = "Contact from your live chat";
			mail.Body = "name:\t" + name + "\r\nemail:\t" + email + "\r\n\r\nMessage:\r\n" + message;

			SmtpClient svc = new SmtpClient();
			svc.Send(mail);

			return RedirectToAction("MailSent");
		}

		public ActionResult MailSent()
		{
			return View();
		}

		public ActionResult Chat(Guid id)
		{
			return View(id);
		}

		[HttpPost]
		public ActionResult AddMsg(Guid id, string msg)
		{
			string name = "n/a";
			if (Request.Cookies["lcsk_name"] != null)
				name = Request.Cookies["lcsk_name"].Value.ToString();

			ChatService.AddMessage(new ChatMessage()
			{
				ChatId = id,
				Message = msg,
				Name = name
			});

			Debug.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, "AddMsg", msg));

			return null;
		}

		[HttpPost]
		public ActionResult CheckMessages(Guid id)
		{
			long lastId = 0;
			if (Request.Cookies["lcsk_id"] != null)
			{
				lastId = long.Parse(Request.Cookies["lcsk_id"].Value);
			}

			Debug.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, "CheckMessages", lastId));

			if (ChatService.HasNewMessage(id, lastId))
			{
				var msgs = ChatService.GetMessages(id, lastId);
				lastId = msgs.Max(x => x.MessageId);

				HttpCookie ck = new HttpCookie("lcsk_id");
				ck.Value = lastId.ToString();
				Response.Cookies.Add(ck);

				return PartialView("ChatLine", msgs);
			}
			return null;
		}

		[HttpPost]
		public ActionResult CheckTyping(Guid id)
		{
			LCSKServices.OpServices svc = new LCSKServices.OpServices();
			return PartialView("Typing", svc.IsTyping(id.ToString(), true));
		}

		public ActionResult Install()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Install(string server, string dbname, string username, string password, string adminPassword)
		{
			var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"/");
			if (cfg.ConnectionStrings.ConnectionStrings["LCSK"] == null)
			{
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
