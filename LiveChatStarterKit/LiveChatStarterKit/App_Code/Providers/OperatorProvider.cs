#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Operator Provider
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
using System.Configuration;
using System.Configuration.Provider;

/// <summary>
/// Manage all action regarding an operator
/// </summary>
public abstract class OperatorProvider : ProviderBase
{
	public abstract OperatorInfo LogIn(string userName, string password);
	public abstract void UpdateStatus(int operatorId, bool isOnline);
	public abstract bool GetOperatorStatus();
}
