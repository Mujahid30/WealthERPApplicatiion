ALTER PROCEDURE [dbo].[SP_DeleteConditionalEvent]
@EventSetupID BIGINT


AS

BEGIN
	SET NOCOUNT ON
	
	Declare  
	@bTran AS INT,      
	@lErrCode AS INT,
	@EventID as SMALLINT,
	@CustomerID as INT,
	@SchemeID as INT
	
	If (@@Trancount = 0)  
	Begin  
		Set @bTran = 1  
		Begin Transaction  
	End 
		
		--Getting required details to delete from AlertDataConditionSetup
		SET @EventID=(Select AEL_EventID from dbo.AlertEventSetup WHERE
						AES_EventSetupID = @EventSetupID)
		SET @CustomerID= (Select AES_CreatedFor from dbo.AlertEventSetup WHERE
						AES_EventSetupID = @EventSetupID)
		SET @SchemeID= (Select AES_SchemeId from dbo.AlertEventSetup WHERE
						AES_EventSetupID = @EventSetupID)
		
		--print(@EventID)
		--print(@CustomerID)
		--print(@SchemeID)
		
		/*Delete Details from Data Conditions Table*/
		DELETE FROM
			dbo.AlertDataConditionSetup
		WHERE
			AEL_EventID = @EventID
			AND
			ADCS_UserID = @CustomerID
			AND
			ADCS_SchemeID = @SchemeID
		
		/*Delete Details from Event Setup Table*/
		DELETE FROM
			dbo.AlertEventSetup
		WHERE
			AES_EventSetupID = @EventSetupID
			
			

			
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
END

--exec SP_DeleteConditionalEvent 129 