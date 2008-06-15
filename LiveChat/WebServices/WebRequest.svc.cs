using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.WCF
{
    // NOTE: If you change the class name "WebRequest" here, you must also update the reference to "WebRequest" in Web.config.
    public class WebRequest : IWebRequest
    {

        #region IWebRequest Members

        public void LogRequest(PageRequestEntity page)
        {
            LiveChat.BusinessLogic.WebRequest.LogRequest(page);
        }

        public List<PageRequestEntity> GetRequests(DateTime since)
        {
            return LiveChat.BusinessLogic.WebRequest.GetRequests(since);
        }

        #endregion
    }
}
