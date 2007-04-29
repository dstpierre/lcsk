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
}