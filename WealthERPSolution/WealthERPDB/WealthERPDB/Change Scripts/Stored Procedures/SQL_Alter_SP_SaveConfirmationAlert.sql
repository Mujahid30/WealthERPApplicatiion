-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_SaveConfirmationAlert]
	@AEL_EventID INT,
	@AES_EventMessage VARCHAR(500),
	@AES_SchemeID INT,
	@AES_TargetID INT,
	@AES_CreatedFor INT,
	@AES_CreatedBy INT,
	@AES_ModifiedBy INT
AS
BEGIN

	SET NOCOUNT ON;
	
	Declare  
	@bTran AS INT,      
	@lErrCode AS INT

	If (@@Trancount = 0)  
	Begin  
		Set @bTran = 1  
		Begin Transaction  
	End 
	
    -- Insert statements for procedure here
	INSERT INTO 
		AlertEventSetup
			(
				AEL_EventId,
				AES_EventMessage,
				AES_SchemeID,
				AES_TargetID,
				AES_EventSubscriptionDate,
				AES_CreatedFor,
				AES_DeliveryMode,
				AES_SentToQueue,
				AES_CreatedBy,
				AES_ModifiedBy,
				AES_CreatedOn,
				AES_ModifiedOn
			)
	VALUES
			(
				@AEL_EventId,
				@AES_EventMessage,
				@AES_SchemeID,
				@AES_TargetID,
				CURRENT_TIMESTAMP,
				@AES_CreatedFor,
				'1,3',
				0,
				@AES_CreatedBy,
				@AES_ModifiedBy,
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP
			)
			
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
END
 