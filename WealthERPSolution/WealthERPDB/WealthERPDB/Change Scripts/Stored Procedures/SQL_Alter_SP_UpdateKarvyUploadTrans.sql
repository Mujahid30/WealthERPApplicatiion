






ALTER PROCEDURE [dbo].[SP_UpdateKarvyUploadTrans]
	-- Add the parameters for the stored procedure here
	@CIMFKXTS_Id int,
	@TransactionNumber Varchar(20),
	@Folio Varchar(20)
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
	 
	Update dbo.dbo.CustomerMFKarvyXtrnlTransactionStaging set
	CIMFKXTS_FolioNumber = @Folio,
	CIMFKXTS_TransactionNumber = @TransactionNumber
	where CIMFKXTS_Id = @CIMFKXTS_Id
	
	
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


 