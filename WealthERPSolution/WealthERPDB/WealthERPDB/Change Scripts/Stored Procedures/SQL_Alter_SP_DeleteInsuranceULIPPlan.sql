
ALTER PROCEDURE [dbo].[SP_DeleteInsuranceULIPPlan]
@CINP_InsuranceNPId INT

AS

BEGIN
	DELETE FROM CustomerInsuranceULIPPlan
	WHERE CINP_InsuranceNPId = @CINP_InsuranceNPId
END 