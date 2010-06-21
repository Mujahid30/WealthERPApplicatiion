
ALTER PROCEDURE [dbo].[SP_GetPropertyNetPosition]
@CPNP_PropertyNPId INT

AS

BEGIN
	
	SELECT * FROM
		dbo.CustomerPropertyNetPosition
	WHERE
		CPNP_PropertyNPId = @CPNP_PropertyNPId
	
END 