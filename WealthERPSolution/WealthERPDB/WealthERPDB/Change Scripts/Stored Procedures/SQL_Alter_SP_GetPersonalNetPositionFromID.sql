
ALTER PROCEDURE [dbo].[SP_GetPersonalNetPositionFromID]
@CPNP_PersonalNPId INT

AS

BEGIN
	
	SELECT * FROM 
		dbo.CustomerPersonalNetPosition 
	WHERE 
		CPNP_PersonalNPId = @CPNP_PersonalNPId
	
END
 