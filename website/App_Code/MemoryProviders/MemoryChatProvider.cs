#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/17
 * Comment		: Memory Chat Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Web.Caching;



/// <summary>
/// Memory Chat Provider
/// </summary>
public class MemoryChatProvider : ChatProvider
{
	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "MemoryChatProvider";

		base.Initialize(name, config);
	}

	public override string RequestChat(ChatRequestInfo request)
	{
		bool cacheExists;
		List<ChatRequestInfo> requests = GetCurrentRequest(out cacheExists);

		// Add the new requests
		requests.Add(request);

		SaveRequests(cacheExists, requests);

        return request.ChatId;
	}

	private static void SaveRequests(bool cacheExists, List<ChatRequestInfo> requests)
	{
		if (cacheExists)
		{
			HttpContext.Current.Cache["_lcsk_requests"] = requests;
		}
		else
		{
			HttpContext.Current.Cache.Add("_lcsk_requests", requests, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5), CacheItemPriority.Normal, null);
		}
	}

	private static List<ChatRequestInfo> GetCurrentRequest(out bool cacheExists)
	{
		// get the current in-memory list
		cacheExists = false;
		List<ChatRequestInfo> requests;
		if( HttpContext.Current.Cache["_lcsk_requests"] != null )
		{
			requests = (List<ChatRequestInfo>)HttpContext.Current.Cache["_lcsk_requests"];
			cacheExists = true;
		}
		else
		{
			requests = new List<ChatRequestInfo>();
		}

		return requests;
	}

	public override void AddChatMessage(ChatMessageInfo msg)
	{
		bool cacheExists = false;
		List<ChatMessageInfo> messages = GetChatMessages(msg.ChatId, out cacheExists);

        // Get the last ID
        messages.Sort(SortByIDDescending);

        if (messages.Count > 0)
            msg.MessageId = messages[0].MessageId + 1;
        else
            msg.MessageId = 0;

		// Add the new requests
		messages.Add(msg);

		WriteChatMessages(msg.ChatId, cacheExists, messages);
	}

	private static void WriteChatMessages(string chatId, bool cacheExists, List<ChatMessageInfo> messages)
	{
		if (cacheExists)
		{
			HttpContext.Current.Cache["_lcsk_" + chatId] = messages;
		}
		else
		{
			HttpContext.Current.Cache.Add("_lcsk_" + chatId, messages, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(2), CacheItemPriority.Normal, null);
		}
	}

	private static List<ChatMessageInfo> GetChatMessages(string chatId, out bool cacheExists)
	{
		// get the current in-memory list
		List<ChatMessageInfo> messages;
		cacheExists = false;
		if (HttpContext.Current.Cache["_lcsk_" + chatId] != null)
		{
			messages = (List<ChatMessageInfo>)HttpContext.Current.Cache["_lcsk_" + chatId];
			cacheExists = true;
		}
		else
		{
			messages = new List<ChatMessageInfo>();
		}
		return messages;
	}

	public override List<ChatMessageInfo> GetMessages(string chatId, long lastCheck)
	{
		bool cacheExists = false;
		List<ChatMessageInfo> messages = GetChatMessages(chatId, out cacheExists);
		List<ChatMessageInfo> notViewed = new List<ChatMessageInfo>();
		// If we got at least one message
		if (messages.Count > 0)
		{
			// We sort by MessageId and get only those messages that are not read
			messages.Sort(ChatMessageInfo.SortByDate);

			foreach (ChatMessageInfo msg in messages)
			{
				if (msg.SentDate > lastCheck)
					notViewed.Add(msg);
				else
					break;
			}
		}
		return notViewed;
	}

	public override List<ChatRequestInfo> GetChatRequests(int operatorId)
	{
		bool cacheExists;
		List<ChatRequestInfo> results = new List<ChatRequestInfo>();
		foreach (ChatRequestInfo req in GetCurrentRequest(out cacheExists))
		{
			if (req.AcceptByOpereratorId == operatorId)
				results.Add(req);
		}

		return results;
	}

	public override void RemoveChatRequest(ChatRequestInfo req)
	{
		bool cacheExists;
		List<ChatRequestInfo> requests = GetCurrentRequest(out cacheExists);

		for (int i = 0; i < requests.Count; i++)
		{
			if (requests[i].ChatId == req.ChatId)
			{
				requests.RemoveAt(i);
				break;
			}
		}

		SaveRequests(cacheExists, requests);
	}

    public override bool HasNewMessage(string chatId, long lastMessageId)
    {
        bool cacheExists = false;
        List<ChatMessageInfo> messages = GetChatMessages(chatId, out cacheExists);

        if (messages != null && messages.Count > 0)
        {
            messages.Sort(SortByIDDescending);

            return messages[0].MessageId > lastMessageId;
        }
        else
            return false;

        
    }

    private int SortByIDDescending(ChatMessageInfo x, ChatMessageInfo y)
    {
        if (x != null && y != null)
            return x.MessageId < y.MessageId ? 1 : -1;
        else
            return 1;
    }
}
