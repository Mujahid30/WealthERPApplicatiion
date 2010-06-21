ALTER PROCEDURE SP_RollbackKarvyProfileInput
@processId INT

AS

SET NOCOUNT ON 

	DELETE FROM dbo.CustomerMFKarvyXtrnlProfileInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 