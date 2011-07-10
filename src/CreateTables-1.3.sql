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

insert into LiveChat_Operators
values('admin', 'admin', 'admin@yourdomain.com', 0, '')