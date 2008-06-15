using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace LiveChat.Providers
{
    public class RequestProviderCollection : ProviderCollection
    {
        // Return an instance of OperatorProvider
        // for a specified provider name
        new public RequestProvider this[string name]
        {
            get { return (RequestProvider)base[name]; }
        }
    }

}
