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
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using LCSK.Core;
using LCSK.Providers;

namespace LCSK.Services
{
	public class ChatService
	{
		private static ChatProvider _provider = null;
		private static object _lock = new object();

		public ChatProvider Provider
		{
			get { return _provider; }
		}

		public static void RequestChat(ChatRequest request)
		{
			// Load the provider
			LoadProvider();

			_provider.RequestChat(request);
		}

		public static bool AcceptRequest(Guid chatId, int operatorId)
		{
			if (_provider == null)
				LoadProvider();

			return _provider.AcceptRequest(chatId, operatorId);
		}

		public static void AddMessage(ChatMessage msg)
		{
			// Load the provider
			LoadProvider();

			_provider.AddChatMessage(msg);
		}

		public static List<ChatMessage> GetMessages(Guid chatId, long lastCheck)
		{
			// Load the provider
			LoadProvider();

			return _provider.GetMessages(chatId, lastCheck);
		}

		public static List<ChatRequest> GetRequests(int operatorId, string[] departments)
		{
			// Load the provider
			LoadProvider();

			return _provider.GetChatRequests(operatorId, departments);
		}

		public static void RemoveChatRequest(ChatRequest req)
		{
			// Load the provider
			LoadProvider();

			_provider.RemoveChatRequest(req);
		}

		public static bool HasNewMessage(Guid chatId, long lastMessageId)
		{
			// Load the provider
			LoadProvider();

			return _provider.HasNewMessage(chatId, lastMessageId);
		}

		public static SendTranscriptViewModel GetTranscript(Guid chatId)
		{
			if (_provider == null)
				LoadProvider();

			return _provider.GetTranscript(chatId);
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
						ChatServiceSection section = (ChatServiceSection)WebConfigurationManager.GetSection("LCSK/chatService");

						// Load the default provider
						if (section.Providers.Count > 0 && !string.IsNullOrEmpty(section.DefaultProvider) && section.Providers[section.DefaultProvider] != null)
							_provider = (ChatProvider)ProvidersHelper.InstantiateProvider(section.Providers[section.DefaultProvider], typeof(ChatProvider));

						if (_provider == null)
							throw new ProviderException("Unable to load the ChatProvider");
					}
				}
			}
		}
	}
}