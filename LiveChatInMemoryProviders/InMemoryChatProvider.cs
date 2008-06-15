using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;
using LiveChat.Providers;


namespace LiveChat.InMemoryProvider
{
    public class InMemoryChatProvider : ChatProvider
    {
        public override string RequestChat(ChatRequestEntity chatRequest)
        {
            throw new NotImplementedException();
        }

        public override void WriteMessage(MessageEntity msg)
        {
            throw new NotImplementedException();
        }

        public override List<MessageEntity> GetMessages(string channelId, long lastId)
        {
            throw new NotImplementedException();
        }

        public override void RemoveChatRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public override bool HasNewMessage(string channelId, long lastId)
        {
            throw new NotImplementedException();
        }
    }
}
