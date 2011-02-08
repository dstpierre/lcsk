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
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LCSK.Core;

namespace LCSK.Providers.Sql
{
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

		public override Operator LogIn(string userName, string password)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var entity = db.LiveChat_Operators.SingleOrDefault(x =>
					x.OperatorName == userName && x.OperatorPassword == password);

				if (entity == null)
					return null;

				Operator op = new Operator();
				op.Department = entity.Department;
				op.Email = entity.OperatorEmail;
				op.IsOnline = entity.IsOnline;
				op.OperatorId = entity.OperatorID;
				op.OperatorName = entity.OperatorName;
				op.Password = entity.OperatorPassword;

				return op;
			}
		}

		public override void UpdateStatus(int operatorId, bool isOnline)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var op = db.LiveChat_Operators.SingleOrDefault(x => x.OperatorID == operatorId);
				if (op != null)
				{
					op.IsOnline = isOnline;
					db.SubmitChanges();
				}
			}
		}

		public override bool GetOperatorStatus()
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				return db.LiveChat_Operators.Count(x => x.IsOnline) > 0;
			}
		}

		public override List<ChatRequest> GetChatRequest(int operatorId, string[] departments)
		{
			SqlChatProvider chat = new SqlChatProvider();
			chat.connectionString = connectionString;

			return chat.GetChatRequests(operatorId, departments);
		}

		public override List<Operator> GetOnlineOperator()
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var ops = db.LiveChat_Operators.Where(x => x.IsOnline).ToList();

				List<Operator> results = new List<Operator>();
				foreach (var op in ops)
				{
					results.Add(new Operator(op.OperatorID, op.OperatorName, op.OperatorPassword, op.OperatorEmail, op.IsOnline, op.Department));
				}

				return results;
			}
		}

		public override bool CreateDatabase(string password)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				//TODO: Refactor this
				db.ExecuteCommand(@"

CREATE TABLE [dbo].[LiveChat_ChatMessages](
	[MessageID] [bigint] IDENTITY(1,1) NOT NULL,
	[ChatID] [uniqueidentifier] NOT NULL,
	[FromName] [varchar](100) NOT NULL CONSTRAINT [DF_ASPLiveSupport_ChatMessages_FromName]  DEFAULT (''),
	[Message] [varchar](3000) NOT NULL CONSTRAINT [DF_ASPLiveSupport_ChatMessages_Message]  DEFAULT (''),
	[SentDate] [bigint] NOT NULL,
 CONSTRAINT [PK_ASPLiveSupport_ChatMessages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LiveChat_ChatRequests](
	[ChatID] [uniqueidentifier] NOT NULL,
	[VisitorIP] [varchar](50) NOT NULL CONSTRAINT [DF_ASPLiveSupport_ChatRequests_VisitorIP]  DEFAULT (''),
	[VisitorName] [varchar](100) NOT NULL CONSTRAINT [DF_LiveChat_ChatRequests_VisitorName]  DEFAULT (''),
	[VisitorEmail] [varchar](225) NOT NULL CONSTRAINT [DF_LiveChat_ChatRequests_VisitorEmail]  DEFAULT (''),
	[VisitorUserAgent] [varchar](125) NOT NULL CONSTRAINT [DF_LiveChat_ChatRequests_VisitorUserAgent]  DEFAULT (''),
	[OperatorID] [int] NOT NULL CONSTRAINT [DF_LiveChat_ChatRequests_OperatorID]  DEFAULT ((-1)),
	[Department] varchar(100) NULL,
	[RequestDate] [smalldatetime] NOT NULL CONSTRAINT [DF_ASPLiveSupport_ChatRequests_RequestDate]  DEFAULT (getdate()),
	[AcceptDate] [smalldatetime] NULL,
	[ClosedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_ASPLiveSupport_ChatRequests] PRIMARY KEY CLUSTERED 
(
	[ChatID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LiveChat_LogAccess](
	[LogAccessID] [int] IDENTITY(1,1) NOT NULL,
	[PageRequested] [varchar](500) NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_PageRequested]  DEFAULT (''),
	[DomainRequested] [varchar](250) NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_DomainRequested]  DEFAULT (''),
	[RequestedTime] [smalldatetime] NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_RequestedTime]  DEFAULT (getdate()),
	[Referrer] [varchar](500) NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_Referrer]  DEFAULT (''),
	[VisitorUserAgent] [varchar](100) NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_VisitorUserAgent]  DEFAULT (''),
	[VisitorIP] [varchar](50) NOT NULL CONSTRAINT [DF_ASPLiveSupport_LogAccess_VisitorIP]  DEFAULT (''),
 CONSTRAINT [PK_ASPLiveSupport_LogAccess] PRIMARY KEY CLUSTERED 
