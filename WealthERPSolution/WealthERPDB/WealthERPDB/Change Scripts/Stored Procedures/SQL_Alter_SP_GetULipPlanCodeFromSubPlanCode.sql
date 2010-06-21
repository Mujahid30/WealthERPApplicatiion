
ALTER PROCEDURE [dbo].[SP_GetULipPlanCodeFromSubPlanCode]
@WUSP_ULIPSubPlanCode INT

AS

BEGIN
	
	SELECT TOP 1 
		WUP_ULIPPlanCode
	FROM
		dbo.WerpULIPSubPlan
	WHERE
		WUSP_ULIPSubPlanCode = @WUSP_ULIPSubPlanCode
	
END 