#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Web.Config Operator Section
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
public class OperatorServiceSection : ConfigurationSection
{
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers
	{
		get { return (ProviderSettingsCollection)base["providers"]; }
	}

	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "MemoryOperatorProvider")]
	public string DefaultProvider
	{
		get { return (string)base["defaultProvider"]; }
		set { base["defaultProvider"] = value; }
	}
}
