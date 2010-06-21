ALTER PROCEDURE SP_RollbackEQOdineBSETransactionInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM dbo.CustomerEquityOdinBSEXtrnlTransactionInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 