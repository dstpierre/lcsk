#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/17
 * Comment		: Chat Provider Helper
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Collections.Generic;

/// <summary>
/// Chat Service
/// </summary>
public class ChatService
{
	private static ChatProvider _provider = null;
	private static object _lock = new object();

	public ChatProvider Provider
	{
		get { return _provider; }
	}

	public static void RequestChat(ChatRequestInfo request)
	{
		// Load the provider
		LoadProvider();

		_provider.RequestChat(request);
	}

	public static void AddMessage(ChatMessageInfo msg)
	{
		// Load the provider
		LoadProvider();

		_provider.AddChatMessage(msg);
	}

	public static List<ChatMessageInfo> GetMessages(string chatId, int lastId)
	{
		// Load the provider
		LoadProvider();

		return _provider.GetMessages(chatId, lastId);
	}

	public static int GetLastMessageId(string chatId)
	{
		// Load the provider
		LoadProvider();

		return _provider.GetLastMessageId(chatId);
	}

	public static List<ChatRequestInfo> GetRequests(bool active)
	{
		// Load the provider
		LoadProvider();

		return _provider.GetChatRequests(active);
	}

	public static void RemoveChatRequest(ChatRequestInfo req)
	{
		// Load the provider
		LoadProvider();

		_provider.RemoveChatRequest(req);
	}

	private static void LoadProvider()
	{
		// if we do not have initiated the provider
		if (_provider == null)
		{
			lock (_lock)
			{
				// Do this again to make sure _provider is still null
				if (_provider == null)
				{
					// Get a reference to the <requestService> section
					ChatServiceSection section = (ChatServiceSection)WebConfigurationManager.GetSection("system.web/chatService");

					// Load the provider
					if (section.Providers.Count > 0)
						_provider = (ChatProvider)ProvidersHelper.InstantiateProvider(section.Providers[0], typeof(ChatProvider));

					if (_provider == null)
						throw new ProviderException("Unable to load the ChatProvider");
				}
			}
		}
	}
}
