using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LCSK.Core;
using LCSK.Services;

namespace LCSK.Web.LCSKServices
{
	[WebService(Namespace = "http://focuscentric.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	public class OpServices : System.Web.Services.WebService
	{
		private bool IsAuthenticated(Guid key)
		{
			var ctx = HttpContext.Current;
			if (ctx != null)
				return ctx.Application[key.ToString()] != null;
			return false;
		}

		private Guid SetAuthentication(int operatorId)
		{
			var ctx = HttpContext.Current;
			if (ctx != null)
			{
				var key = Guid.NewGuid();
				ctx.Application.Add(key.ToString(), operatorId);
				return key;
			}
			return Guid.Empty;
		}

		[WebMethod]
		public Operator LogIn(string userName, string password)
		{
			var op = OperatorService.LogIn(userName, password);
			if (op != null)
			{
				op.Password = SetAuthentication(op.OperatorId).ToString();
			}
			return op;
		}

		[WebMethod]
		public List<WebRequest> GetWebSiteRequests(DateTime lastRequestTime)
		{
			return RequestService.GetRequest(lastRequestTime);
		}

		[WebMethod]
		public void SetOperatorStatus(Guid key, int operatorId, bool isOnline)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");

			OperatorService.UpdateStatus(operatorId, isOnline);
		}

		[WebMethod]
		public List<ChatRequest> GetChatRequests(Guid key, Operator op)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			return OperatorService.GetChatRequests(op.OperatorId, op.DepartmentList());
		}

		[WebMethod]
		public void AddMessage(Guid key, ChatMessage msg)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			ChatService.AddMessage(msg);
		}

		[WebMethod]
		public void RemoveChatRequest(Guid key, ChatRequest req)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			ChatService.RemoveChatRequest(req);
		}

		[WebMethod]
		public List<ChatMessage> GetChatMessages(Guid key, Guid chatId, long lastCheck)
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

				if (duration.Seconds > 45)
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

		[WebMethod]
		public List<Operator> GetOnlineOperator()
		{
			return OperatorService.GetOnlineOperator();
		}

		[WebMethod]
		public void TransferChat(ChatRequest chatRequest)
		{
			ChatService.RequestChat(chatRequest);
		}

		[WebMethod]
		public bool HasNewMessage(Guid chatId, long lastMessageId)
		{
			return ChatService.HasNewMessage(chatId, lastMessageId);
		}

		[WebMethod]
		public List<Operator> GetOperators(Guid key)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			return OperatorService.List();
		}

		[WebMethod]
		public bool Save(Guid key, Operator op)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			return OperatorService.Save(op);
		}

		[WebMethod]
		public bool Delete(Guid key, Operator op)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			return OperatorService.Delete(op);
		}

		[WebMethod]
		public List<WebRequest> VisitorPages(string visitorIp)
		{
			return RequestService.VisitorPages(visitorIp);
		}

		[WebMethod]
		public ChatRequest Invite(Guid key, int operatorId, string visitorIp, string prompt)
		{
			if (!IsAuthenticated(key))
				throw new Exception("The key is not authenticated");
			return OperatorService.InviteVisitor(operatorId, visitorIp, prompt);
		}
	}
}
