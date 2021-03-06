/****** Object:  StoredProcedure [dbo].[SP_DeletePropertyNetPostion]    Script Date: 06/24/2009 11:15:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeletePropertyNetPostion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeletePropertyNetPostion]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeletePropertyNetPostion]    Script Date: 06/24/2009 11:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeletePropertyNetPostion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DeletePropertyNetPostion]
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
		
END' 
END
GO
