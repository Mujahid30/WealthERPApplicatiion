/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUlipPlanCode]    Script Date: 06/24/2009 12:01:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerUlipPlanCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerUlipPlanCode]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUlipPlanCode]    Script Date: 06/24/2009 12:01:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerUlipPlanCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetCustomerUlipPlanCode]
@CINP_InsuranceNPId INT

AS

BEGIN
	
	SELECT TOP(1)
		dbo.WerpULIPPlan.WUP_ULIPPlanCode 
	FROM
		dbo.WerpULIPPlan 
		INNER JOIN 
		dbo.WerpULIPSubPlan 
	ON 
	dbo.WerpULIPPlan.WUP_ULIPPlanCode = dbo.WerpULIPSubPlan.WUP_ULIPPlanCode
		INNER JOIN CustomerInsuranceULIPPlan
	ON 
	dbo.WerpULIPSubPlan.WUSP_ULIPSubPlanCode = dbo.CustomerInsuranceULIPPlan.WUSP_ULIPSubPlanCode
	WHERE 
		CustomerInsuranceULIPPlan.CINP_InsuranceNPId = @CINP_InsuranceNPId
	
END' 
END
GO
