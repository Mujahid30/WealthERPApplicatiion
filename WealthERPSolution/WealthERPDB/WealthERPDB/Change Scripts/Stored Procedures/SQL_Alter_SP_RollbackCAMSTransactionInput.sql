ALTER PROCEDURE SP_RollbackCAMSTransactionInput
@processId INT


AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerMFCAMSXtrnlTransactionInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 