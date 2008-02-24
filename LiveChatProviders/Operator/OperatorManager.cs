using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace LiveChat.Providers.Manager
{
    public class Operator
    {
        private static OperatorProvider defaultProvider;
        private static OperatorProviderCollection providers;

        static Operator()
        {
            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                OperatorServiceSection configuration =
                    (OperatorServiceSection)
                    ConfigurationManager.GetSection("OperatorProvider");

                if (configuration == null)
                    throw new ConfigurationErrorsException
                        ("OperatorProvider configuration section is not set correctly.");

                providers = new OperatorProviderCollection();

                ProvidersHelper.InstantiateProviders(configuration.Providers
                    , providers, typeof(OperatorProvider));

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

        public static OperatorProvider Provider
        {
            get
            {
                return defaultProvider;
            }
        }

        public static OperatorProviderCollection Providers
        {
            get
            {
                return providers;
            }
        }
    }
}
