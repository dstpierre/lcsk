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
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration.Provider;

/// <summary>
/// Manage requests
/// </summary>
public abstract class RequestProvider : ProviderBase
{
	public abstract bool LogRequest(RequestInfo req);
}
