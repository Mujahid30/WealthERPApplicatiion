ALTER PROCEDURE SP_RollbackCAMSTransactionXtrnl
@processId INT

AS

SET NOCOUNT ON

DECLARE 
	@bTran AS INT,      
	@lErrCode AS INT

	-- Begin Tran
	If (@@Trancount = 0)  
	Begin  
		Set @bTran = 1  
		Begin Transaction  
	End   

	
	/* Step1: Delete Data from CAMS External Transaction Table*/
	DELETE FROM  dbo.CustomerMFCAMSXtrnlTransaction
	WHERE ADUL_ProcessId = @processId;
	
	/* Step 2: Delete Data from CAMS Transaction WERP, Staging, Input Tables*/
	EXEC dbo.SP_RollbackCAMSProfileWerp 
	@processId ;
	
	
	If (@@Error <> 0)                    
	Begin                    
		Set @lErrCode = 1001 -- This is an error code set by the application     
		Goto Error                    
	End  

	Success:      
		If (@bTran = 1 And @@Trancount > 0)      
		Begin                                    
			Commit Transaction      
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
 