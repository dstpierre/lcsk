using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCSK.Core
{
    public class DashboardViewModel
    {
        public List<WebRequest> Visitors { get; set; }
        public List<ChatRequest> PendingRequests { get; set; }
        public List<ChatRequest> CurrentChat { get; set; }
        public List<ChatRequest> PendingInvitations { get; set; }

        public DashboardViewModel()
        {
        }
    }
}
