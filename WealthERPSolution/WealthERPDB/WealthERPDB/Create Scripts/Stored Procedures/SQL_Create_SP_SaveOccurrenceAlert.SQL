/****** Object:  StoredProcedure [dbo].[SP_SaveOccurrenceAlert]    Script Date: 06/24/2009 12:05:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveOccurrenceAlert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_SaveOccurrenceAlert]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveOccurrenceAlert]    Script Date: 06/24/2009 12:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveOccurrenceAlert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaveOccurrenceAlert] 
	@AEL_EventID INT,
	@AES_EventMessage VARCHAR(500),
	@AES_SchemeID INT,
	@AES_TargetID INT,
	@AES_CreatedFor INT,
	@AES_CreatedBy INT,
	@AES_ModifiedBy INT,
	@ADCS_Condition VARCHAR(2),
    @ADCS_PresetValue NUMERIC(18,3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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
				AES_NextOccurence,
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
				CURRENT_TIMESTAMP,
				@AES_CreatedFor,
				''1,3'',
				0,
				@AES_CreatedBy,
				@AES_ModifiedBy,
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP
			)
			
	INSERT INTO
		AlertDataConditionSetup
			(
				ADCS_UserID,
				ADCS_SchemeID,
				AEL_EventID,
				ADCS_Condition,
				ADCS_PresetValue
			)
	VALUES
			(
				@AES_CreatedFor,
				@AES_SchemeID,
				@AEL_EventId,
				@ADCS_Condition,
				@ADCS_PresetValue
			)
	
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
' 
END
GO
