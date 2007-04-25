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
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Collections.Generic;


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

	public static List<RequestInfo> GetRequest(DateTime lastRequest)
	{
		// Load the provider
		LoadProvider();

		return _provider.GetRequest(lastRequest);
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
