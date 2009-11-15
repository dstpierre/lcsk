/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.LiveChat_Operators ADD
	Department varchar(500) NOT NULL CONSTRAINT DF_LiveChat_Operators_Department DEFAULT 'support'
GO
ALTER TABLE dbo.LiveChat_Operators SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

/****** Object:  StoredProcedure [dbo].[LiveChat_OperatorsGet]    Script Date: 11/15/2009 06:28:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LiveChat_OperatorsGet]
	@OperatorName varchar(100),
	@OperatorPassword varchar(50)
AS

UPDATE LiveChat_Operators SET
	IsOnline = 1
WHERE
	(OperatorName = @OperatorName) AND
	(OperatorPassword = @OperatorPassword)

SELECT OperatorID, OperatorName, OperatorPassword, OperatorEmail, IsOnline, Department
FROM LiveChat_Operators
WHERE
	(OperatorName = @OperatorName) AND
	(OperatorPassword = @OperatorPassword)


GO

/****** Object:  StoredProcedure [dbo].[LiveChat_OperatorsGetAllOnline]    Script Date: 11/15/2009 06:31:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[LiveChat_OperatorsGetAllOnline]

AS
SELECT OperatorID, OperatorName, OperatorPassword, OperatorEmail, IsOnline, Department
FROM LiveChat_Operators
WHERE
	(IsOnline = 1)

GO
/****** Object:  StoredProcedure [dbo].[LiveChat_OperatorsGetByID]    Script Date: 11/15/2009 06:32:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[LiveChat_OperatorsGetByID]
	@OperatorID	int
AS
SELECT OperatorID, OperatorName, OperatorPassword, OperatorEmail, IsOnline, Department
FROM LiveChat_Operators
WHERE
	(OperatorID = @OperatorID)


GO

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.LiveChat_ChatRequests ADD
	Department varchar(100) NULL
GO
ALTER TABLE dbo.LiveChat_ChatRequests SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

/****** Object:  StoredProcedure [dbo].[LiveChat_ChatRequestsAdd]    Script Date: 11/15/2009 07:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LiveChat_ChatRequestsAdd]
	@ChatID	char(39),
	@VisitorIP varchar(50),
	@VisitorName varchar(100),
	@VisitorEmail varchar(225),
	@VisitorUserAgent varchar(125),
	@OperatorID int,
	@RequestDate smalldatetime,
	@Department varchar(100)
AS

DECLARE
	@ExistingRequest char(39)

SELECT 
	@ExistingRequest = ChatID
FROM 
	LiveChat_ChatRequests 
WHERE 
	VisitorIP = @VisitorIP AND 
	OperatorID = -1

IF @ExistingRequest IS NULL  BEGIN

	INSERT INTO LiveChat_ChatRequests (ChatID, VisitorIP, VisitorName, VisitorEmail, VisitorUserAgent, OperatorID, RequestDate, Department)
	VALUES (@ChatID, @VisitorIP, @VisitorName, @VisitorEmail, @VisitorUserAgent, @OperatorID, @RequestDate, @Department)

END ELSE BEGIN

	SET @ChatID = @ExistingRequest

END

SELECT @ChatID

GO

/****** Object:  StoredProcedure [dbo].[LiveChat_ChatRequestsGetByChatID]    Script Date: 11/15/2009 07:10:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[LiveChat_ChatRequestsGetByChatID]
	@ChatID char(39)
AS
SELECT ChatID, VisitorIP, VisitorName, VisitorEmail, VisitorUserAgent, OperatorID, RequestDate, AcceptDate, ClosedDate, Department
FROM LiveChat_ChatRequests
WHERE
	ChatID = @ChatID

GO

/****** Object:  StoredProcedure [dbo].[LiveChat_ChatRequestsGetFromVisitors]    Script Date: 11/15/2009 07:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LiveChat_ChatRequestsGetFromVisitors]
	@OperatorID	int
AS
SELECT ChatID, VisitorIP, VisitorName, VisitorEmail, VisitorUserAgent, OperatorID, RequestDate, AcceptDate, ClosedDate, Department
 FROM LiveChat_ChatRequests
WHERE
	(OperatorID = -1 OR OperatorID = @OperatorID) AND
	(AcceptDate IS NULL)

GO