using System;
using System.Configuration.Provider;
using System.Collections.Generic;
using LiveChat.Entities;


namespace LiveChat.Providers
{
    public abstract class ChatProvider : ProviderBase
    {
        public abstract string RequestChat(ChatRequestEntity chatRequest);
        public abstract void WriteMessage(MessageEntity msg);
        public abstract List<MessageEntity> GetMessages(string channelId, long lastId);
        public abstract void RemoveChatRequest(int requestId);
        public abstract bool HasNewMessage(string channelId, long lastId);
    }

}