/****** Object:  UserDefinedFunction [dbo].[Fn_GetBranchEQAssetAgr]    Script Date: 06/24/2009 15:29:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetBranchEQAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetBranchEQAssetAgr]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetBranchEQAssetAgr]    Script Date: 06/24/2009 15:29:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetBranchEQAssetAgr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[Fn_GetBranchEQAssetAgr]
(
	@AB_BranchId INT
)

RETURNS NUMERIC(18,4) 

AS

BEGIN
	
	DECLARE @Result NUMERIC(18,4);
	
	SELECT 
		@Result =ISNULL( SUM(CENP_CurrentValue),0)
	FROM 
		ViewEquityNP
	WHERE
		AR_RMId IN (
						SELECT AR_RMId FROM AdviserRMBranch WHERE AB_BranchId = @AB_BranchId
					)
	
	RETURN @Result;
	
END' 
END

GO
