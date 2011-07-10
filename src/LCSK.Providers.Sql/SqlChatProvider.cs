#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/17
 * Comment		: Memory Chat Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LCSK.Core;

namespace LCSK.Providers.Sql
{
	public class SqlChatProvider : ChatProvider
	{
		public string connectionString { get; set; }

		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			// check to ensure config is not null
			if (config == null)
				throw new ArgumentNullException("config");

			if (string.IsNullOrEmpty(name))
				name = "SqlChatProvider";

			// Check for the connection string
			if (config["connectionStringName"] != null && !string.IsNullOrEmpty(config["connectionStringName"].ToString()) && !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString()))
			{
				connectionString = ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString();
				config.Remove("connectionStringName");
			}
			else
				throw new ArgumentNullException("The ConnectionStringName is not defined");

			base.Initialize(name, config);
		}

		public override Guid RequestChat(ChatRequest request)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
                if (request.Department == null)
                    request.Department = "";

				var existing = db.LiveChat_ChatRequests.SingleOrDefault(x =>
					x.VisitorIP == request.VisitorIp &&
					x.OperatorID == -1);
				if (existing != null)
					return existing.ChatID;

				// Get the less busy operator
				int operatorId = -1;

				var results = from o in db.LiveChat_Operators
							  join c in db.LiveChat_ChatRequests on o.OperatorID equals c.OperatorID into req
							  from r in req.DefaultIfEmpty()
							  where o.IsOnline && o.Department.Contains(request.Department)
							  group o by o.OperatorID into g
							  select new { Id = g.Key, Count = g.Count() };

				if (results != null && results.Count() > 0)
					operatorId = results.OrderBy(x => x.Count).First().Id;
				else
				{
					// WTH
					operatorId = db.LiveChat_Operators.First(x => x.IsOnline).OperatorID;
				}

				LiveChat_ChatRequest entity = new LiveChat_ChatRequest();
				entity.ChatID = request.ChatId;
				entity.Department = request.Department;
				entity.OperatorID = operatorId;
				entity.RequestDate = DateTime.Now;
				entity.VisitorEmail = request.VisitorEmail;
				entity.VisitorIP = request.VisitorIp;
				entity.VisitorName = request.VisitorName;
				entity.VisitorUserAgent = request.VisitorUserAgent.Length > 100 ? request.VisitorUserAgent.Substring(0, 100) : request.VisitorUserAgent;

				db.LiveChat_ChatRequests.InsertOnSubmit(entity);
				db.SubmitChanges();
				return entity.ChatID;
			}
		}

		public override bool AcceptRequest(Guid id, int operatorId)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var req = db.LiveChat_ChatRequests.SingleOrDefault(x =>
					x.ChatID == id);

				if (req != null)
				{
					req.AcceptDate = DateTime.Now;

					db.SubmitChanges();

					return true;
				}
				return false;
			}
		}

		public override void AddChatMessage(ChatMessage msg)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				LiveChat_ChatMessage entity = new LiveChat_ChatMessage();
				entity.ChatID = msg.ChatId;
				entity.FromName = msg.Name;
				entity.Message = msg.Message;
				//TODO: Change SentDate to DateTime
				//entity.SentDate = DateTime.Now;

				db.LiveChat_ChatMessages.InsertOnSubmit(entity);
				db.SubmitChanges();
			}
		}

		public override List<ChatMessage> GetMessages(Guid chatId, long lastCheck)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var messages = db.LiveChat_ChatMessages.OrderBy(x => x.MessageID).Where(x =>
					x.ChatID == chatId && x.MessageID > lastCheck).ToList();

				List<ChatMessage> results = new List<ChatMessage>();
				foreach (var m in messages)
				{
					results.Add(new ChatMessage()
					{
						ChatId = m.ChatID,
						Message = m.Message,
						MessageId = m.MessageID,
						Name = m.FromName,
						Sent = DateTime.Now
					});
				}

				return results;
			}
		}

		public override List<ChatRequest> GetChatRequests(int operatorId, string[] departments)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var pending = db.LiveChat_ChatRequests.Where(x => x.AcceptDate == null &&
					(x.OperatorID == -1 || x.OperatorID == operatorId)).ToList();

				//TODO: Elegantly manage this in another way
				bool found = false;
				List<LiveChat_ChatRequest> toRemove = new List<LiveChat_ChatRequest>();
				foreach (var req in pending)
				{
					found = false;

					foreach (string dep in departments)
					{
						found = dep.ToLower() == req.Department.ToLower();
						if (found)
							break;
					}

					if (!found)
					{
						// if the request has been idle (not answer for more than 3 minutes
						if (req.RequestDate > DateTime.Now.AddMinutes(-3))
						{
							//TODO: Swtich operator
							toRemove.Add(req);
						}
					}
				}

				foreach (var r in toRemove)
					pending.Remove(r);

				List<ChatRequest> results = new List<ChatRequest>();
				foreach (var req in pending)
				{
					results.Add(new ChatRequest()
					{
						Accepted = req.AcceptDate,
						ChatId = req.ChatID,
						Closed = req.ClosedDate,
						Department = req.Department,
						OperatorId = req.OperatorID,
						Requested = req.RequestDate,
						VisitorEmail = req.VisitorEmail,
						VisitorIp = req.VisitorIP,
						VisitorName = req.VisitorName,
						VisitorUserAgent = req.VisitorUserAgent,
						WasAccepted = req.AcceptDate.HasValue
					});
				}

				return results;
			}
		}

		public override void RemoveChatRequest(ChatRequest req)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				db.LiveChat_ChatRequests.DeleteAllOnSubmit(db.LiveChat_ChatRequests.Where(x =>
					x.ChatID == req.ChatId));
				db.SubmitChanges();
			}
		}

		public override bool HasNewMessage(Guid chatId, long lastMessageId)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				return db.LiveChat_ChatMessages.Count(x =>
					x.ChatID == chatId && x.MessageID > lastMessageId) > 0;
			}
		}

		public override SendTranscriptViewModel GetTranscript(Guid chatId)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var request = db.LiveChat_ChatRequests.SingleOrDefault(x => x.ChatID == chatId);
				if (request != null)
				{
					SendTranscriptViewModel vm = new SendTranscriptViewModel();
					vm.Email = request.VisitorEmail;
					vm.Requested = request.RequestDate;
					vm.Accepted = request.AcceptDate ?? DateTime.MinValue;

					vm.Conversation = new List<ChatMessage>();

					foreach(var m in db.LiveChat_ChatMessages.OrderBy(x => x.MessageID)
						.Where(x => x.ChatID == chatId).ToList())
					{
						ChatMessage msg = new ChatMessage();
						msg.ChatId = m.ChatID;
						msg.Message = m.Message;
						msg.MessageId = m.MessageID;
						msg.Name = m.FromName;
						
						vm.Conversation.Add(msg);
					}

					return vm;
				}
				return null;
			}
		}

        public override List<ChatRequest> GetPendingRequests()
        {
            using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
            {
                var operators = db.LiveChat_Operators.ToList();
                var requests = db.LiveChat_ChatRequests.Where(x => x.AcceptDate == null).OrderBy(x => x.RequestDate);


                List<ChatRequest> pendingRequests = new List<ChatRequest>();
                foreach (var r in requests)
                {
                    pendingRequests.Add(new ChatRequest()
                    {
                        Accepted = r.AcceptDate,
                        ChatId = r.ChatID,
                        Closed = r.ClosedDate,
                        Department = r.Department,
                        OperatorId = r.OperatorID,
                        OperatorName = r.OperatorID > -1 ? 
                            operators.Single(x => x.OperatorID == r.OperatorID).OperatorName : "n/a",
                        Requested = r.RequestDate,
                        VisitorEmail = r.VisitorEmail,
                        VisitorIp = r.VisitorIP,
                        VisitorName = r.VisitorName,
                        VisitorUserAgent = r.VisitorUserAgent,
                        WasAccepted = r.AcceptDate != null
                    });
                }

                return pendingRequests;
            }
        }

        public override List<ChatRequest> GetCurrentSessions()
        {
            using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
            {
                var operators = db.LiveChat_Operators.ToList();
                var sessions = db.LiveChat_ChatRequests.Where(x => x.AcceptDate != null && x.ClosedDate == null).OrderBy(x =>
                    x.RequestDate);

                List<ChatRequest> currentSessions = new List<ChatRequest>();
                foreach (var r in sessions)
                {
                    currentSessions.Add(new ChatRequest()
                    {
                        Accepted = r.AcceptDate,
                        ChatId = r.ChatID,
                        Closed = r.ClosedDate,
                        Department = r.Department,
                        OperatorId = r.OperatorID,
                        OperatorName = r.OperatorID > -1 ?
                            operators.Single(x => x.OperatorID == r.OperatorID).OperatorName : "n/a",
                        Requested = r.RequestDate,
                        VisitorEmail = r.VisitorEmail,
                        VisitorIp = r.VisitorIP,
                        VisitorName = r.VisitorName,
                        VisitorUserAgent = r.VisitorUserAgent,
                        WasAccepted = r.AcceptDate != null
                    });
                }
                return currentSessions;
            }
        }

        public override List<ChatRequest> GetPendingInvitations()
        {
            using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
            {
                var operators = db.LiveChat_Operators.ToList();
                var invites = db.LiveChat_ChatRequests.Where(x => x.AcceptDate != null && x.Department == "op-invite").OrderBy(x =>
                    x.RequestDate);

                List<ChatRequest> pendingInvites = new List<ChatRequest>();
                foreach (var r in invites)
                {
                    pendingInvites.Add(new ChatRequest()
                    {
                        Accepted = r.AcceptDate,
                        ChatId = r.ChatID,
                        Closed = r.ClosedDate,
                        Department = r.Department,
                        OperatorId = r.OperatorID,
                        OperatorName = r.OperatorID > -1 ?
                            operators.Single(x => x.OperatorID == r.OperatorID).OperatorName : "n/a",
                        Requested = r.RequestDate,
                        VisitorEmail = r.VisitorEmail,
                        VisitorIp = r.VisitorIP,
                        VisitorName = r.VisitorName,
                        VisitorUserAgent = r.VisitorUserAgent,
                        WasAccepted = r.AcceptDate != null
                    });
                }
                return pendingInvites;
            }
        }
	}
}