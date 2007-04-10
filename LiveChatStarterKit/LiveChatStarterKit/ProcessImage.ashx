<%@ WebHandler Language="C#" Class="ProcessImage" %>
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Display the livechat image and log the http request
 * 
 * History:
 * 
 */

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

public class ProcessImage : IHttpHandler
{

	public void ProcessRequest(HttpContext context)
	{
		string referrer = string.Empty;
		string pageRequested = string.Empty;
		string domainRequested = string.Empty;
		string visitorUserAgent = string.Empty;
		string visitorIP = string.Empty;
		string imgName = string.Empty;
		bool opOnline = false;

		// Add Request Log
		if (context.Request.QueryString["referrer"] != null)
			referrer = context.Request.QueryString["referrer"].ToString();

		if (context.Request.UserHostAddress != null)
			visitorIP = context.Request.UserHostAddress.ToString();

		if (context.Request.UrlReferrer != null)
		{
			pageRequested = context.Request.UrlReferrer.ToString();
			domainRequested = Lib.GetDomainName(context.Request.UrlReferrer.ToString());
		}

		if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
			visitorUserAgent = context.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

		// Log the request
		RequestInfo req = new RequestInfo();
		req.DomainRequested = domainRequested;
		req.PageRequested = pageRequested;
		req.Referrer = referrer;
		req.VisitorIP = visitorIP;
		req.VisitorUserAgent = visitorUserAgent;
		req.RequestTime = DateTime.Now;

		RequestService.LogRequest(req);

		// we get the status of the operators
		opOnline = OperatorService.GetOperatorStatus();

		if (opOnline)
			imgName = "online.jpg";
		else
			imgName = "offline.jpg";

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