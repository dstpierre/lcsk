using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Providers;
using LiveChat.DAL;
using LiveChat.Entities;

namespace LiveChat.SQLProvider
{
    public class SQLRequestProvider : RequestProvider
    {
        public override void LogRequest(LiveChat.Entities.PageRequestEntity pageRequested)
        {
            VisitorHistories.Create(pageRequested.VisitorIp, pageRequested.PageRequested, pageRequested.Referrer);
        }

        public override List<LiveChat.Entities.PageRequestEntity> GetRequests(DateTime since)
        {
            List<PageRequestEntity> newRequests = new List<PageRequestEntity>();
            PageRequestEntity req = null;
            foreach (var r in VisitorHistories.Fetch(since))
            {
                req = new PageRequestEntity();
                req.VisitorIp = r.VisitorIp;
                req.PageRequested = r.RequestedPage;
                req.Referrer = r.Referrer;
                req.UserAgent = r.UserAgent;
                req.RequestedDate = r.RequestedTime;
                newRequests.Add(req);
            }
            return newRequests;
        }
    }
}
