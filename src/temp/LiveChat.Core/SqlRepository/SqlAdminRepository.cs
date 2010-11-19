using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace LiveChat.Core.SqlRepository
{
	//TODO: Add Interface
	public class SqlAdminRepository
	{
		public bool Install()
		{
			try
			{
				using (Data.LCSKDataContext db = new Data.LCSKDataContext(ConfigurationManager.ConnectionStrings["LCSK"].ToString()))
				{
					if (db.DatabaseExists())
					{
						//TODO: Fix this
						#region Sql Scripts - Most ugliest things I've done ;)
						string sql = @"
/****** Object:  Table [dbo].[LCSK_CannedMessages]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_CannedMessages](
	[OperatorId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Message] [varchar](1024) NOT NULL,
	[ShowPrompt] [bit] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LCSK_CannedMessages] PRIMARY KEY CLUSTERED 
(
	[OperatorId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_ChatMessages]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_ChatMessages](
	[Id] [uniqueidentifier] NOT NULL,
	[RequestId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Message] [varchar](1024) NOT NULL,
	[Posted] [datetime] NOT NULL,
 CONSTRAINT [PK_LCSK_ChatMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_ChatRequests]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LCSK_ChatRequests](
	[Id] [uniqueidentifier] NOT NULL,
	[VisitorId] [uniqueidentifier] NOT NULL,
	[OperatorId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[Requested] [smalldatetime] NOT NULL,
	[InitiatedFromVisitor] [bit] NOT NULL,
	[Accepted] [bit] NOT NULL,
	[Closed] [bit] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LCSK_ChatRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[LCSK_Departments]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_Departments](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LCSK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_Operators]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_Operators](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[DisplayName] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsManager] [bit] NOT NULL,
	[LastLogin] [smalldatetime] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_Operators_Departments]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LCSK_Operators_Departments](
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[OperatorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LCSK_Operators_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[OperatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[LCSK_QuickLinks]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_QuickLinks](
	[OperatorId] [uniqueidentifier] NOT NULL,
	[LinkName] [varchar](50) NOT NULL,
	[Link] [varchar](255) NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LCSK_QuickLinks] PRIMARY KEY CLUSTERED 
(
	[OperatorId] ASC,
	[LinkName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_Visitors]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_Visitors](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](125) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[TranscriptByEmail] [bit] NOT NULL,
	[idAddress] [varchar](50) NOT NULL,
	[Browser] [varchar](255) NOT NULL,
	[InChat] [bit] NOT NULL,
	[ProActiveRequest] [bit] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LCSK_Visitors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LCSK_WebRequests]    Script Date: 07/01/2010 07:09:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LCSK_WebRequests](
	[Id] [uniqueidentifier] NOT NULL,
	[VisitorId] [uniqueidentifier] NOT NULL,
	[RequestedDate] [smalldatetime] NOT NULL,
	[Page] [varchar](255) NOT NULL,
	[DomainName] [varchar](150) NOT NULL,
	[Referrer] [varchar](350) NOT NULL,
 CONSTRAINT [PK_WebRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LCSK_ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_LCSK_ChatMessages_LCSK_ChatRequests] FOREIGN KEY([Id])
REFERENCES [dbo].[LCSK_ChatRequests] ([Id])
GO

ALTER TABLE [dbo].[LCSK_ChatMessages] CHECK CONSTRAINT [FK_LCSK_ChatMessages_LCSK_ChatRequests]
GO

ALTER TABLE [dbo].[LCSK_ChatRequests]  WITH CHECK ADD  CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Departments] FOREIGN KEY([Id])
REFERENCES [dbo].[LCSK_Departments] ([Id])
GO

ALTER TABLE [dbo].[LCSK_ChatRequests] CHECK CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Departments]
GO

ALTER TABLE [dbo].[LCSK_ChatRequests]  WITH CHECK ADD  CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Operators] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[LCSK_Operators] ([Id])
GO

ALTER TABLE [dbo].[LCSK_ChatRequests] CHECK CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Operators]
GO

ALTER TABLE [dbo].[LCSK_ChatRequests]  WITH CHECK ADD  CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Visitors] FOREIGN KEY([Id])
REFERENCES [dbo].[LCSK_Visitors] ([Id])
GO

ALTER TABLE [dbo].[LCSK_ChatRequests] CHECK CONSTRAINT [FK_LCSK_ChatRequests_LCSK_Visitors]
GO

ALTER TABLE [dbo].[LCSK_WebRequests]  WITH CHECK ADD  CONSTRAINT [FK_LCSK_WebRequests_LCSK_Visitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[LCSK_Visitors] ([Id])
GO

ALTER TABLE [dbo].[LCSK_WebRequests] CHECK CONSTRAINT [FK_LCSK_WebRequests_LCSK_Visitors]
GO



";
						#endregion

						db.ExecuteCommand(sql.Replace("GO", ""));
					}
					else
						db.CreateDatabase();
				}
				return true;
			}
			catch (Exception ex)
			{
				//TODO: Create Genereric helper for executing queries and loging exception
				Debug.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
