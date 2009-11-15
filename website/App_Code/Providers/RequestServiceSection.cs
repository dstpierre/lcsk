#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Web.Config Request Section
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;

/// <summary>
/// Summary description for RequestServiceSection
/// </summary>
public class RequestServiceSection : ConfigurationSection
{
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers
	{
		get { return (ProviderSettingsCollection)base["providers"]; }
	}

	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider",DefaultValue = "MemoryRequestProvider")]
	public string DefaultProvider
	{
		get { return (string)base["defaultProvider"]; }
		set { base["defaultProvider"] = value; }
	}
}
