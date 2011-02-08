#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Chat Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Collections.Generic;
using LCSK.Core;

namespace LCSK.Providers
{
	public abstract class ChatProvider : ProviderBase
	{
		public abstract Guid RequestChat(ChatRequest request);
		public abstract bool AcceptRequest(Guid id, int operatorId);
		public abstract void AddChatMessage(ChatMessage msg);
		public abstract List<ChatMessage> GetMessages(Guid chatId, long lastCheck);
		public abstract List<ChatRequest> GetChatRequests(int operatorId, string[] departments);
		public abstract void RemoveChatRequest(ChatRequest req);
		public abstract bool HasNewMessage(Guid chatId, long lastMessageId);

		public abstract SendTranscriptViewModel GetTranscript(Guid chatId);
	}

}