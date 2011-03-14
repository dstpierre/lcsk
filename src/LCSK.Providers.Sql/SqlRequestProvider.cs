#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Memory Request Provider
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
	public class SqlRequestProvider : RequestProvider
	{
		private string connectionString = string.Empty;

		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			// check to ensure config is not null
			if (config == null)
				throw new ArgumentNullException("config");

			if (string.IsNullOrEmpty(name))
				name = "SqlRequestProvider";

			// Check for the connection string
			if (config["connectionStringName"] != null && !string.IsNullOrEmpty(config["connectionStringName"].ToString()) && 
				ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()] != null && !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString()))
			{
				connectionString = ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString();
				config.Remove("connectionStringName");
			}
			else
				throw new ArgumentNullException("The ConnectionStringName is not defined");

			base.Initialize(name, config);
		}

		public override bool LogRequest(WebRequest req)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				LiveChat_LogAccess webrequest = new LiveChat_LogAccess();
				webrequest.DomainRequested = req.DomainName;
				webrequest.PageRequested = req.PageRequested;
				webrequest.Referrer = req.Referrer;
				webrequest.RequestedTime = DateTime.Now;
				webrequest.VisitorIP = req.VisitorIp;
				webrequest.VisitorUserAgent = req.VisitorUserAgent.Length > 100 ? req.VisitorUserAgent.Substring(0, 100) : req.VisitorUserAgent;

				db.LiveChat_LogAccesses.InsertOnSubmit(webrequest);
				db.SubmitChanges();
				return true;
			}
		}

		public override List<WebRequest> GetRequest(DateTime lastRequestDate)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var last = from a in db.LiveChat_LogAccesses
						   where a.RequestedTime > lastRequestDate
						   group a by a.VisitorIP into g
						   select new
						   {
							   Id = (from log in g select log.LogAccessID).Max(),
							   Ip = g.Key
						   };

				var requests = from a in db.LiveChat_LogAccesses
							   join l in last on a.LogAccessID equals l.Id
							   select a;

				List<WebRequest> results = new List<WebRequest>();
				foreach (var r in requests.ToList())
				{
					results.Add(new WebRequest()
					{
						DomainName = r.DomainRequested,
						PageRequested = r.PageRequested,
						Referrer = r.Referrer,
						Requested = r.RequestedTime,
						RequestId = r.LogAccessID,
						VisitorIp = r.VisitorIP,
						VisitorUserAgent = r.VisitorUserAgent
					});
				}

				return results;
			}
		}

		public override List<WebRequest> VisitorPages(string visitorIp)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var pages = db.LiveChat_LogAccesses.OrderBy(x => x.LogAccessID).Where(x =>
					x.RequestedTime >= DateTime.Now.AddDays(-1)).ToList();

				List<WebRequest> results = new List<WebRequest>();
				foreach (var p in pages)
				{
					results.Add(new WebRequest()
					{
						DomainName = p.DomainRequested,
						PageRequested = p.PageRequested,
						Referrer = p.Referrer,
						Requested = p.RequestedTime,
						RequestId = p.LogAccessID,
						VisitorIp = p.VisitorIP,
						VisitorUserAgent = p.VisitorUserAgent
					});
				}

				return results;
			}
		}

		public override ChatRequest CheckForInvites(string visitorIp)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var invites = db.LiveChat_ChatRequests.OrderByDescending(x => x.RequestDate).Where(x =>
					x.VisitorIP == visitorIp && x.Department == "op-invite" && x.AcceptDate == null && x.ClosedDate == null).ToList();

				if(invites != null && invites.Count() > 0)
				{
					// do they have more than 1 invite
					var invite = invites.First();
					if (invites.Count() > 1)
					{
						invites.Remove(invite);

						foreach (var inv in invites)
						{
							inv.ClosedDate = DateTime.Now;
							inv.Department = "Visitor had more than 1 invite pending...";
						}

						db.SubmitChanges();
					}

					ChatRequest req = new ChatRequest();
					req.Accepted = null;
					req.ChatId = invite.ChatID;
					req.Closed = null;
					req.Department = invite.Department;
					req.OperatorId = invite.OperatorID;
					req.Requested = invite.RequestDate;
					req.VisitorEmail = invite.VisitorEmail;
					req.VisitorIp = invite.VisitorIP;
					req.VisitorName = invite.VisitorName;
					req.VisitorUserAgent = invite.VisitorUserAgent;
					req.WasAccepted = false;

					return req;
				}
				return null;
			}
		}

		public override bool AcceptInvite(ChatRequest req)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var entity = db.LiveChat_ChatRequests.SingleOrDefault(x => x.ChatID == req.ChatId);
				if (entity != null)
				{
					entity.AcceptDate = DateTime.Now;
					entity.Department = "visitor-accept";

					db.SubmitChanges();
					return true;
				}
				return false;
			}
		}

		public override bool RejectInvite(ChatRequest req)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var entity = db.LiveChat_ChatRequests.SingleOrDefault(x => x.ChatID == req.ChatId);
				if (entity != null)
				{
					entity.ClosedDate = DateTime.Now;
					entity.Department = "visitor-rejected";

					db.SubmitChanges();
					return true;
				}
				return false;
			}
		}
	}
}