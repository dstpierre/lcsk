/****** Object:  Table [dbo].[lcsk_Chats]    Script Date: 06/03/2012 07:29:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[lcsk_Chats](
	[Id] [uniqueidentifier] NOT NULL,
	[OperatorId] [uniqueidentifier] NOT NULL,
	[VisitorIp] [varchar](25) NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Accepted] [smalldatetime] NULL,
	[Closed] [smalldatetime] NULL,
	[VisitorId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_lcsk_Chats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[lcsk_Messages]    Script Date: 06/03/2012 07:29:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[lcsk_Messages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[FromName] [varchar](100) NOT NULL,
	[Message] [varchar](3500) NOT NULL,
	[Sent] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_lcsk_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[lcsk_Operators]    Script Date: 06/03/2012 07:29:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[lcsk_Operators](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[DisplayName] [varchar](100) NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[Ping] [datetime] NOT NULL,
 CONSTRAINT [PK_lcsk_Operators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[lcsk_RealTimeVisits]    Script Date: 06/03/2012 07:29:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[lcsk_RealTimeVisits](
	[Id] [uniqueidentifier] NOT NULL,
	[VisitorIp] [varchar](25) NOT NULL,
	[PageRequested] [varchar](255) NOT NULL,
	[Referrer] [varchar](255) NOT NULL,
	[RequestedOn] [smalldatetime] NOT NULL,
	[Ping] [datetime] NOT NULL,
	[CountryCode] [varchar](5) NULL,
	[CountryName] [nvarchar](150) NULL,
	[LocationName] [nvarchar](200) NULL,
	[VisitorId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_lcsk_RealTimeVisits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

