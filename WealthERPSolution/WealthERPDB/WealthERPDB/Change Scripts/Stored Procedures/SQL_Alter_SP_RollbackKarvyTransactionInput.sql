ALTER PROCEDURE SP_RollbackKarvyTransactionInput
@processId INT

AS

SET NOCOUNT ON 

	DELETE FROM dbo.CustomerMFKarvyXtrnlTransactionInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 