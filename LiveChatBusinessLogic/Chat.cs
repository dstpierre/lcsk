using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;
using LiveChat.Entities;
using LiveChat.Providers;

namespace LiveChat.BusinessLogic
{
    public class Chat
    {
        private static ChatProvider _provider = LiveChat.Providers.Manager.Chat.Provider;
        //private static object _lock = new object();

        public ChatProvider Provider
        {
            get { return _provider; }
        }

        public static string RequestChat(ChatRequestEntity request)
        {
            // Load the provider
            //LoadProvider();

            return _provider.RequestChat(request);
        }

        public static void WriteMessage(MessageEntity msg)
        {
            // Load the provider
            //LoadProvider();

            _provider.WriteMessage(msg);
        }

        public static List<MessageEntity> GetMessages(string channelId, long lastId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetMessages(channelId, lastId);
        }

        public static void RemoveChatRequest(int requestId)
        {
            // Load the provider
            //LoadProvider();

            _provider.RemoveChatRequest(requestId);
        }

        public static bool HasNewMessage(string channelId, long lastId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.HasNewMessage(channelId, lastId);
        }
    }
}
