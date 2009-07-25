using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using LiveChat.Entities;
using LiveChat.BusinessLogic;
using System.Net.Mail;

namespace LiveChat.WebSite
{
	public partial class Chat : System.Web.UI.Page
	{

		public string VisitorName
		{
			get
			{
				if (ViewState["__visitorName"] != null)
					return ViewState["__visitorName"].ToString();
				else
					return string.Empty;
			}
			set
			{
				if (ViewState["__visitorName"] != null)
					ViewState["__visitorName"] = value;
				else
					ViewState.Add("__visitorName", value);
			}
		}

		public static string VName
		{

			get
			{
				HttpContext ctx = HttpContext.Current;
				if (ctx != null)
				{
					if (ctx.Request.Cookies["VisitorName"] != null)
						return ctx.Request.Cookies["VisitorName"].Value.ToString();
					else
						return string.Empty;
				}
				else
					return "nocontext";
			}
			set
			{
				HttpContext ctx = HttpContext.Current;
				if (ctx != null)
				{
					if (ctx.Request.Cookies["VisitorName"] != null)
						ctx.Response.Cookies["VisitorName"].Value = value;
					else
					{
						HttpCookie c = new HttpCookie("VisitorName", value);
						ctx.Response.Cookies.Add(c);
					}
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				pnlNoOperator.Visible = pnlChat.Visible = pnlRequest.Visible = false;

				if (Operator.IsOperatorOnline())
				{
					pnlRequest.Visible = true;
				}
				else
				{
					pnlNoOperator.Visible = true;
				}
			}

		}
		protected void lnkStartChat_Click(object sender, EventArgs e)
		{
			// Initiate a chat request
			string chatId = Guid.NewGuid().ToString();
			if (Request.Cookies["chatId"] != null)
			{
				Response.Cookies["chatId"].Value = chatId;
			}
			else
			{
				HttpCookie cookie = new HttpCookie("chatId", chatId);
				Response.Cookies.Add(cookie);
			}

			if (Request.Cookies[chatId + "_lastCheck"] != null)
			{
				Response.Cookies[chatId + "_lastCheck"].Value = "0";
			}
			else
			{
				HttpCookie cookie = new HttpCookie(chatId + "_lastCheck", "0");
				Response.Cookies.Add(cookie);
			}

			ChatRequestEntity request = new ChatRequestEntity();
			request.RequestedDate = DateTime.Now;
			request.VisitorEmail = txtEmail.Text;
			if (Request.UserHostAddress != null)
				request.VisitorIp = Request.UserHostAddress.ToString();
			request.VisitorName = txtName.Text;

			LiveChat.BusinessLogic.Chat.RequestChat(request);

			// we set the visitor name in the ViewState
			VisitorName = request.VisitorName;
			VName = request.VisitorName;

			pnlChat.Visible = true;
			pnlRequest.Visible = false;
		}

		protected void timerRefresh_Tick(object sender, EventArgs e)
		{
			if (Request.Cookies["chatId"] != null)
			{
				string chatId = Request.Cookies["chatId"].Value.ToString();
				if (Request.Cookies[chatId + "_lastCheck"] != null)
				{
					long lastCheck = long.Parse(Request.Cookies[chatId + "_lastCheck"].Value.ToString());
					if (LiveChat.BusinessLogic.Chat.HasNewMessage(chatId, lastCheck))
					{
						List<MessageEntity> messages = LiveChat.BusinessLogic.Chat.GetMessages(chatId, lastCheck);
						if (messages.Count > 0)
						{
							for (int i = messages.Count - 1; i >= 0; i--)
							{
								litChat.Text += string.Format("<span class=\"chatName\">{0} :</span>{1}<br />", messages[i].FromName, messages[i].Message);
								lastCheck = messages[i].EntityId;
							}

							// set the lastId
							Response.Cookies[chatId + "_lastCheck"].Value = lastCheck.ToString();

						}
					}
				}
			}
		}
		[System.Web.Services.WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static string CheckTypingNotification(string chatId)
		{
			if (!string.IsNullOrEmpty(chatId))
			{
				return "not implemented...";
			}
			else return string.Empty;
		}

		[System.Web.Services.WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static string SetTypingNotification(string chatId, string msg)
		{
			return "";
		}

		[System.Web.Services.WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static string SendMsg(string msg, string chtID)
		{
			if (chtID != null && msg.Length > 0)
			{
				// Add a new message to the discussion
				string chatId = chtID;

				MessageEntity mesg = new MessageEntity();
				mesg.ChannelId = chatId;
				mesg.FromName = VName;
				mesg.Message = msg;
				mesg.SendDate = DateTime.Now;

				LiveChat.BusinessLogic.Chat.WriteMessage(mesg);

				//TODO: Set typing to false
			}
			return "";
		}



		protected void lnkSendEmail_Click(object sender, EventArgs e)
		{
			// we send an email to the configured email on the web.config
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(txtSendBy.Text);
			mail.To.Add(new MailAddress(Config.ServiceKey));
			mail.Subject = "Message from your LiveChat application";
			mail.Body = txtComment.Text;

			SmtpClient mailer = new SmtpClient(Config.SMTPServer);
			mailer.Send(mail);

			lblConfirmation.Visible = true;
			lblConfirmation.Text = "Thank you, we will answer your email as soon as possible.";
		}
	}
}
