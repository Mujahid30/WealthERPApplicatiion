/****** Object:  StoredProcedure [dbo].[SP_DeleteGovtSavingsNetPostion]    Script Date: 06/24/2009 11:14:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteGovtSavingsNetPostion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteGovtSavingsNetPostion]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteGovtSavingsNetPostion]    Script Date: 06/24/2009 11:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteGovtSavingsNetPostion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeleteGovtSavingsNetPostion
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
		
END' 
END
GO
