ALTER PROCEDURE SP_GetCAMSRejectedProfile
@RejectedId INT

AS

SET NOCOUNT ON

	SELECT 
		* 
	FROM
		CustomerMFCAMSXtrnlProfileStaging
	WHERE
		CMGCXPS_Id = @RejectedId
	

SET NOCOUNT OFF
 