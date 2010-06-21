ALTER PROCEDURE [dbo].[SP_DeleteMFTransactions]
@CMFT_MFTransId INT

AS

BEGIN
	
	DELETE FROM CustomerMutualFundTransaction
	WHERE 
		CMFT_MFTransId = @CMFT_MFTransId
		AND
		CMFT_IsSourceManual = 1
	
END 