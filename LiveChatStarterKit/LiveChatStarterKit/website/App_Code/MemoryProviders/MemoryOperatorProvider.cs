#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Operator Memory Provider
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

/// <summary>
/// Summary description for MemoryOperatorProvider
/// </summary>
public class MemoryOperatorProvider : OperatorProvider
{
	public override OperatorInfo LogIn(string userName, string password)
	{
		throw new Exception("The method or operation is not implemented.");
	}

	public override void UpdateStatus(int operatorId, bool isOnline)
	{
		throw new Exception("The method or operation is not implemented.");
	}

	public override bool GetOperatorStatus()
	{
		bool status = false;

		if (HttpContext.Current.Cache["_lcst_opstatus"] != null)
			status = (bool)HttpContext.Current.Cache["_lcst_opstatus"];

		return status;
	}
}
