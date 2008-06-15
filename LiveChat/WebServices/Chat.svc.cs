using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LiveChat.WCF
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class Chat : IChatService
    {

        public string RequestChat(LiveChat.Entities.ChatRequestEntity request)
        {
            return LiveChat.BusinessLogic.Chat.RequestChat(request);
        }

        public void WriteMessage(LiveChat.Entities.MessageEntity msg)
        {
            LiveChat.BusinessLogic.Chat.WriteMessage(msg);
        }

        public List<LiveChat.Entities.MessageEntity> GetMessages(string channelId, long lastId)
        {
            return LiveChat.BusinessLogic.Chat.GetMessages(channelId, lastId);
        }

        public void RemoveChatRequest(int requestId)
        {
            LiveChat.BusinessLogic.Chat.RemoveChatRequest(requestId);
        }

        public bool HasNewMessage(string channelId, long lastId)
        {
            return LiveChat.BusinessLogic.Chat.HasNewMessage(channelId, lastId);
        }

        
    }
}
