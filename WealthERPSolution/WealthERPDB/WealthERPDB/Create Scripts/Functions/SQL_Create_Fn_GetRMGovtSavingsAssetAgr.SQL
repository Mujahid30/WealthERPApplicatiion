/****** Object:  UserDefinedFunction [dbo].[Fn_GetRMGovtSavingsAssetAgr]    Script Date: 06/24/2009 15:29:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetRMGovtSavingsAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetRMGovtSavingsAssetAgr]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetRMGovtSavingsAssetAgr]    Script Date: 06/24/2009 15:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetRMGovtSavingsAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Fn_GetRMGovtSavingsAssetAgr]
(
	@AR_RMId INT
	
)
RETURNS NUMERIC(18,4)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result NUMERIC(18,4);
	

	-- Add the T-SQL statements to compute the return value here
	SELECT @result =ISNULL( SUM(CGSNP_CurrentValue),0) FROM dbo.ViewGovtSavingsNP
	WHERE AR_RMId = @AR_RMId
	-- Return the result of the function
	RETURN @result;


END

' 
END

GO
