#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Visitors services called by the chat window
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;



    /// <summary>
    /// Contains all fonctionalities for the client chat session...
    /// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Visitors : System.Web.Services.WebService
{
	public Visitors()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	
}