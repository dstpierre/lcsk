using System;
using System.Configuration;

namespace LiveChat.WebSite
{
    public static class Config
    {
        public static string ServiceKey { get { return ConfigurationManager.AppSettings["ServiceKey"].ToString(); } }
		public static string Email { get { return ConfigurationManager.AppSettings["Email"].ToString(); } }
		public static string SMTPServer { get { return ConfigurationManager.AppSettings["SMTPServer"].ToString(); } }
    }
}
