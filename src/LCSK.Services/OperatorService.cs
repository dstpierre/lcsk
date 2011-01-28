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
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using LCSK.Core;
using LCSK.Providers;
using System;

namespace LCSK.Services
{
	public class OperatorService
	{
		private static OperatorProvider _provider = null;
		private static object _lock = new object();

		public OperatorProvider Provider
		{
			get { return _provider; }
		}

		public static void UpdateStatus(int operatorId, bool isOnline)
		{
			// Load the provider
			LoadProvider();

			_provider.UpdateStatus(operatorId, isOnline);
		}

		public static bool GetOperatorStatus()
		{
			try
			{
				// Load the provider
				LoadProvider();

				return _provider.GetOperatorStatus();
			}
			catch
			{
				// probably not installed and demo page did start
				return false;
			}
		}

		public static Operator LogIn(string userName, string password)
		{
			// Load the provider
			LoadProvider();

			return _provider.LogIn(userName, password);
		}

		public static List<ChatRequest> GetChatRequests(int operatorId, string[] departments)
		{
			// Load the provider
			LoadProvider();

			return _provider.GetChatRequest(operatorId, departments);
		}

		public static List<Operator> GetOnlineOperator()
		{
			// Load the provider
			LoadProvider();

			return _provider.GetOnlineOperator();
		}

		public static bool CreateDatabase(string password)
		{
			// Load the provider
			LoadProvider();

			//TODO: not seriously clear password?
			return _provider.CreateDatabase(password);
		}

		public static List<Operator> List()
		{
			// Load the provider
			LoadProvider();

			return _provider.List();
		}

		public static bool Save(Operator op)
		{
			// Load the provider
			LoadProvider();

			return _provider.Save(op);
		}

		public static bool Delete(Operator op)
		{
			// Load the provider
			LoadProvider();

			return _provider.Delete(op);
		}

		public static ChatRequest InviteVisitor(int operatorId, string visitorIp, string prompt)
		{
			// Load the provider
			LoadProvider();

			return _provider.InviteVisitor(operatorId, visitorIp, prompt);
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
						OperatorServiceSection section = (OperatorServiceSection)WebConfigurationManager.GetSection("LCSK/operatorService");

						// Load the default provider
						if (section.Providers.Count > 0 && !string.IsNullOrEmpty(section.DefaultProvider) && section.Providers[section.DefaultProvider] != null)
							_provider = (OperatorProvider)ProvidersHelper.InstantiateProvider(section.Providers[section.DefaultProvider], typeof(OperatorProvider));

						if (_provider == null)
							throw new ProviderException("Unable to load the OperatorProvider");
					}
				}
			}
		}
	}
}