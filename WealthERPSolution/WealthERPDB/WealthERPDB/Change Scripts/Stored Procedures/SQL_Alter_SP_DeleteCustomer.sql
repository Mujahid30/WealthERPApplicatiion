ALTER PROCEDURE SP_DeleteCustomer
@C_CustomerId INT,
@U_UserId INT

AS

BEGIN 
	
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM dbo.Customer
				WHERE C_CustomerId = @C_CustomerId;
				
				DELETE FROM UserRoleAssociation
				WHERE U_UserId = @U_UserId;
				
				DELETE FROM dbo.[User]
				WHERE U_UserId = @U_UserId;
				
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