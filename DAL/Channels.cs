using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class Channels : Manager
    {
        public static IList<Channel> Fetch(int operatorId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.Channels.Where(c => c.OperatorId == operatorId && c.AcceptDate == null).ToList();
                }, "Uneble to fetch the chat channel.");
        }

        public static Channel Fetch(string channelId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.Channels.FirstOrDefault(c => c.ChannelId.ToString() == channelId);
                }, "Uneble to fetch the chat channel.");
        }

        public static bool Save(Channel entity)
        {
            return ExecuteNonQuery((dc) =>
                {
                    Channel c = dc.Channels.FirstOrDefault(ch => ch.ChannelId == entity.ChannelId);
                    if( c != null )
                    {
                        c.AcceptDate = entity.AcceptDate;
                        c.CloseDate = entity.CloseDate;

                        dc.SubmitChanges();
                        return true;
                    }
                    return false;
                }, "Uneble to update the chat channel.");
        }

        public static bool Remove(string channelId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    Channel c = dc.Channels.FirstOrDefault(ch => ch.ChannelId.ToString() == channelId);
                    if (c != null)
                    {
                        dc.Channels.DeleteOnSubmit(c);
                        dc.SubmitChanges();
                        return true;
                    }
                    return false;
                }, "Unable to remove the chat channel.");
        }

        public static bool Remove(int requestId)
        {
            return ExecuteNonQuery((dc) =>
            {
                Channel c = dc.Channels.FirstOrDefault(ch => ch.RequestId == requestId);
                if (c != null)
                {
                    dc.Channels.DeleteOnSubmit(c);
                    dc.SubmitChanges();
                    return true;
                }
                return false;
            }, "Unable to remove the chat channel.");
        }
    }
}
