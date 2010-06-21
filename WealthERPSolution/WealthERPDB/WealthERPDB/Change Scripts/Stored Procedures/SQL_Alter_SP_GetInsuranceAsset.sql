
ALTER PROCEDURE [dbo].[SP_GetInsuranceAsset]
@CINP_InsuranceNPId INT

AS

BEGIN
	
	SELECT 
		* 
	FROM 
		CustomerInsuranceNetPosition
	WHERE 
		CINP_InsuranceNPId = @CINP_InsuranceNPId
		
END 