/****** Object:  UserDefinedFunction [dbo].[Fn_GetCustCollectibleCurValAggr]    Script Date: 06/24/2009 15:29:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetCustCollectibleCurValAggr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetCustCollectibleCurValAggr]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetCustCollectibleCurValAggr]    Script Date: 06/24/2009 15:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetCustCollectibleCurValAggr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION Fn_GetCustCollectibleCurValAggr

(
	-- Add the parameters for the function here
	@C_CustomerId int
)
RETURNS NUMERIC(18,4)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result NUMERIC(18,4);

	-- Add the T-SQL statements to compute the return value here
	SELECT @result = SUM(CCNP_CurrentValue) FROM ViewCollectiblesNP
	WHERE C_CustomerId = @C_CustomerId
	-- Return the result of the function
	RETURN @result;

END
' 
END

GO
