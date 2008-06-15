using System;
using System.Configuration;

namespace LiveChat.Providers
{
    public class RequestServiceSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "MemoryRequestProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }

        [ConfigurationProperty("default", DefaultValue = "SqlProvider")]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }
            set
            {
                base["default"] = value;
            }
        }
    }
}