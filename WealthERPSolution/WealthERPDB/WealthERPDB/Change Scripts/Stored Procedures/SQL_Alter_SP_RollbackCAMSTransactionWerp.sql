ALTER PROCEDURE SP_RollbackCAMSTransactionWerp
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
	
	/* Step1: Delete Data from Customer MF Transaction Table */
	DELETE FROM dbo.CustomerMutualFundTransaction
	WHERE CMFA_AccountId IN (
								SELECT 
									CMFA.CMFA_AccountId
								FROM
									dbo.Customer AS C
									INNER JOIN
									dbo.CustomerPortfolio AS CP
									ON C.C_CustomerId = CP.C_CustomerId
									INNER JOIN
									dbo.CustomerMutualFundAccount AS CMFA
									ON CP.CP_PortfolioId = CMFA.CP_PortfolioId
								WHERE
									C.ADUL_ProcessId = @processId
							)
	
	
	/* Step2: Delete Data from Customer MF Transaction Staging & Input Table*/
	EXEC dbo.SP_RollbackCAMSTransactionStaging
	@processId;
	
	
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
 