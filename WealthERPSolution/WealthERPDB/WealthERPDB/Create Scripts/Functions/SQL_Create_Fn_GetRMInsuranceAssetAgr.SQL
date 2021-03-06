/****** Object:  UserDefinedFunction [dbo].[Fn_GetRMInsuranceAssetAgr]    Script Date: 06/24/2009 15:29:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetRMInsuranceAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetRMInsuranceAssetAgr]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetRMInsuranceAssetAgr]    Script Date: 06/24/2009 15:29:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetRMInsuranceAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Fn_GetRMInsuranceAssetAgr] 
(
	-- Add the parameters for the function here
	@AR_RMId int
)
RETURNS NUMERIC(18,4)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result NUMERIC(18,4)

	-- Add the T-SQL statements to compute the return value here
	SELECT @result=ISNULL(SUM(CINP_PremiumAccumalated),0) FROM ViewInsuranceNP
WHERE AR_RMId=@AR_RMId
	-- Return the result of the function
	RETURN @result

END
' 
END

GO
