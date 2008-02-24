using System;
using System.Configuration.Provider;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.Providers
{
    public abstract class RequestProvider : ProviderBase
    {
        public abstract void LogRequest(PageRequestEntity pageRequested);
        public abstract List<PageRequestEntity> GetRequests(DateTime since);
    }
}
