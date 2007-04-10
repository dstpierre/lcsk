#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Request Provider Helper
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

/// <summary>
/// Summary description for RequestService
/// </summary>
public class RequestService
{
	private static RequestProvider _provider = null;
	private static object _lock = new object();

	public RequestProvider Provider
	{
		get { return _provider; }
	}

	public static bool LogRequest(RequestInfo req)
	{
		// Load the provider
		LoadProvider();

		return _provider.LogRequest(req);
	}

	private static void LoadProvider()
	{
		// if we do not have initiated the provider
		if (_provider == null)
		{
			lock (_lock)
			{
				// Do this again to make sure _provider is still null
				if (_provider == null)
				{
					// Get a reference to the <requestService> section
					RequestServiceSection section = (RequestServiceSection)WebConfigurationManager.GetSection("system.web/requestService");

					// Load the provider
					if (section.Providers.Count > 0)
						_provider = (RequestProvider)ProvidersHelper.InstantiateProvider(section.Providers[0], typeof(RequestProvider));

					if (_provider == null)
						throw new ProviderException("Unable to load the RequestProvider");
				}
			}
		}
	}
}
