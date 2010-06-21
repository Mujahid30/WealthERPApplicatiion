ALTER PROCEDURE SP_DeleteInsuranceNetPosition
@CINP_InsuranceNPId INT,
@CIA_AccountId INT

AS

BEGIN
	
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM dbo.CustomerInsuranceULIPPlan
				WHERE CINP_InsuranceNPId = @CINP_InsuranceNPId;
				
				DELETE FROM dbo.CustomerInsuranceMoneyBackEpisodes
				WHERE CINP_InsuranceNPId = @CINP_InsuranceNPId;
				
				DELETE FROM CustomerInsuranceNetPosition
				WHERE CINP_InsuranceNPId = @CINP_InsuranceNPId;
				
				DELETE FROM CustomerInsuranceAccountAssociates
				WHERE CIA_AccountId = @CIA_AccountId;
				
				DELETE FROM dbo.CustomerInsuranceAccount
				WHERE CIA_AccountId = @CIA_AccountId;

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