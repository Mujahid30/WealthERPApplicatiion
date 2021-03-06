/****** Object:  UserDefinedFunction [dbo].[Fn_GetAdvisorBranch]    Script Date: 06/24/2009 15:29:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetAdvisorBranch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetAdvisorBranch]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetAdvisorBranch]    Script Date: 06/24/2009 15:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetAdvisorBranch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION Fn_GetAdvisorBranch
(
	@A_AdviserId INT
)
RETURNS NUMERIC(18,4)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result NUMERIC(18,4)

	-- Add the T-SQL statements to compute the return value here
	SELECT @result=AB_BranchId FROM dbo.AdviserBranch WHERE A_AdviserId=@A_AdviserId

	-- Return the result of the function
	RETURN @result

END
' 
END

GO
