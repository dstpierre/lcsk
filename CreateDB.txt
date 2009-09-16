/****** Object:  Table [dbo].[LiveChat_Channels]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiveChat_Channels](
	[ChannelId] [uniqueidentifier] NOT NULL,
	[RequestId] [int] NOT NULL,
	[OperatorId] [int] NOT NULL,
	[OpenDate] [smalldatetime] NOT NULL,
	[AcceptDate] [smalldatetime] NULL,
	[CloseDate] [smalldatetime] NULL,
 CONSTRAINT [PK_dbo.LiveChat_Channels] PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LiveChat_ChatInvitations]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LiveChat_ChatInvitations](
	[InvitationId] [int] IDENTITY(1,1) NOT NULL,
	[OperatorId] [int] NOT NULL,
	[VisitorIp] [varchar](11) NOT NULL,
	[Message] [nvarchar](350) NOT NULL,
	[WasAccept] [bit] NOT NULL,
	[RequestDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_ChatInvitations] PRIMARY KEY CLUSTERED 
(
	[InvitationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LiveChat_ChatRequests]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LiveChat_ChatRequests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[VisitorIp] [varchar](11) NOT NULL,
	[VisitorName] [nvarchar](100) NOT NULL,
	[VisitorEmail] [nvarchar](200) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SendCopyOfChat] [bit] NOT NULL,
	[RequestedDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_ChatRequests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LiveChat_DepartmentOperators]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiveChat_DepartmentOperators](
	[DepartmentId] [int] NOT NULL,
	[OperatorId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_DepartmentOperators] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[OperatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LiveChat_Departments]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiveChat_Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LiveChat_Messages]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiveChat_Messages](
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChannelId] [uniqueidentifier] NOT NULL,
	[FromName] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[SentDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LiveChat_Operators]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LiveChat_Operators](
	[OperatorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_Operators] PRIMARY KEY CLUSTERED 
(
	[OperatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LiveChat_VisitorHistories]    Script Date: 07/25/2009 06:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LiveChat_VisitorHistories](
	[HistoryId] [int] IDENTITY(1,1) NOT NULL,
	[VisitorIp] [varchar](11) NOT NULL,
	[RequestedPage] [varchar](300) NOT NULL,
	[RequestedTime] [smalldatetime] NOT NULL,
	[Referrer] [varchar](300) NOT NULL,
	[UserAgent] [varchar](85) NOT NULL,
 CONSTRAINT [PK_dbo.LiveChat_VisitorHistories] PRIMARY KEY CLUSTERED 
(
	[HistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[LiveChat_Channels]  WITH CHECK ADD  CONSTRAINT [ChatRequest_Channel] FOREIGN KEY([RequestId])
REFERENCES [dbo].[LiveChat_ChatRequests] ([RequestId])
GO
ALTER TABLE [dbo].[LiveChat_Channels] CHECK CONSTRAINT [ChatRequest_Channel]
GO
ALTER TABLE [dbo].[LiveChat_Channels]  WITH CHECK ADD  CONSTRAINT [Operator_Channel] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[LiveChat_Operators] ([OperatorId])
GO
ALTER TABLE [dbo].[LiveChat_Channels] CHECK CONSTRAINT [Operator_Channel]
GO
ALTER TABLE [dbo].[LiveChat_ChatInvitations]  WITH CHECK ADD  CONSTRAINT [Operator_ChatInvitation] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[LiveChat_Operators] ([OperatorId])
GO
ALTER TABLE [dbo].[LiveChat_ChatInvitations] CHECK CONSTRAINT [Operator_ChatInvitation]
GO
ALTER TABLE [dbo].[LiveChat_ChatRequests]  WITH CHECK ADD  CONSTRAINT [Department_ChatRequest] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[LiveChat_Departments] ([DepartmentId])
GO
ALTER TABLE [dbo].[LiveChat_ChatRequests] CHECK CONSTRAINT [Department_ChatRequest]
GO
ALTER TABLE [dbo].[LiveChat_DepartmentOperators]  WITH CHECK ADD  CONSTRAINT [Department_DepartmentOperator] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[LiveChat_Departments] ([DepartmentId])
GO
ALTER TABLE [dbo].[LiveChat_DepartmentOperators] CHECK CONSTRAINT [Department_DepartmentOperator]
GO
ALTER TABLE [dbo].[LiveChat_DepartmentOperators]  WITH CHECK ADD  CONSTRAINT [Operator_DepartmentOperator] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[LiveChat_Operators] ([OperatorId])
GO
ALTER TABLE [dbo].[LiveChat_DepartmentOperators] CHECK CONSTRAINT [Operator_DepartmentOperator]
GO
ALTER TABLE [dbo].[LiveChat_Messages]  WITH CHECK ADD  CONSTRAINT [Channel_ChatMessage] FOREIGN KEY([ChannelId])
REFERENCES [dbo].[LiveChat_Channels] ([ChannelId])
GO
ALTER TABLE [dbo].[LiveChat_Messages] CHECK CONSTRAINT [Channel_ChatMessage]