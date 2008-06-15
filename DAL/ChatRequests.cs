using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class ChatRequests : Manager
    {
        public static IList<ChatRequest> Fetch(int departmentId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.ChatRequests.Where(cr => cr.DepartmentId == departmentId).ToList();
                }, "Unable to fetch chat requests.");
        }

        public static Channel Create(string visitorIp, string visitorName, string visitorEmail, int departmentId, bool sendCopyOfChat)
        {
            return ExecuteQuery((dc) =>
                {
                    ChatRequest cr = new ChatRequest();
                    cr.VisitorIp = visitorIp;
                    cr.VisitorName = visitorName;
                    cr.VisitorEmail = visitorEmail;
                    cr.DepartmentId = departmentId;
                    cr.SendCopyOfChat = sendCopyOfChat;
                    cr.RequestedDate = DateTime.Now;

                    dc.ChatRequests.InsertOnSubmit(cr);

                    // Queue the chat for the most available operator in that department
                    var operators = from o in dc.Operators
                             join c in dc.Channels on o.OperatorId equals c.OperatorId into oc
                             orderby oc.Count()
                             select new { OperatorId = o.OperatorId, OpenChannel = oc.Count() };

                    var op = operators.FirstOrDefault();
                    if (op != null)
                    {
                        Channel c = new Channel();
                        c.ChannelId = Guid.NewGuid();
                        c.OpenDate = DateTime.Now;
                        c.AcceptDate = null;
                        c.CloseDate = null;
                        c.OperatorId = op.OperatorId;
                        c.ChatRequest = cr;

                        dc.Channels.InsertOnSubmit(c);
                        dc.SubmitChanges();

                        return c;
                    }
                    else
                        return null;
                }, "Unable to add chat request.");
        }

        public static bool Remove(int requestId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    var r = dc.ChatRequests.FirstOrDefault(cr => cr.RequestId == requestId);
                    if (r != null)
                    {
                        Channels.Remove(r.RequestId);
                        dc.ChatRequests.DeleteOnSubmit(r);
                        dc.SubmitChanges();
                        return true;
                    }
                    return false;
                }, "Unable to remove chat request.");
        }
    }
}
