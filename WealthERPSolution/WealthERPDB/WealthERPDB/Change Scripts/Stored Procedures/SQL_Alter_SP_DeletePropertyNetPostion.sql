ALTER PROCEDURE [dbo].[SP_DeletePropertyNetPostion]
@CPNP_PropertyNPId INT,
@CPA_AccountId INT

AS

BEGIN
		
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerPropertyNetPosition
				WHERE CPNP_PropertyNPId = @CPNP_PropertyNPId;
				
				DELETE FROM CustomerPropertyAccountAssociates
				WHERE CPA_AccountId = @CPA_AccountId;
				
				DELETE FROM dbo.CustomerPropertyAccount
				WHERE CPA_AccountId = @CPA_AccountId;

			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH	
		
END 