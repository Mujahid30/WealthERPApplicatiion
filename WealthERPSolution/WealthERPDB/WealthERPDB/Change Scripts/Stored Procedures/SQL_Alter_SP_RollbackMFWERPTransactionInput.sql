ALTER PROCEDURE SP_RollbackMFWERPTransactionInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerMFXtrnlTransactionInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 