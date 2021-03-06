/****** Object:  UserDefinedFunction [dbo].[FN_CheckEventSubscription]    Script Date: 06/24/2009 15:29:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_CheckEventSubscription]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FN_CheckEventSubscription]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_CheckEventSubscription]    Script Date: 06/24/2009 15:29:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_CheckEventSubscription]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create FUNCTION [dbo].[FN_CheckEventSubscription](@EventID SMALLINT, @CustomerID INT)
RETURNS INT
AS

BEGIN
	
	DECLARE @CNT INT;
	DECLARE @RESULT INT;
	
	SELECT 
		@CNT = COUNT(*) 
	FROM
		dbo.AlertEventSetup
	WHERE
		AEL_EventID = @EventID
		AND
		AES_TargetID = @CustomerID
	
	IF (@CNT > 0)
	BEGIN
		SET @RESULT = 1;
	END
	ELSE
	BEGIN
		SET @RESULT = 0;
	END
	
	RETURN @RESULT;
	
END
' 
END

GO
