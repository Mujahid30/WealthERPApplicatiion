ALTER PROCEDURE SP_DeletePensionGratuitiesNetPostion
@CPGNP_PensionGratutiesNPId INT,
@CPGA_AccountId INT

AS

BEGIN
		
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerPensionandGratuitiesNetPosition
				WHERE CPGNP_PensionGratutiesNPId = @CPGNP_PensionGratutiesNPId;
				
				DELETE FROM CustomerPensionandGrauitiesAccountAssociates
				WHERE CPGA_AccountId = @CPGA_AccountId;
				
				DELETE FROM dbo.CustomerPensionandGratuitiesAccount
				WHERE CPGA_AccountId = @CPGA_AccountId;

			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH	
		
END 