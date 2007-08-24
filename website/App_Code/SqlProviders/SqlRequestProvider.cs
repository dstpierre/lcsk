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
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlRequestProvider
/// </summary>
public class SqlRequestProvider : RequestProvider
{
    private string connectionString = string.Empty;

	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "SqlRequestProvider";

        // Check for the connection string
        if( config["connectionStringName"] != null &&  !string.IsNullOrEmpty(config["connectionStringName"].ToString()) && !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString()) )
        {
            connectionString = ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString();
            config.Remove("connectionStringName");
        }
        else
            throw new ArgumentNullException("The ConnectionStringName is not defined");
		
		base.Initialize(name, config);
	}

	public override bool LogRequest(RequestInfo req)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_LogAccessAdd", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        bool retVal = false;

        try
        {
            cmd.Parameters.Add("@PageRequested", SqlDbType.VarChar, 500).Value = req.PageRequested;
            cmd.Parameters.Add("@DomainRequested", SqlDbType.VarChar, 250).Value = req.DomainRequested;
            cmd.Parameters.Add("@RequestedTime", SqlDbType.SmallDateTime).Value = req.RequestTime;
            cmd.Parameters.Add("@Referrer", SqlDbType.VarChar, 500).Value = req.Referrer;
            cmd.Parameters.Add("@VisitorUserAgent", SqlDbType.VarChar, 100).Value = req.VisitorUserAgent;
            cmd.Parameters.Add("@VisitorIP", SqlDbType.VarChar, 50).Value = req.VisitorIP;

            sqlC.Open();
            retVal = Convert.ToBoolean(cmd.ExecuteNonQuery());
            
            cmd.Dispose();
            sqlC.Close();
        }
        catch
        {
            throw;
        }
        finally
        {
            if (sqlC != null)
            {
                if (sqlC.State == ConnectionState.Open)
                    sqlC.Close();

                sqlC.Dispose();
                sqlC = null;
            }
        }
        return retVal;
	}


    public override List<RequestInfo> GetRequest(DateTime lastRequestDate)
    {
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_LogAccessGet", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader data = null;
        List<RequestInfo> retList = new List<RequestInfo>();

        try
        {
            cmd.Parameters.Add("@RequestedTime", SqlDbType.SmallDateTime).Value = lastRequestDate;

            sqlC.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
                retList.Add(new RequestInfo(data));

            data.Close();
            data.Dispose();

            cmd.Dispose();
            sqlC.Close();
        }
        catch
        {
            throw;
        }
        finally
        {
            if (sqlC != null)
            {
                if (sqlC.State == ConnectionState.Open)
                    sqlC.Close();

                sqlC.Dispose();
                sqlC = null;
            }
        }
        return retList;
    }
}
