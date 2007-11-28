using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;
using LiveChat.DAL;
using LiveChat.Providers;

namespace LiveChat.SQLProvider
{
    public class SQLChatProvider : ChatProvider
    {
        public override string RequestChat(ChatRequestEntity chatRequest)
        {
            return ChatRequests.Create(chatRequest.VisitorIp, chatRequest.VisitorName, chatRequest.VisitorEmail, chatRequest.DepartmentId, chatRequest.SendCopyOfChat).ChannelId.ToString();
        }

        public override void WriteMessage(MessageEntity msg)
        {
            ChatMessages.Create(msg.ChannelId, msg.FromName, msg.Message);
        }

        public override List<MessageEntity> GetMessages(string channelId, long lastId)
        {
            List<MessageEntity> results = new List<MessageEntity>();
            MessageEntity entity = null;
            foreach (var m in ChatMessages.Fetch(channelId, lastId))
            {
                entity = new MessageEntity();
                entity.EntityId = m.MessageId;
                entity.ChannelId = m.ChannelId.ToString();
                entity.FromName = m.FromName;
                entity.Message = m.Message;
                entity.SendDate = m.SentDate;
                results.Add(entity);
            }
            return results;
        }

        public override void RemoveChatRequest(int requestId)
        {
            ChatRequests.Remove(requestId);
        }

        public override bool HasNewMessage(string channelId, long lastId)
        {
            return ChatMessages.HasNewMessages(channelId, lastId);
        }
    }
}
