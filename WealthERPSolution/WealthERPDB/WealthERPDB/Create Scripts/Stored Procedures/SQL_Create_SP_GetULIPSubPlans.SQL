/****** Object:  StoredProcedure [dbo].[SP_GetULIPSubPlans]    Script Date: 06/24/2009 12:05:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULIPSubPlans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetULIPSubPlans]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetULIPSubPlans]    Script Date: 06/24/2009 12:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULIPSubPlans]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetULIPSubPlans]
@WUP_ULIPPlanCode INT
AS 
SELECT * FROM dbo.WerpULIPSubPlan WHERE WUP_ULIPPlanCode = @WUP_ULIPPlanCode' 
END
GO
