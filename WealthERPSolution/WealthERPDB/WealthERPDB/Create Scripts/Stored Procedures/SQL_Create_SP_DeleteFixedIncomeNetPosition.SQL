/****** Object:  StoredProcedure [dbo].[SP_DeleteFixedIncomeNetPosition]    Script Date: 06/24/2009 11:14:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteFixedIncomeNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteFixedIncomeNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteFixedIncomeNetPosition]    Script Date: 06/24/2009 11:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteFixedIncomeNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeleteFixedIncomeNetPosition
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
	
END' 
END
GO
