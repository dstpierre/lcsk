using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveChat.Entities;
using LiveChat.BusinessLogic;
using System.Drawing.Imaging;

namespace LiveChat.WebSite
{
	public class ProcessImage : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			string referrer = string.Empty;
			string pageRequested = string.Empty;
			string visitorUserAgent = string.Empty;
			string visitorIP = string.Empty;
			string imgName = string.Empty;
			bool opOnline = false;

			// Add to VisitorHistories
			if (context.Request.QueryString["referrer"] != null)
				referrer = context.Request.QueryString["referrer"].ToString();

			if (context.Request.UserHostAddress != null)
				visitorIP = context.Request.UserHostAddress.ToString();

			if (context.Request.UrlReferrer != null)
			{
				pageRequested = context.Request.UrlReferrer.ToString();
			}

			if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
				visitorUserAgent = context.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

			PageRequestEntity req = new PageRequestEntity();
			req.VisitorIp = visitorIP;
			req.PageRequested = pageRequested;
			req.Referrer = referrer;
			req.UserAgent = visitorUserAgent ?? "";
			req.RequestedDate = DateTime.Now;

			LiveChat.BusinessLogic.WebRequest.LogRequest(req);

			// we get the status of the operators
			imgName = "offline.jpg";
			if (Operator.IsOperatorOnline())
				imgName = "online.jpg";

			System.Drawing.Image returnImg = System.Drawing.Image.FromFile(context.Server.MapPath("Images/" + imgName));
			returnImg.Save(context.Response.OutputStream, ImageFormat.Jpeg);

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
