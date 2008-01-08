using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using LiveChat.Entities;

namespace LiveChat.WebSite.WebServices
{
    [WebService(Namespace="http://www.dominicstpierre.net/LiveChat/WebServices/")]
    [WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // [System.Web.Script.Services.ScriptService]
    public class Operators : System.Web.Services.WebService
    {
        public AuthHeader authenticationHeader;

        private bool ValidateKey(string key)
        {
            return Config.ServiceKey == (key == null ? string.Empty : key);
        }



        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public OperatorEntity LogIn(string name, string password)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");

            return OperatorService.LogIn(name, password);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public List<PageRequestEntity> GetPageRequested(DateTime since)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            return RequestService.GetRequests(since);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public void SetOperatorStatus(int operatorId, bool isOnline)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            OperatorService.UpdateStatus(operatorId, isOnline);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public List<ChannelEntity> GetPendingChannel(int operatorId)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            return OperatorService.GetChatChannel(operatorId);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public void AddMessage(MessageEntity msg)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            ChatService.WriteMessage(msg);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public void RemoveChatRequest(int requestId)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            ChatService.RemoveChatRequest(requestId);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public List<MessageEntity> GetChatMessages(string channelId, long lastId)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            return ChatService.GetMessages(channelId, lastId);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public bool IsTyping(string channelId, bool isOperator)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            bool retVal = false;
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                string who = isOperator ? "op" : "visitor";
                if (ctx.Application[channelId + "-" + who + "typing"] != null)
                    retVal = (bool)ctx.Application[channelId + "-" + who + "typing"];

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
                        if (key.EndsWith("typing"))
                            ctx.Application.Remove(key);
                    }
                    ctx.Application["lastCleanUp"] = DateTime.Now;
                }

            }
            return retVal;
        }

        [WebMethod]
        public void SetTyping(string channelId, bool isOperator, bool typing)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                string who = isOperator ? "op" : "visitor";
                if (ctx.Application[channelId + "-" + who + "typing"] != null)
                    ctx.Application[channelId + "-" + who + "typing"] = typing;
                else
                    ctx.Application.Add(channelId + "-" + who + "typing", typing);
            }
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public List<OperatorEntity> GetOnlineOperator()
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            return OperatorService.GetOnlineOperator();
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public void TransferChat(ChatRequestEntity request)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            ChatService.RequestChat(request);
        }

        [WebMethod]
        [SoapHeader("authenticationHeader")]
        public bool HasNewMessage(string channelId, long lastId)
        {
            if (!ValidateKey(authenticationHeader.Key))
                throw new AccessViolationException("The service key is not valid.");
            return ChatService.HasNewMessage(channelId, lastId);
        }
    }
}
