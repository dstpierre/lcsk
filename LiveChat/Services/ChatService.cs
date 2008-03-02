using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Collections.Generic;
using LiveChat.Entities;
using LiveChat.Providers.Manager;
using LiveChat.Providers;

namespace LiveChat.WebSite
{
    //public class ChatService
    //{
    //    private static ChatProvider _provider = Chat.Provider;
    //    //private static object _lock = new object();

    //    public ChatProvider Provider
    //    {
    //        get { return _provider; }
    //    }

    //    public static string RequestChat(ChatRequestEntity request)
    //    {
    //        // Load the provider
    //        //LoadProvider();

    //        return _provider.RequestChat(request);
    //    }

    //    public static void WriteMessage(MessageEntity msg)
    //    {
    //        // Load the provider
    //        //LoadProvider();

    //        _provider.WriteMessage(msg);
    //    }

    //    public static List<MessageEntity> GetMessages(string channelId, long lastId)
    //    {
    //        // Load the provider
    //        //LoadProvider();

    //        return _provider.GetMessages(channelId, lastId);
    //    }

    //    public static void RemoveChatRequest(int requestId)
    //    {
    //        // Load the provider
    //        //LoadProvider();

    //        _provider.RemoveChatRequest(requestId);
    //    }

    //    public static bool HasNewMessage(string channelId, long lastId)
    //    {
    //        // Load the provider
    //        //LoadProvider();

    //        return _provider.HasNewMessage(channelId, lastId);
    //    }

    //    //private static void LoadProvider()
    //    //{
    //    //    // if we do not have initiated the provider
    //    //    if (_provider == null)
    //    //    {
    //    //        lock (_lock)
    //    //        {
    //    //            // Do this again to make sure _provider is still null
    //    //            if (_provider == null)
    //    //            {
    //    //                // Get a reference to the <requestService> section
    //    //                ChatServiceSection section = (ChatServiceSection)WebConfigurationManager.GetSection("system.web/chatService");

    //    //                // Load the default provider
    //    //                if (section.Providers.Count > 0 && !string.IsNullOrEmpty(section.DefaultProvider) && section.Providers[section.DefaultProvider] != null)
    //    //                    _provider = (ChatProvider)ProvidersHelper.InstantiateProvider(section.Providers[section.DefaultProvider], typeof(ChatProvider));

    //    //                if (_provider == null)
    //    //                    throw new ProviderException("Unable to load the ChatProvider");
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}
}