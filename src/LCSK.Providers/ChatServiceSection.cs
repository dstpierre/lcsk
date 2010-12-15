#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Web.config Chat Section
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;

namespace LCSK.Providers
{
	public class ChatServiceSection : ConfigurationSection
	{
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get { return (ProviderSettingsCollection)base["providers"]; }
		}

		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "MemoryChatProvider")]
		public string DefaultProvider
		{
			get { return (string)base["defaultProvider"]; }
			set { base["defaultProvider"] = value; }
		}
	}
}