ALTER PROCEDURE SP_RollbackCAMSProfileInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerMFCAMSXtrnlProfileInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 