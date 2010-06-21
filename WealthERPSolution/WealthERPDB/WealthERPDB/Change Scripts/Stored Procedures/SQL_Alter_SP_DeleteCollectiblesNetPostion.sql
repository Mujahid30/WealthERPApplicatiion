ALTER PROCEDURE SP_DeleteCollectiblesNetPostion
@CCNP_CollectibleNPId INT

AS

BEGIN
		
	DECLARE @ErrorMessage VARCHAR(1000);
	
	BEGIN TRY
		
		    BEGIN TRANSACTION -- Start the transaction..

				DELETE FROM CustomerCollectibleNetPosition
				WHERE CCNP_CollectibleNPId = @CCNP_CollectibleNPId;
				
			COMMIT TRAN -- Transaction Success!
		
	END TRY
	
	BEGIN CATCH
		        IF @@TRANCOUNT > 0
					ROLLBACK TRAN --RollBack in case of Error
					
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
	END CATCH	
		
END 