/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomer]    Script Date: 06/24/2009 11:14:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteCustomer]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomer]    Script Date: 06/24/2009 11:14:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeleteCustomer
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
	
	
END' 
END
GO
