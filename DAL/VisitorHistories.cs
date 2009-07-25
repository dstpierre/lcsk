using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class VisitorHistories : Manager
    {

        public static IList<VisitorHistory> Fetch(DateTime since)
        {
            return ExecuteQuery((dc) =>
            {
                var current = from v in dc.VisitorHistories
                              orderby v.RequestedTime descending
                              where v.RequestedTime >= since
                              group v by v.VisitorIp into g
                              select new VisitorHistory()
                              {
                                  HistoryId = g.FirstOrDefault().HistoryId,
                                  VisitorIp = g.FirstOrDefault().VisitorIp,
                                  RequestedPage = g.FirstOrDefault().RequestedPage,
                                  RequestedTime = g.FirstOrDefault().RequestedTime,
                                  Referrer = dc.VisitorHistories.FirstOrDefault(vh =>
                                      vh.VisitorIp == g.Key &&
                                      vh.Referrer.Substring(0, 12) != vh.RequestedPage.Substring(0, 15)).Referrer
                              };
                return current.ToList();
            }, "Unable to fetch visitors histories.");
        }

        public static int PageView(DateTime since)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.VisitorHistories.Count(vh => vh.RequestedTime.ToString("yyyy/MM/dd") == since.ToString("yyyy/MM/dd"));
                }, "Unable to get the count for visitor histories.");
        }

        public static bool Create(string visitorIp, string pageRequested, string referrer, string userAgent)
        {
            return ExecuteNonQuery((dc) =>
                {
                    VisitorHistory vh = new VisitorHistory();
                    vh.VisitorIp = visitorIp;
                    vh.RequestedPage = pageRequested;
                    vh.RequestedTime = DateTime.Now;
                    vh.Referrer = referrer;
					vh.UserAgent = userAgent.Length > 85 ? userAgent.Substring(0, 85) : userAgent;
                    dc.VisitorHistories.InsertOnSubmit(vh);
                    dc.SubmitChanges();
                    return true;
                }, "Unable to add the visitor history.");
        }
    }
}
