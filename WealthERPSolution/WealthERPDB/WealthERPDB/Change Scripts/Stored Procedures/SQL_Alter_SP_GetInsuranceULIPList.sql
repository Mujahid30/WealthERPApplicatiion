
ALTER PROCEDURE [dbo].[SP_GetInsuranceULIPList]
@CINP_InsuranceNPId INT
AS
SELECT 
	* 
FROM 
	CustomerInsurabceULIPPlan 
WHERE 
	CINP_InsuranceNPId = @CINP_InsuranceNPId
 