(
	[LogAccessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LiveChat_Operators](
	[OperatorID] [int] IDENTITY(1,1) NOT NULL,
	[OperatorName] [varchar](100) NOT NULL CONSTRAINT [DF_ASPLiveSupport_Operators_OperatorName]  DEFAULT (''),
	[OperatorPassword] [varchar](50) NOT NULL CONSTRAINT [DF_ASPLiveSupport_Operators_OperatorPassword]  DEFAULT ('nopw'),
	[OperatorEmail] [varchar](250) NOT NULL CONSTRAINT [DF_ASPLiveSupport_Operators_OperatorEmail]  DEFAULT (''),
	[IsOnline] [bit] NOT NULL CONSTRAINT [DF_ASPLiveSupport_Operators_OperatorStatus]  DEFAULT ((0)),
	Department varchar(500) NOT NULL CONSTRAINT [DF_LiveChat_Operators_Department] DEFAULT ('support'),
 CONSTRAINT [PK_ASPLiveSupport_Operators] PRIMARY KEY CLUSTERED 
(
	[OperatorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

");
				LiveChat_Operator admin = new LiveChat_Operator();
				admin.Department = "";
				admin.IsOnline = false;
				admin.OperatorEmail = "admin@mail.com";
				admin.OperatorName = "admin";
				admin.OperatorPassword = password;

				db.LiveChat_Operators.InsertOnSubmit(admin);
				db.SubmitChanges();
				
				return true;
			}
		}

		public override List<Operator> List()
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var ops = db.LiveChat_Operators.ToList();

				List<Operator> results = new List<Operator>();
				foreach (var op in ops)
				{
					results.Add(new Operator(op.OperatorID, op.OperatorName, op.OperatorPassword, op.OperatorEmail, op.IsOnline, op.Department));
				}

				return results;
			}
		}

		public override bool Save(Operator op)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				var entity = db.LiveChat_Operators.SingleOrDefault(x => x.OperatorID == op.OperatorId);
				if (entity == null)
				{
					entity = new LiveChat_Operator();

					db.LiveChat_Operators.InsertOnSubmit(entity);
				}
				
				entity.Department = op.Department;
				entity.OperatorEmail = op.Email;
				entity.OperatorName = op.OperatorName;
				entity.OperatorPassword = op.Password.Length > 0 ? entity.OperatorPassword : op.Password;

				db.SubmitChanges();
				return true;
			}
		}

		public override bool Delete(Operator op)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				db.LiveChat_Operators.DeleteAllOnSubmit(db.LiveChat_Operators.Where(x => x.OperatorID == op.OperatorId));
				db.SubmitChanges();
				return true;
			}
		}

		public override ChatRequest InviteVisitor(int operatorId, string visitorIp, string prompt)
		{
			using (LCSKDbDataContext db = new LCSKDbDataContext(connectionString))
			{
				LiveChat_ChatRequest invite = new LiveChat_ChatRequest();
				invite.AcceptDate = null;
				invite.ChatID = Guid.NewGuid();
				invite.ClosedDate = null;
				//HACK: this will serve as an indicator when visitor display chat button
				invite.Department = "op-invite";
				invite.OperatorID = operatorId;
				invite.RequestDate = DateTime.Now;
				invite.VisitorEmail = "";
				invite.VisitorIP = visitorIp;
				invite.VisitorName = "Me";
				invite.VisitorUserAgent = "";

				db.LiveChat_ChatRequests.InsertOnSubmit(invite);
				try
				{
					db.SubmitChanges();

					ChatRequest req = new ChatRequest();
					req.Accepted = null;
					req.ChatId = invite.ChatID;
					req.Closed = null;
					req.Department = invite.Department;
					req.OperatorId = invite.OperatorID;
					req.Requested = invite.RequestDate;
					req.VisitorEmail = invite.VisitorEmail;
					req.VisitorIp = invite.VisitorIP;
					req.VisitorName = invite.VisitorName;
					req.VisitorUserAgent = invite.VisitorUserAgent;
					req.WasAccepted = false;

					return req;
				}
				catch
				{
					throw;
				}
			}
		}
	}
}