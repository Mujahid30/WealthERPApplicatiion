ALTER PROCEDURE SP_DeleteGovtSavingsNetPostion
@CGSNP_GovtSavingNPId INT,
@CGSA_AccountId INT

AS

BEGIN
		
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerGovtSavingNetPosition
				WHERE CGSNP_GovtSavingNPId = @CGSNP_GovtSavingNPId;
				
				DELETE FROM CustomerGovtSavingAccountAssociates
				WHERE CGSA_AccountId = @CGSA_AccountId;
				
				DELETE FROM dbo.CustomerGovtSavingAccount
				WHERE CGSA_AccountId = @CGSA_AccountId;

			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH	
		
END 