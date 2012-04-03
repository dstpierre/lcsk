using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSK
{
    public class ChatBoardViewModel
    {
        public List<RealTimeVisit> Visits { get; set; }
        public List<Chat> PendingChats { get; set; }
    }
}