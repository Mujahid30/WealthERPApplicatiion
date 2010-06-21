ALTER PROCEDURE [dbo].[SP_GetCustomerUlipPlanCode]
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
	
END 