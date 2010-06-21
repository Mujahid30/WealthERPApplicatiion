ALTER PROCEDURE SP_GetKarvyRejectedProfile
@RejectedId INT

AS

SET NOCOUNT ON

		SELECT
			*
		FROM
			dbo.CustomerMFKarvyXtrnlProfileStaging
		WHERE
			CMFKXPS_Id = @RejectedId

SET NOCOUNT OFF
 