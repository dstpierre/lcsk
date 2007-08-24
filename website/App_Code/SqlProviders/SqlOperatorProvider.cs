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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlOperatorProvider
/// </summary>
public class SqlOperatorProvider : OperatorProvider
{
    private string connectionString = string.Empty;


	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "MemoryOperatorProvider";

        // Check for the connection string
        if (config["connectionStringName"] != null && !string.IsNullOrEmpty(config["connectionStringName"].ToString()) && !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString()))
        {
            connectionString = ConfigurationManager.ConnectionStrings[config["connectionStringName"].ToString()].ToString();
            config.Remove("connectionStringName");
        }
        else
            throw new ArgumentNullException("The ConnectionStringName is not defined");

		base.Initialize(name, config);
	}
	public override OperatorInfo LogIn(string userName, string password)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_OperatorsGet", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader data = null;
        OperatorInfo retVal = null;

        try
        {
            cmd.Parameters.Add("@OperatorName", SqlDbType.VarChar, 100).Value = userName;
            cmd.Parameters.Add("@OperatorPassword", SqlDbType.VarChar, 50).Value = password;

            sqlC.Open();
            data = cmd.ExecuteReader();
            if (data.Read())
                retVal = new OperatorInfo(data);

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
        return retVal;
	}

	public override void UpdateStatus(int operatorId, bool isOnline)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_OperatorsUpdate", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorId;
            cmd.Parameters.Add("@IsOnline", SqlDbType.Bit).Value = isOnline;

            sqlC.Open();
            cmd.ExecuteNonQuery();

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
	}

	public override bool GetOperatorStatus()
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_OperatorsGetStatus", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        bool retVal = false;

        try
        {
            sqlC.Open();
            retVal = Convert.ToBoolean(cmd.ExecuteScalar().ToString());

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

	public override List<ChatRequestInfo> GetChatRequest(int operatorId)
	{
        return ChatService.GetRequests(operatorId);
	}

    public override List<OperatorInfo> GetOnlineOperator()
    {
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_OperatorsGetAllOnline", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader data = null;
        List<OperatorInfo> retList = new List<OperatorInfo>();

        try
        {
            sqlC.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
                retList.Add(new OperatorInfo(data));

            data.Close();
            data.Dispose();
            data = null;
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
