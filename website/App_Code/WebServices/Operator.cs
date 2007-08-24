#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Service called by the operator's console
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;


    /// <summary>
    /// Contains all functionality for an operator to maintain
    /// a chat session with a client.
    /// </summary>
[WebService(Namespace = "http://www.dominicstpierre.net/LiveChatStarterKit/2007/09")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Operator : System.Web.Services.WebService
{
    public AuthenticationHeader Authentication;

	public Operator()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public OperatorInfo LogIn(string userName, string password)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");

		return OperatorService.LogIn(userName, password);
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public List<RequestInfo> GetWebSiteRequests(DateTime lastRequestTime)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
		return RequestService.GetRequest(lastRequestTime);
	}
    
    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public void SetOperatorStatus(int operatorId, bool isOnline)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
		OperatorService.UpdateStatus(operatorId, isOnline);
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public List<ChatRequestInfo> GetChatRequests(OperatorInfo op)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
        return OperatorService.GetChatRequests(op.OperatorId);
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public void AddMessage(ChatMessageInfo msg)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
		ChatService.AddMessage(msg);
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public void RemoveChatRequest(ChatRequestInfo req)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
		ChatService.RemoveChatRequest(req);
	}

    [SoapHeader("Authentication", Required = true)]
	[WebMethod]
	public List<ChatMessageInfo> GetChatMessages(string chatId, long lastCheck)
	{
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
		return ChatService.GetMessages(chatId, lastCheck);
	}

    [SoapHeader("Authentication", Required = true)]
    [WebMethod]
    public bool IsTyping(string chatId, bool isOperator)
    {
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
        bool retVal = false;
        HttpContext ctx = HttpContext.Current;
        if (ctx != null)
        {
            string who = isOperator ? "op" : "visitor";
            if (ctx.Application[chatId + "_" + who + "_typing"] != null)
                retVal = (bool)ctx.Application[chatId + "_" + who + "_typing"];
            
            // Clean up application variables
            DateTime lastCleanUp = DateTime.Now;
            DateTime now = DateTime.Now;
            if (ctx.Application["lastCleanUp"] != null)
                lastCleanUp = (DateTime)ctx.Application["lastCleanUp"];
            else
                ctx.Application.Add("lastCleanUp", DateTime.Now);

            TimeSpan duration = now - lastCleanUp;

            if ( duration.Seconds > 45 )
            {
                List<string> keyNames = new List<string>();
                foreach (string key in ctx.Application.Keys)
                    keyNames.Add(key);

                foreach (string key in keyNames)
                {
                    if (key.EndsWith("_typing"))
                        ctx.Application.Remove(key);
                }
                ctx.Application["lastCleanUp"] = DateTime.Now;
            }
            
        }
        return retVal;
    }

    [SoapHeader("Authentication", Required = true)]
    [WebMethod]
    public void SetTyping(string chatId, bool isOperator, bool typing)
    {
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
        HttpContext ctx = HttpContext.Current;
        if (ctx != null)
        {
            string who = isOperator ? "op" : "visitor";
            if (ctx.Application[chatId + "_" + who + "_typing"] != null)
                ctx.Application[chatId + "_" + who + "_typing"] = typing;
            else
                ctx.Application.Add(chatId + "_" + who + "_typing", typing);
        }
     }

    [SoapHeader("Authentication", Required = true)]
    [WebMethod]
    public List<OperatorInfo> GetOnlineOperator()
    {
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
        return OperatorService.GetOnlineOperator();
    }

    [SoapHeader("Authentication", Required = true)]
    [WebMethod]
    public void TransferChat(ChatRequestInfo chatRequest)
    {
        if (Authentication.userName != System.Configuration.ConfigurationManager.AppSettings["WSUser"].ToString())
            throw new AccessViolationException("invalid user");
        ChatService.RequestChat(chatRequest);
    }
}