#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Operator Provider Helper
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
/// Summary description for OperatorService
/// </summary>
public class OperatorService
{
	private static OperatorProvider _provider = null;
	private static object _lock = new object();

	public OperatorProvider Provider
	{
		get { return _provider; }
	}

	public static bool GetOperatorStatus()
	{
		// Load the provider
		LoadProvider();

		return _provider.GetOperatorStatus();
	}

	public static OperatorInfo LogIn(string userName, string password)
	{
		// Load the provider
		LoadProvider();

		return _provider.LogIn(userName, password);
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
					OperatorServiceSection section = (OperatorServiceSection)WebConfigurationManager.GetSection("system.web/operatorService");

					// Load the provider
					if (section.Providers.Count > 0)
						_provider = (OperatorProvider)ProvidersHelper.InstantiateProvider(section.Providers[0], typeof(OperatorProvider));

					if (_provider == null)
						throw new ProviderException("Unable to load the OperatorProvider");
				}
			}
		}
	}
}
