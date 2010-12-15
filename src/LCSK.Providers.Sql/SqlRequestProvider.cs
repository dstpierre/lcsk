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
			if (config["connectionStringName"] != null && !string.IsNullOrEmpty(config["connectionStringName"].ToString()) && !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString()))
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
	}
}