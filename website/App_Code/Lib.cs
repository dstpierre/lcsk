#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Shared methods
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Net.Mail;
using System.Text;



	/// <summary>
	/// Public methods accessibles throw all over the application
	/// </summary>
public class Lib
{
	public static string GetDomainName(string urlReferer)
	{
		string url = urlReferer.Replace("http://", string.Empty).ToString().Replace("https://", string.Empty).ToString().Replace("www.", string.Empty).ToString();
		char[] c = "/".ToCharArray();

		string[] result = url.Split(c[0]);

		if (result[0] != null && result[0] != string.Empty)
			return result[0];
		else
			return string.Empty;
	}

	public static void SendChatMail(Int64 ChatRequestID, string EmailToSend)
	{
		/*
		MailMessage mailMsg = new MailMessage();
		mailMsg.From = new MailAddress("chat@onlinecontact.com");
		mailMsg.To = new MailAddress(EmailToSend);
		mailMsg.Subject = "Chat session (http://www.onlinecontact.ca/";
		mailMsg.BodyFormat = MailFormat.Html;

		StringBuilder sb = new StringBuilder();
		sb.Append("<html>");
		sb.Append("<head>");
		sb.Append("<title>Chat</title>");
		sb.Append("<style type=\"text/css\">");
		sb.Append("td {	font-family: Verdana;	font-size: 10pt;color: black;}");
		sb.Append(".user {color: red;}");
		sb.Append(".chatlb {	font-size: 8pt;}");
		sb.Append("</style></head><body topmargin=\"5\" leftmargin=\"5\" bgcolor=\"#CBD2E7\">");
		sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"525\">");
		sb.Append("<tr><td><b>Chat session:  http://www.eliostech.com/ with Dominic</b></td>");
		sb.Append("</tr></table><table border=\"1\" bordercolor=\"#000000\" cellpadding=\"0\" cellspacing=\"0\" width=\"525\" bgcolor=\"#FFFFE1\">");
		sb.Append("<tr valign=\"top\">");
		sb.Append("<td><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
		sb.Append("<tr valign=\"top\"><td width=\"5\"></td>	<td>	<br class=\"chatlb\">");
		sb.Append("<b>Dominic :</b> Hi how may i help you | Bonjour comment puis-je vous aider?<br class=\"chatlb\">");
		sb.Append("<td width=\"5\"></td></tr></table></td></tr></table></body></html>");

		mailMsg.Body = sb.ToString();
		SmtpMail.SmtpServer = "relais.videotron.ca";
		//try
		//{
		SmtpMail.Send(mailMsg);
		//}
		//catch {  }
		*/
	}
}