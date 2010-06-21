ALTER PROCEDURE SP_RollbackEQOdineNSETransactionInput
@processId INT

AS

SET NOCOUNT ON

	DELETE FROM CustomerEquityOdinNSEXtrnlTransactionInput
	WHERE ADUL_ProcessId = @processId

SET NOCOUNT OFF
 