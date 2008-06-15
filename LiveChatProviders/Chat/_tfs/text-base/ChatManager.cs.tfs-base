using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace LiveChat.Providers.Manager
{
    public class Chat
    {
        private static ChatProvider defaultProvider;
        private static ChatProviderCollection providers;

        static Chat()
        {
            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                ChatServiceSection configuration =
                    (ChatServiceSection)
                    ConfigurationManager.GetSection("ChatProvider");

                if (configuration == null)
                    throw new ConfigurationErrorsException
                        ("ChatProvider configuration section is not set correctly.");

                providers = new ChatProviderCollection();

                ProvidersHelper.InstantiateProviders(configuration.Providers
                    , providers, typeof(ChatProvider));

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

        public static ChatProvider Provider
        {
            get
            {
                return defaultProvider;
            }
        }

        public static ChatProviderCollection Providers
        {
            get
            {
                return providers;
            }
        }
    }
}
