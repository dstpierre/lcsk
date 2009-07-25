using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace LiveChat.WebSite
{
	public class ChatButton : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			StringBuilder js = new StringBuilder();

			string domain = context.Request.Url.Host;

			if (context.Request.Url.Port != 80)
				domain += ":" + context.Request.Url.Port;

			js.Append("function lcsk_openChat()\r\n");
			js.Append("\t{\r\n\t\t");
			js.Append("var win = window.open('Chat.aspx', 'chat', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=603,height=510');\r\n");
			js.Append("\t\twin.focus();\r\n");
			js.Append("\t\twin.opener = window;\r\n");
			js.Append("\t\treturn false;");
			js.Append("\t}");

			js.Append("document.write('<a href=\"javascript://\" onclick=\"lcsk_openChat();\">");
			js.Append("<img id=\"_imgLC\" src=\"http://");
			js.Append(domain);
			js.Append("/ProcessImage.ashx?referrer=' + document.referrer + '\" border=\"0\" alt=\"Click here for live chat\" /></a>');");

			context.Response.ContentType = "text/javascript";
			context.Response.Write(js.ToString());
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
