/****** Object:  StoredProcedure [dbo].[LiveChat_ChatRequestsAdd]    Script Date: 11/20/2009 06:06:51 ******/
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

	-- Get the less busy operator
	IF @OperatorID = -1 BEGIN

		SELECT TOP 1 @OperatorID = o.OperatorID
		FROM
			LiveChat_Operators o LEFT OUTER JOIN LiveChat_ChatRequests cr ON
			o.OperatorID = cr.OperatorID
		WHERE
			o.IsOnline = 1 AND
			cr.ClosedDate IS NULL AND
			o.Department LIKE '%' + @Department + '%'
		GROUP BY o.OperatorID
		ORDER BY COUNT(cr.ChatID), o.OperatorID
	
	END
	
	INSERT INTO LiveChat_ChatRequests (ChatID, VisitorIP, VisitorName, VisitorEmail, VisitorUserAgent, OperatorID, RequestDate, Department)
	VALUES (@ChatID, @VisitorIP, @VisitorName, @VisitorEmail, @VisitorUserAgent, @OperatorID, @RequestDate, @Department)

END ELSE BEGIN

	SET @ChatID = @ExistingRequest

END

SELECT @ChatID

GO