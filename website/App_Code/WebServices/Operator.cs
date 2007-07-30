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
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Operator : System.Web.Services.WebService
{
	public Operator()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	[WebMethod]
	public OperatorInfo LogIn(string userName, string password)
	{
		return OperatorService.LogIn(userName, password);
	}

	[WebMethod]
	public List<RequestInfo> GetWebSiteRequests(DateTime lastRequestTime)
	{
		return RequestService.GetRequest(lastRequestTime);
	}

	[WebMethod]
	public void SetOperatorStatus(int operatorId, bool isOnline)
	{
		OperatorService.UpdateStatus(operatorId, isOnline);
	}

	[WebMethod]
	public List<ChatRequestInfo> GetChatRequests(OperatorInfo op)
	{
        return OperatorService.GetChatRequests(op.OperatorId);
	}

	[WebMethod]
	public void AddMessage(ChatMessageInfo msg)
	{
		ChatService.AddMessage(msg);
	}

	[WebMethod]
	public void RemoveChatRequest(ChatRequestInfo req)
	{
		ChatService.RemoveChatRequest(req);
	}

	[WebMethod]
	public List<ChatMessageInfo> GetChatMessages(string chatId, long lastCheck)
	{
		return ChatService.GetMessages(chatId, lastCheck);
	}

    [WebMethod]
    public bool IsTyping(string chatId, bool isOperator)
    {
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

    [WebMethod]
    public void SetTyping(string chatId, bool isOperator, bool typing)
    {
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
}