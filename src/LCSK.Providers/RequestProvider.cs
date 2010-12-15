#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Request Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Collections.Generic;
using LCSK.Core;

namespace LCSK.Providers
{
	public abstract class RequestProvider : ProviderBase
	{
		public abstract bool LogRequest(WebRequest req);
		public abstract List<WebRequest> GetRequest(DateTime lastRequestDate);
	}
}