#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/17
 * Comment		: Memory Chat Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Memory Chat Provider
/// </summary>
public class SqlChatProvider : ChatProvider
{
    private string connectionString = string.Empty;

	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
	{
		// check to ensure config is not null
		if (config == null)
			throw new ArgumentNullException("config");

		if (string.IsNullOrEmpty(name))
			name = "SqlChatProvider";

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

	public override string RequestChat(ChatRequestInfo request)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_ChatRequestsAdd", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        string retVal = request.ChatId;

        try
        {
            cmd.Parameters.Add("@ChatID", SqlDbType.Char, 39).Value = request.ChatId;
            cmd.Parameters.Add("@VisitorIP", SqlDbType.VarChar, 50).Value = request.VisitorIP;
            cmd.Parameters.Add("@VisitorName", SqlDbType.VarChar, 100).Value = request.VisitorName;
            cmd.Parameters.Add("@VisitorEmail", SqlDbType.VarChar, 225).Value = request.VisitorEmail;
            cmd.Parameters.Add("@VisitorUserAgent", SqlDbType.VarChar, 125).Value = request.VisitorUserAgent;
            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = request.AcceptByOpereratorId;
            cmd.Parameters.Add("@RequestDate", SqlDbType.SmallDateTime).Value = request.RequestDate;

            sqlC.Open();
            retVal = cmd.ExecuteScalar().ToString();
            
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

	public override void AddChatMessage(ChatMessageInfo msg)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_ChatMessagesAdd", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.Add("@ChatID", SqlDbType.Char, 39).Value = msg.ChatId;
            cmd.Parameters.Add("@FromName", SqlDbType.VarChar, 100).Value = msg.Name;
            cmd.Parameters.Add("@Message", SqlDbType.VarChar, 3000).Value = msg.Message;
            cmd.Parameters.Add("@SentDate", SqlDbType.BigInt).Value = msg.SentDate;

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

	public override List<ChatMessageInfo> GetMessages(string chatId, long lastCheck)
    {
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_ChatMessagesGet", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader data = null;
        List<ChatMessageInfo> retList = new List<ChatMessageInfo>();

        try
        {
            cmd.Parameters.Add("@ChatID", SqlDbType.Char, 39).Value = chatId;
            cmd.Parameters.Add("@LastCheck", SqlDbType.BigInt).Value = lastCheck;

            sqlC.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
                retList.Add(new ChatMessageInfo(data));

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

	public override List<ChatRequestInfo> GetChatRequests(int operatorId)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_ChatRequestsGetFromVisitors", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader data = null;
        List<ChatRequestInfo> retList = new List<ChatRequestInfo>();

        try
        {
            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorId;

            sqlC.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
                retList.Add(new ChatRequestInfo(data));

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

	public override void RemoveChatRequest(ChatRequestInfo req)
	{
        SqlConnection sqlC = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("LiveChat_ChatRequestsDelete", sqlC);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.Add("@ChatID", SqlDbType.Char, 39).Value = req.ChatId;

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
}
