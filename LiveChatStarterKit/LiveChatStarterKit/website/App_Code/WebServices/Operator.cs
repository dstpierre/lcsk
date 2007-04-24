#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Service called by the operator's console
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;


    /// <summary>
    /// Contains all functionality for an operator to maintain
    /// a chat session with a client.
    /// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Operator : System.Web.Services.WebService
{
	public Operator()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	
}