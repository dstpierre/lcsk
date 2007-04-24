using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Net.Mail;

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
		set { ViewState["__visitorName"] = value; }
	}

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Page.IsPostBack)
		{
			pnlNoOperator.Visible = pnlChat.Visible = pnlRequest.Visible = false;

			if (OperatorService.GetOperatorStatus())
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

		if (Request.Cookies[chatId + "_lastId"] != null)
		{
			Response.Cookies[chatId + "_lastId"].Value = "0";
		}
		else
		{
			HttpCookie cookie = new HttpCookie(chatId + "_lastId", "0");
			Response.Cookies.Add(cookie);
		}

		ChatRequestInfo request = new ChatRequestInfo();
		request.ChatId = chatId;
		request.RequestDate = DateTime.Now;
		request.VisitorEmail = txtEmail.Text;
		if (Request.UserHostAddress != null)
			request.VisitorIP = Request.UserHostAddress.ToString();
		request.VisitorName = txtName.Text;
		if( Request.ServerVariables["HTTP_USER_AGENT"] != null )
			request.VisitorUserAgent = Request.ServerVariables["HTTP_USER_AGENT"].ToString();
		request.WasAccept = false;

		ChatService.RequestChat(request);

		ChatMessageInfo msg = new ChatMessageInfo(1, request.ChatId, string.Empty, DateTime.Now, "Waiting for an operator to accept your request.");
		ChatService.AddMessage(msg);

		// we set the visitor name in the ViewState
		VisitorName = request.VisitorName;

		pnlChat.Visible = true;
		pnlRequest.Visible = false;
	}
	protected void timerRefresh_Tick(object sender, EventArgs e)
	{
		if (Request.Cookies["chatId"] != null)
		{
			string chatId = Request.Cookies["chatId"].Value.ToString();
			if (Request.Cookies[chatId + "_lastId"] != null)
			{
				int lastId = int.Parse(Request.Cookies[chatId + "_lastId"].Value.ToString());
				List<ChatMessageInfo> messages = ChatService.GetMessages(chatId, lastId);
				if (messages.Count > 0)
				{
					for (int i = messages.Count - 1; i >= 0; i--)
					{
						litChat.Text += string.Format("<span class=\"chatName\">{0} :</span>{1}<br />", messages[i].Name, messages[i].Message);
					}

					lastId = messages[0].MessageId;
					
					// set the lastId
					Response.Cookies[chatId + "_lastId"].Value = lastId.ToString();
				}
			}
		}
	}
	protected void btnSendMsg_Click(object sender, EventArgs e)
	{
		if (Request.Cookies["chatId"] != null && txtMsg.Text.Length > 0)
		{
			// Add a new message to the discussion
			string chatId = Request.Cookies["chatId"].Value.ToString();
			
			// we get the last MessageId
			int id = ChatService.GetLastMessageId(chatId) + 1;

			ChatMessageInfo msg = new ChatMessageInfo(id, chatId, VisitorName, DateTime.Now, txtMsg.Text);
			ChatService.AddMessage(msg);
			txtMsg.Text = string.Empty;
		}
	}
	protected void lnkSendEmail_Click(object sender, EventArgs e)
	{
		// we send an email to the configured email on the web.config
		MailMessage mail = new MailMessage();
		mail.From = new MailAddress(txtSendBy.Text);
		mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["Email"].ToString()));
		mail.Subject = "Message from your LiveChat application";
		mail.Body = txtComment.Text;

		SmtpClient mailer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString());
		mailer.Send(mail);

		lblConfirmation.Visible = true;
		lblConfirmation.Text = "Thank you, we will answer your email as soon as possible.";
	}
}
