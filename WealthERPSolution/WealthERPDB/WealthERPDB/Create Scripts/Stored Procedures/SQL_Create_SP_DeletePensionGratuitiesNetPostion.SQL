/****** Object:  StoredProcedure [dbo].[SP_DeletePensionGratuitiesNetPostion]    Script Date: 06/24/2009 11:15:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeletePensionGratuitiesNetPostion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeletePensionGratuitiesNetPostion]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeletePensionGratuitiesNetPostion]    Script Date: 06/24/2009 11:15:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeletePensionGratuitiesNetPostion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeletePensionGratuitiesNetPostion
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
		
END' 
END
GO
