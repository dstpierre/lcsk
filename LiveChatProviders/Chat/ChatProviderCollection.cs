using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace LiveChat.Providers
{
    public class ChatProviderCollection : ProviderCollection
    {
        // Return an instance of DataProvider
        // for a specified provider name
        new public ChatProvider this[string name]
        {
            get { return (ChatProvider)base[name]; }
        }
    }

}
