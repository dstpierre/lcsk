using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class ChatMessages : Manager
    {
        public static IList<ChatMessage> Fetch(string channelId, long lastId)
        {
            return ExecuteQuery((dc) =>
                {
                    var msgs = from m in dc.ChatMessages
                               orderby m.MessageId
                               where m.MessageId > lastId && m.ChannelId.ToString() == channelId
                               select m;

                    return msgs.ToList();
                }, "Unable to fetch chat messages.");
        }

        public static long Create(string channelId, string fromName, string message)
        {
            return ExecuteQuery((dc) =>
                {
                    ChatMessage msg = new ChatMessage();
                    msg.ChannelId = new Guid(channelId);
                    msg.FromName = fromName;
                    msg.Message = message;
                    msg.SentDate = DateTime.Now;

                    dc.ChatMessages.InsertOnSubmit(msg);
                    dc.SubmitChanges();

                    return msg.MessageId;
                }, "Unable to create new chat message.");
        }

        public static bool HasNewMessages(string channelId, long lastId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.ChatMessages.Count(c => c.ChannelId.ToString() == channelId && c.MessageId > lastId) > 0;
                }, "Unable to check for new message.");
        }
    }
}
