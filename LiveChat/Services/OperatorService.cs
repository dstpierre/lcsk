using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Collections.Generic;
using LiveChat.Entities;
using LiveChat.Providers;

namespace LiveChat.WebSite
{
    public class OperatorService
    {
        private static OperatorProvider _provider = LiveChat.Providers.Manager.Operator.Provider;
        //private static object _lock = new object();

        //public OperatorProvider Provider
        //{
        //    get { return _provider; }
        //}

        public static List<OperatorEntity> GetOnlineOperator()
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetOnlineOperator();
        }

        public static bool IsOperatorOnline()
        {
            // Load the provider
            //LoadProvider();

            return _provider.IsOperatorOnline();
        }

        public static int Create(string name, string password, string email, bool isAdmin)
        {
            // Load the provider
            //LoadProvider();

            return _provider.Create(name, password, email, isAdmin);
        }

        public static void UpdateStatus(int operatorId, bool isOnline)
        {
            // Load the provider
            //LoadProvider();

            _provider.UpdateStatus(operatorId, isOnline);
        }

        
        public static OperatorEntity LogIn(string name, string password)
        {
            // Load the provider
            //LoadProvider();

            return _provider.LogIn(name, password);
        }

        public static bool Remove(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.Remove(operatorId);
        }

        public static List<ChannelEntity> GetChatChannel(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetChatChannel(operatorId);
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
        //                OperatorServiceSection section = (OperatorServiceSection)WebConfigurationManager.GetSection("system.web/operatorService");

        //                // Load the default provider
        //                if (section.Providers.Count > 0 && !string.IsNullOrEmpty(section.DefaultProvider) && section.Providers[section.DefaultProvider] != null)
        //                    _provider = (OperatorProvider)ProvidersHelper.InstantiateProvider(section.Providers[section.DefaultProvider], typeof(OperatorProvider));

        //                if (_provider == null)
        //                    throw new ProviderException("Unable to load the OperatorProvider");
        //            }
        //        }
        //    }
        //}
    }
}