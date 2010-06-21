
ALTER PROCEDURE [dbo].[SP_GetPensionAndGratuities]
@CPGNP_PensionGratutiesNPId INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		CustomerPensionandGratuitiesNetPosition 
	WHERE 
		CPGNP_PensionGratutiesNPId = @CPGNP_PensionGratutiesNPId
END


 