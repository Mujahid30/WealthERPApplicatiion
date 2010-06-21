ALTER PROCEDURE SP_RollbackEQWERPProfileInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerEquityXtrnlProfileInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 