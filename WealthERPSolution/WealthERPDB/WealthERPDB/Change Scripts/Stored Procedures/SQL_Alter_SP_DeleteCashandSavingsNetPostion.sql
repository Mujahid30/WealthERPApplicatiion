ALTER PROCEDURE SP_DeleteCashandSavingsNetPostion
@CCSNP_CashSavingsNPId INT

AS

BEGIN
		
	DECLARE @ErrorMessage VARCHAR(1000);
	DECLARE @CCSA_AccountId INT;
	
	SELECT @CCSA_AccountId = CCSA_AccountId 
	FROM dbo.CustomerCashSavingsNetPosition
	WHERE CCSNP_CashSavingsNPId = @CCSNP_CashSavingsNPId;
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerCashSavingsNetPosition
				WHERE CCSNP_CashSavingsNPId = @CCSNP_CashSavingsNPId;
				
				DELETE FROM CustomerCashSavingsAccountAssociates
				WHERE CCSA_AccountId = @CCSA_AccountId;
				
				DELETE FROM dbo.CustomerCashSavingsAccount
				WHERE CCSA_AccountId = @CCSA_AccountId;

			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH	
		
END 