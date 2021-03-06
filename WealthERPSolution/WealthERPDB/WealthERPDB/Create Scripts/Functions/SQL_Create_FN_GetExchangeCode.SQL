/****** Object:  UserDefinedFunction [dbo].[FN_GetExchangeCode]    Script Date: 06/24/2009 15:29:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_GetExchangeCode]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FN_GetExchangeCode]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_GetExchangeCode]    Script Date: 06/24/2009 15:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_GetExchangeCode]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[FN_GetExchangeCode]
(
	@WerpCode INT,
	@Code VARCHAR(5)
)

RETURNS VARCHAR(255)

AS

BEGIN
	
	DECLARE @returnCode VARCHAR(255);
	
	SELECT @returnCode = B.PESM_Identifier
	FROM 
		ProductEquityMaster A
		INNER JOIN 
		ProductEquityScripMapping B 
	ON 
	B.PEM_ScripCode = A.PEM_ScripCode
	WHERE
		B.PESM_IdentifierName = @Code
		AND
		A.PEM_ScripCode = @WerpCode
		
	RETURN @returnCode;
	
	
END' 
END

GO
