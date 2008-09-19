using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Providers;
using LiveChat.Entities;

namespace LiveChat.InMemoryProvider
{
    public class InMemoryRequestProvider : RequestProvider
    {
        public override void LogRequest(PageRequestEntity pageRequested)
        {
            throw new NotImplementedException();
        }

        public override List<PageRequestEntity> GetRequests(DateTime since)
        {
            throw new NotImplementedException();
        }
    }
}
