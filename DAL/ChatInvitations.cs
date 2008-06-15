using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class ChatInvitations : Manager
    {
        public static IList<ChatInvitation> Fetch(string visitorIp)
        {
            return ExecuteQuery((dc) =>
                {
                    var inv = from i in dc.ChatInvitations
                              where i.VisitorIp == visitorIp && !i.WasAccept
                              select i;

                    return inv.ToList();
                }, "Unable to fetch for chat invitations.");
        }

        public static bool HasInvitation(string visitorIp)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.ChatInvitations.Count(i => i.VisitorIp == visitorIp && !i.WasAccept) > 0;
                }, "Unable to check for chat invitation.");
        }

        public static bool Create(int operatorId, string message, string visitorIp)
        {
            return ExecuteNonQuery((dc) =>
                {
                    ChatInvitation inv = new ChatInvitation();
                    inv.OperatorId = operatorId;
                    inv.Message = message;
                    inv.VisitorIp = visitorIp;
                    inv.RequestDate = DateTime.Now;
                    inv.WasAccept = false;

                    dc.ChatInvitations.InsertOnSubmit(inv);
                    dc.SubmitChanges();
                    return true;
                }, "Unable to create new chat invitation");
        }

        public static bool Remove(ChatInvitation invitation)
        {
            return ExecuteNonQuery((dc) =>
            {
                dc.ChatInvitations.DeleteOnSubmit(invitation);
                dc.SubmitChanges();
                return true;
            }, "Unable to remove chat invitation for the moment.");
        }

        public static bool Remove(IList<ChatInvitation> invitations)
        {
            return ExecuteNonQuery((dc) =>
                {
                    foreach (var i in invitations)
                        dc.ChatInvitations.DeleteOnSubmit(i);

                    dc.SubmitChanges();
                    return true;
                }, "Unable to remove chat invitation for the moment.");
        }
    }
}
