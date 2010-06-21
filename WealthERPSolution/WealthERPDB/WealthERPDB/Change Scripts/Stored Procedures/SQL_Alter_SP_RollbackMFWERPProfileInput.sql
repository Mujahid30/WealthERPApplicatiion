ALTER PROCEDURE SP_RollbackMFWERPProfileInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerMFXtrnlProfileInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 