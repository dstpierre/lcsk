using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LiveChat.SQLProvider;
using System.Configuration;

namespace LiveChat.WebSite
{
	public partial class Install : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LiveChat.SQLProvider.Install o = new LiveChat.SQLProvider.Install();

				if (o.IsDatabaseExists(ConfigurationManager.ConnectionStrings["LiveChat"].ToString()))
				{
					litCreated.Text = "The database already exists";
					btnCreate.Enabled = false;
				}
			}
		}

		protected void CreateDB(object sender, EventArgs e)
		{
			LiveChat.SQLProvider.Install o = new LiveChat.SQLProvider.Install();

			o.CreateDB(ConfigurationManager.ConnectionStrings["LiveChat"].ToString());

			litCreated.Text = "The database has been created.";

			btnCreate.Enabled = false;
		}
	}
}
