using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Collections.Generic;
using LiveChat.Entities;
using LiveChat.Providers.Manager;
using LiveChat.Providers;


namespace LiveChat.WebSite
{
    public class RequestService
    {
        private static RequestProvider _provider = Request.Provider;
        //private static object _lock = new object();

        //public RequestProvider Provider
        //{
        //    get { return _provider; }
        //}

        public static void LogRequest(PageRequestEntity req)
        {
            // Load the provider
            //LoadProvider();

            _provider.LogRequest(req);
        }

        public static List<PageRequestEntity> GetRequests(DateTime since)
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetRequests(since);
        }

        //private static void LoadProvider()
        //{
        //    // if we do not have initiated the provider
        //    if (_provider == null)
        //    {
        //        lock (_lock)
        //        {
        //            // Do this again to make sure _provider is still null
        //            if (_provider == null)
        //            {
        //                // Get a reference to the <requestService> section
        //                RequestServiceSection section = (RequestServiceSection)WebConfigurationManager.GetSection("system.web/requestService");

        //                // Load the default provider
        //                if (section.Providers.Count > 0 && !string.IsNullOrEmpty(section.DefaultProvider) && section.Providers[section.DefaultProvider] != null)
        //                    _provider = (RequestProvider)ProvidersHelper.InstantiateProvider(section.Providers[section.DefaultProvider], typeof(RequestProvider));

        //                if (_provider == null)
        //                    throw new ProviderException("Unable to load the RequestProvider");
        //            }
        //        }
        //    }
        //}
    }
}