using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace LiveChat.Providers.Manager
{
    public class Request
    {
        private static RequestProvider defaultProvider;
        private static RequestProviderCollection providers;

        static Request()
        {
            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                RequestServiceSection configuration =
                    (RequestServiceSection)
                    ConfigurationManager.GetSection("RequestProvider");

                if (configuration == null)
                    throw new ConfigurationErrorsException
                        ("RequestProvider configuration section is not set correctly.");

                providers = new RequestProviderCollection();

                ProvidersHelper.InstantiateProviders(configuration.Providers
                    , providers, typeof(RequestProvider));

                providers.SetReadOnly();

                defaultProvider = providers[configuration.Default];

                if (defaultProvider == null)
                    throw new Exception("defaultProvider");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RequestProvider Provider
        {
            get
            {
                return defaultProvider;
            }
        }

        public static RequestProviderCollection Providers
        {
            get
            {
                return providers;
            }
        }
    }
}
