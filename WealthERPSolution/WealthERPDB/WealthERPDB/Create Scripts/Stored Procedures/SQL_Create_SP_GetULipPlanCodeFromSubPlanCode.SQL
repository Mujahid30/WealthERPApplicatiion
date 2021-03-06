/****** Object:  StoredProcedure [dbo].[SP_GetULipPlanCodeFromSubPlanCode]    Script Date: 06/24/2009 12:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULipPlanCodeFromSubPlanCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetULipPlanCodeFromSubPlanCode]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetULipPlanCodeFromSubPlanCode]    Script Date: 06/24/2009 12:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULipPlanCodeFromSubPlanCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetULipPlanCodeFromSubPlanCode]
@WUSP_ULIPSubPlanCode INT

AS

BEGIN
	
	SELECT TOP 1 
		WUP_ULIPPlanCode
	FROM
		dbo.WerpULIPSubPlan
	WHERE
		WUSP_ULIPSubPlanCode = @WUSP_ULIPSubPlanCode
	
END' 
END
GO
