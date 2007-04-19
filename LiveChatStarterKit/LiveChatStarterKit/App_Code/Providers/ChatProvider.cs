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

/// <summary>
/// Summary description for ChatRequestProvider
/// </summary>
public abstract class ChatProvider  : ProviderBase
{
	public abstract void RequestChat(ChatRequestInfo request);
	public abstract void AddChatMessage(ChatMessageInfo msg);
	public abstract List<ChatMessageInfo> GetMessages(string chatId, int lastId);
	public abstract int GetLastMessageId(string chatId);
}
