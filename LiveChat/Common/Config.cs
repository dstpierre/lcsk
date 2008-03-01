using System;
using System.Configuration;

namespace LiveChat.WebSite
{
    public static class Config
    {
        public static string ServiceKey { get { return ConfigurationManager.AppSettings["ServiceKey"].ToString(); } }
    }
}
