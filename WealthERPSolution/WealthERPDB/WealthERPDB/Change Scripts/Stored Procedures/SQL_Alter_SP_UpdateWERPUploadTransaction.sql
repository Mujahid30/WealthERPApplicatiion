
ALTER PROCEDURE [dbo].[SP_UpdateWERPUploadTransaction]
	-- Add the parameters for the stored procedure here
	@CMFXTS_Id int,
	@TransactionNumber Varchar(20) = NULL,
	@Folio Varchar(20) = NULL
AS
	SET NOCOUNT ON
	
	Declare  
	  @bTran AS INT,      
	  @lErrCode AS INT

	-- Begin Tran
	If (@@Trancount = 0)  
	 Begin  
	  Set @bTran = 1  
	  Begin Transaction  
	 End  
	 
	 
	 IF (@TransactionNumber IS NOT NULL AND @Folio IS NOT NULL)
	 BEGIN
	 	UPDATE dbo.CustomerMFXtrnlTransactionStaging 
		SET 
			CMFXTS_FolioNum = @Folio,
			CMFXTS_TransactionNumber = @TransactionNumber
		WHERE 
			CMFXTS_Id = @CMFXTS_Id
	 END
	 
	 ELSE IF (@TransactionNumber IS NOT NULL AND @Folio IS NULL)
	 BEGIN
	 	
	 	UPDATE dbo.CustomerMFXtrnlTransactionStaging 
		SET 
			CMFXTS_TransactionNumber = @TransactionNumber
		WHERE 
			CMFXTS_Id = @CMFXTS_Id
	 	
	 END
	
	 ELSE IF (@TransactionNumber IS NULL AND @Folio IS NOT NULL)
	 BEGIN
	 	
	 	UPDATE dbo.CustomerMFXtrnlTransactionStaging 
		SET 
			CMFXTS_FolioNum = @Folio
		WHERE 
			CMFXTS_Id = @CMFXTS_Id
	 	
	 END
	
	
		Success:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin                                    
  Commit Tran      
 End      
 Return 0      
      
 Goto Done      
      
Error:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin      
  Rollback Transaction      
 End      
 Return @lErrCode      

	       
Done:     
SET NOCOUNT OFF       


 