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
using System.Configuration;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Summary description for MemoryOperatorProvider
/// </summary>
public class MemoryOperatorProvider : OperatorProvider
{
	private static string opName;
	private static string opPassword;
	private static string opEmail;


	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "MemoryOperatorProvider";

		// Get the operator name
		if (!string.IsNullOrEmpty(config["opName"]))
			opName = config["opName"].ToString();

		config.Remove("opName");

		// Get the operator password
		if (!string.IsNullOrEmpty(config["opPassword"]))
			opPassword = config["opPassword"].ToString();
		
		config.Remove("opPassword");

		// Get the operator email
		if (!string.IsNullOrEmpty(config["opEmail"]))
			opEmail = config["opEmail"].ToString();

		config.Remove("opEmail");

		base.Initialize(name, config);
	}
	public override OperatorInfo LogIn(string userName, string password)
	{
		OperatorInfo op = new OperatorInfo(1, opName, opPassword, opEmail, false);

		// Try to match the password
		if (password == opPassword)
		{
			op.IsOnline = true;
			UpdateStatus(op.OperatorId, true);
			return op;
		}
		else
			return null;
	}

	public override void UpdateStatus(int operatorId, bool isOnline)
	{
		if (HttpContext.Current.Cache["_lcst_opstatus"] != null)
			HttpContext.Current.Cache["_lcst_opstatus"] = isOnline;
		else
			HttpContext.Current.Cache.Add("_lcst_opstatus", isOnline, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5), CacheItemPriority.Normal, null);
	}

	public override bool GetOperatorStatus()
	{
		bool status = false;

		if (HttpContext.Current.Cache["_lcst_opstatus"] != null)
			status = (bool)HttpContext.Current.Cache["_lcst_opstatus"];

		return status;
	}
}
