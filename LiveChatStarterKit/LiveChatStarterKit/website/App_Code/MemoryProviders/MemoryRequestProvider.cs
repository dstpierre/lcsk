#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Memory Request Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.Caching;

/// <summary>
/// Summary description for MemoryRequestProvider
/// </summary>
public class MemoryRequestProvider : RequestProvider
{

	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "MemoryRequestProvider";
		
		base.Initialize(name, config);
	}

	public override bool LogRequest(RequestInfo req)
	{
		// get the current in-memory list
		List<RequestInfo> requests;
		bool cacheExists = false;
		if (HttpContext.Current.Cache["_lcst_req"] != null)
		{
			requests = (List<RequestInfo>)HttpContext.Current.Cache["_lcst_req"];
			cacheExists = true;
		}
		else
		{
			requests = new List<RequestInfo>();
		}

		// Add the new requests
		requests.Add(req);

		if (cacheExists)
		{
			HttpContext.Current.Cache["_lcst_req"] = requests;
		}
		else
		{
			HttpContext.Current.Cache.Add("_lcst_req", requests, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);
		}

		return true;
	}
}
