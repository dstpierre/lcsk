using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;

namespace LCSK
{
    public class ChatRepository
    {
        private string ConnectionString = "";

        public ChatRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private SqlConnection connection;

        private bool OpenConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
                return true;

            connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //TODO: Add error handling here
                return false;
            }
        }

        private bool CloseConnection()
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
                connection = null;
            }
            return true;
        }

        private void LogException(Exception ex)
        {
            //TODO: Implement this.
        }

        public string LogVisit(string ip, string page, string referrer)
        {
            string retval = "offline";
            if (OpenConnection())
            {
                try
                {
                    var sql = @"
INSERT INTO [lcsk_RealTimeVisits]
           ([Id]
           ,[VisitorIp]
           ,[PageRequested]
           ,[Referrer]
           ,[RequestedOn]
           ,[Ping])
     VALUES
           (newid()
           ,@ip
           ,@page
           ,@referrer
           ,getdate()
           ,getdate())

UPDATE lcsk_Operators SET
    IsOnline = 0
WHERE DATEDIFF(second, Ping, GETDATE()) >= 30 AND IsOnline = 1
";
                    connection.Execute(sql, new { ip = ip, page = page, referrer = referrer });

                    using (var multi = connection.QueryMultiple(
                        @"SELECT COUNT(*) FROM lcsk_Operators WHERE IsOnline = 1
                        SELECT TOP 1 CONVERT(VARCHAR(50), Id) FROM lcsk_Chats WHERE VisitorIp = @ip AND Closed IS NULL", new { ip = ip }))
                    {
                        bool isOnline = multi.Read<int>().SingleOrDefault() > 0;

                        string chatId = multi.Read<string>().SingleOrDefault();

                        if (!string.IsNullOrEmpty(chatId))
                            retval = chatId;
                        else
                            retval = isOnline ? "online" : "offline";
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = "failed";
                }
                CloseConnection();
            }
            return retval;
        }

        public string VisitorPing(string ip, string page)
        {
            string retval = "offline";
            if (OpenConnection())
            {
                try
                {
                    connection.Execute(@"
UPDATE lcsk_RealTimeVisits SET Ping = GETDATE() WHERE VisitorIp = @ip AND PageRequested = @page

UPDATE lcsk_Operators SET
    IsOnline = 0
WHERE DATEDIFF(second, Ping, GETDATE()) >= 30 AND IsOnline = 1",
                        new { ip = ip, page = page });

                    using (var multi = connection.QueryMultiple(
                        @"SELECT COUNT(*) FROM lcsk_Operators WHERE IsOnline = 1
                        SELECT TOP 1 CONVERT(VARCHAR(50), Id) FROM lcsk_Chats WHERE VisitorIp = @ip AND Closed IS NULL", new { ip = ip }))
                    {
                        bool isOnline = multi.Read<int>().SingleOrDefault() > 0;

                        string chatId = multi.Read<string>().SingleOrDefault();

                        if (!string.IsNullOrEmpty(chatId) && isOnline)
                            retval = chatId;
                        else
                            retval = isOnline ? "online" : "offline";
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = "failed";
                }
                CloseConnection();
            }
            return retval;
        }

        public Guid RequestChat(string visitorIp, Guid opId)
        {
            Guid retval = Guid.NewGuid();
            if (OpenConnection())
            {
                try
                {
                    var sql = @"
INSERT INTO [lcsk_Chats]
           ([Id]
           ,[OperatorId]
           ,[VisitorIp]
           ,[Created]
           ,[Accepted]
           ,[Closed])
     VALUES
           (@id
           ,@opId
           ,@ip
           ,GETDATE()
           ,NULL
           ,NULL)

";
                    connection.Execute(sql, new { id = retval, opId = opId, ip = visitorIp });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = Guid.Empty;
                }
                CloseConnection();
            }
            return retval;
        }

        public bool AddMsg(Guid chatId, string from, string msg)
        {
            bool retval = false;
            if (OpenConnection())
            {
                try
                {
                    var sql = @"
INSERT INTO [lcsk_Messages]
           ([ChatId]
           ,[FromName]
           ,[Message]
           ,[Sent])
     VALUES
           (@id
           ,@from
           ,@msg
           ,GETDATE())
";
                    connection.Execute(sql, new { id = chatId, from = from, msg = msg });
                    retval = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = false;
                }
                CloseConnection();
            }
            return retval;
        }

        public List<ChatMessage> GetMsgs(Guid chatId, long lastId)
        {
            List<ChatMessage> retval = null;
            if (OpenConnection())
            {
                try
                {
                    retval = connection.Query<ChatMessage>("SELECT * FROM lcsk_Messages WHERE ChatId = @id AND Id > @lid",
                        new { id = chatId, lid = lastId }).ToList();
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = null;
                }
                CloseConnection();
            }
            return retval;
        }

        public ChatOperator GetOperator(string name, string pass)
        {
            ChatOperator retval = null;
            if (OpenConnection())
            {
                try
                {
                    retval = connection.Query<ChatOperator>(
                        "SELECT * FROM lcsk_Operators WHERE UserName = @n ANd Password = @p",
                        new { n = name, p = pass }).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = null;
                }
                CloseConnection();
            }
            return retval;
        }

        public bool ChangeStatus(Guid opId, bool isOnline)
        {
            bool retval = false;
            if (OpenConnection())
            {
                try
                {
                    connection.Execute("UPDATE lcsk_Operators SET IsOnline = @online WHERE Id = @id",
                        new { online = isOnline, id = opId });
                    retval = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = false;
                }
                CloseConnection();
            }
            return retval;
        }

        public ChatBoardViewModel GetVisitors(Guid opId)
        {
            ChatBoardViewModel retval = new ChatBoardViewModel();
            if (OpenConnection())
            {
                try
                {
                    connection.Execute("UPDATE lcsk_Operators SET Ping = GETDATE() WHERE Id = @id", new { id = opId });

                    var sql = @"
SELECT newid() AS Id,VisitorIp,PageRequested,
	(SELECT TOP 1 Referrer FROM lcsk_RealTimeVisits rt WHERE rt.VisitorIp = lcsk_RealTimeVisits.VisitorIp AND Referrer NOT LIKE '%bunkerapp.com%') As Referrer,
	MAX(RequestedOn) AS RequestedOn,MAX(Ping) AS Ping,
    (SELECT TOP 1 Id FROM lcsk_Chats c WHERE c.VisitorIp = lcsk_RealTimeVisits.VisitorIp AND Closed IS NULL) AS InChatId
FROM lcsk_RealTimeVisits 
GROUP BY VisitorIp,PageRequested
HAVING DATEDIFF(second, MAX(Ping), GETDATE()) < 15 
ORDER BY 4 DESC

SELECT * FROM lcsk_Chats
WHERE Closed IS NULL AND Accepted IS NULL
ORDER BY Created
";
                    using(var multi = connection.QueryMultiple(sql, new { opId = Guid.Empty }))
                    {
                        retval.Visits = multi.Read<RealTimeVisit>().ToList();
                        retval.PendingChats = multi.Read<Chat>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = null;
                }
                CloseConnection();
            }
            return retval;
        }

        public bool Accept(Guid id, Guid opId)
        {
            bool retval = false;
            if (OpenConnection())
            {
                try
                {
                    connection.Execute(@"
UPDATE lcsk_Chats SET OperatorId = @opId, Accepted = GETDATE() WHERE Id = @id

INSERT INTO [lcsk_Messages]
           ([ChatId]
           ,[FromName]
           ,[Message]
           ,[Sent])
     SELECT
           @id
           ,DisplayName
           ,'You are now chatting with ' + DisplayName
           ,GETDATE()
    FROM lcsk_Operators
    WHERE Id = @opId",
                       new { opId = opId, id = id });
                    retval = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
                CloseConnection();
            }
            return retval;
        }

        public bool Close(Guid id)
        {
            bool retval = false;
            if (OpenConnection())
            {
                try
                {
                    connection.Execute(@"
UPDATE lcsk_Chats SET Closed = GETDATE() WHERE Id = @id

INSERT INTO [lcsk_Messages]
           ([ChatId]
           ,[FromName]
           ,[Message]
           ,[Sent])
     VALUES
           (@id
           ,'system'
           ,'This chat session has been closed.'
           ,GETDATE())",
                       new { id = id });
                    retval = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
                CloseConnection();
            }
            return retval;
        }

        public bool DeleteUnclosedChat()
        {
            bool retval = false;
            if (OpenConnection())
            {
                try
                {
                    connection.Execute("update lcsk_Chats set closed = GETDATE() where Closed is null and Created <= GETDATE()-1");
                    retval = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    retval = false;
                }
                CloseConnection();
            }
            return retval;
        }
    }
}