ALTER PROCEDURE SP_DeleteFixedIncomeNetPosition
@CFINP_FINPId INT,
@CFIA_AccountId INT

AS

BEGIN
	
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerFixedIncomeNetPosition
				WHERE CFINP_FINPId = @CFINP_FINPId;
				
				DELETE FROM CustomerFixedIncomeAcccountAssociates
				WHERE CFIA_AccountId = @CFIA_AccountId;
				
				DELETE FROM dbo.CustomerFixedIncomeAccount
				WHERE CFIA_AccountId = @CFIA_AccountId;

			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					-- you can Raise ERROR with RAISEERROR() Statement including the details of the exception
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH
	
END 