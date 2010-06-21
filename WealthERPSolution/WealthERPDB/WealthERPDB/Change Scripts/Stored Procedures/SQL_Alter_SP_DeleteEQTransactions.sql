ALTER PROCEDURE [dbo].[SP_DeleteEQTransactions]
@CET_EqTransId INT

AS

BEGIN
	
	DELETE FROM CustomerEquityTransaction
	WHERE 
		CET_EqTransId = @CET_EqTransId
		AND
		CET_IsSourceManual = 0
	
END 