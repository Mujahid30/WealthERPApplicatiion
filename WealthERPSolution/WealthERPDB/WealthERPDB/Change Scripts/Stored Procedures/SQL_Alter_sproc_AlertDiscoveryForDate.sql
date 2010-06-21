ALTER Procedure [dbo].[sproc_AlertDiscoveryForDate]
--@ID   nvarchar(20)
AS

Declare  
  @bTran AS INT,      
  @lErrCode AS INT, 
  @Id AS INT, -- this one is being used for the cursor 
  @cycleID AS TINYINT,    
  @nextOccurence AS DATETIME, -- this is the next occurence of the event
  /* The Following Variables are used for the sp call where insertions into the
     notification table is made */
  @aes_EventSetupId   BIGINT,
  @ael_EventId SMALLINT,
  @aen_EventMessage VARCHAR(500),
  @aen_SchemeId VARCHAR(50),
  @aen_TargetId VARCHAR(50),
  @aen_CreatedBy Varchar(50),
  @aen_CreatedFor Varchar(50),
  @modes VARCHAR(8)
  

/* Create Temp Table*/
      
-- Create Temp Tables
If (object_id(N'tempdb..[#DateEvents]')) Is Null    
  Create Table [#DateEvents]    
  (   
 [Id] [int] IDENTITY (1, 1) NOT NULL,
 [EL_AEL_EventID] [smallint] NOT NULL,
 [EL_AEL_EventCode] [varchar] (50) NOT NULL,
 [EL_AEL_EventType] [char] (20) NOT NULL,
 [EL_AEL_Reminder] [bit] NOT NULL,
 [EL_AEL_DefaultMessage] [varchar] (200) NULL,
 [EL_AEL_TriggerCondition] [varchar](50) NOT NULL,
 [EL_AEL_PrimarySubscriber] [varchar] (50) NOT NULL,
 [EL_CL_CycleID] [tinyint] NULL,
 [EL_AEL_IsAvailable] [bit] NOT NULL,
 [ES_AES_EventSetupID] [bigint] NOT NULL,
 [ES_AES_EventMessage] [varchar](500) NULL,
 [ES_AES_SchemeID] [varchar](50) NULL,
 [ES_AES_TargetId] [varchar](50) NOT NULL,
 [ES_AES_NextOccurence] [datetime] NULL,
 [ES_AES_LastOccurence] [datetime] NULL,
 [ES_AES_EndDate] [datetime] NULL,
 [ES_CL_CycleID] [tinyint] NULL,
 [ES_AES_CreatedFor] [varchar](50) NULL,
 [ES_AES_CreatedBy] [varchar](50) NULL,
 [ES_AES_DeliveryMode][varchar] (8) NOT NULL,
 [ES_AES_SentToQueue] [bit] NOT NULL,
 [FinalMessage] [varchar] (500) NULL,
 [Processed] [int] NOT NULL,
 [CreatedDate] [datetime] NULL,
 [ModifiedDate] [datetime] NULL,
 [NextCalculatedOccurene] [datetime] NULL
  )

/* Step 1 -  Insert Data into the Temp Table */
INSERT INTO [#DateEvents]([EL_AEL_EventID],[EL_AEL_EventCode],[EL_AEL_EventType] ,[EL_AEL_Reminder],
[EL_AEL_DefaultMessage],[EL_AEL_TriggerCondition] ,[EL_AEL_PrimarySubscriber],
[EL_CL_CycleID],[EL_AEL_IsAvailable],[ES_AES_EventSetupID],[ES_AES_EventMessage],
[ES_AES_SchemeID],[ES_AES_TargetId],[ES_AES_NextOccurence] ,[ES_AES_LastOccurence],
[ES_AES_EndDate],[ES_CL_CycleID],[ES_AES_CreatedFor],[ES_AES_CreatedBy],[ES_AES_DeliveryMode],[ES_AES_SentToQueue],[FinalMessage],
[Processed],[CreatedDate],[ModifiedDate],[NextCalculatedOccurene])

Select EL.AEL_EventID,EL.AEL_EventCode,EL.AEL_EventType,EL.AEL_Reminder,EL.AEL_DefaultMessage,
EL.AEL_TriggerCondition,EL.AEL_PrimarySubscriber,EL.CL_CycleId,EL.AEL_IsAvailable,
ES.AES_EventSetupID,ES.AES_EventMessage,ES.AES_SchemeID,ES.AES_TargetId,ES.AES_NextOccurence,
ES.AES_LastOccurence,ES.AES_EndDate,ES.CL_CycleID,ES.AES_CreatedFor,ES.AES_CreatedBy,ES.AES_DeliveryMode,ES.AES_SentToQueue,
CASE When (ES.[AES_EventMessage] IS NULL) THEN EL.[AEL_DefaultMessage] Else ES.[AES_EventMessage] END
,0,GETDATE(),NULL,NULL

From AlertEventSetup ES 

Inner Join AlertEventLookup EL  ON ES.AEL_EventId = EL.AEL_EventID

Where 

(
	(EL.AEL_EventType = 'Date') AND (EL.AEL_IsAvailable = 1) AND 
	(ES.AES_NextOccurence IS NOT NULL) AND (ES.AES_NextOccurence < GETDATE()) AND 
	((ES.AES_EndDate IS NULL) OR (ES.AES_EndDate > GETDATE())) AND
	 (ES.AES_SentToQueue = 0)
)


-- Start the Transaction                        
If (@@Trancount = 0)                      
Begin                      
 Set @bTran = 1                      
 Begin Transaction                      
End 

/* Step 2 - Do a sanity check on the data and delete all the dirty data - TBD */
--Select * From #DateEvents

/* Step 3 -  Handle Insertions in AlertEventNotification     */
-- Start a cursor  
Declare SL_Cursor CURSOR LOCAL FAST_FORWARD READ_ONLY  
For Select [Id]

/*,[ES_AES_NextOccurence],[ES_CL_CycleID],[ES_AES_EventSetupID],
[EL_AEL_EventID], [FinalMessage],[ES_AES_SchemeID],[ES_AES_TargetId],[ES_AES_DeliveryMode]*/

From #DateEvents  
  
-- Open the cursor  
Open SL_Cursor  
   
 -- Fetch  
 Fetch Next From SL_Cursor Into @Id 

/*, @nextOccurence, @cycleID,
 @aes_EventSetupId,@ael_EventId ,@aen_EventMessage,@aen_SchemeId */

 While @@FETCH_STATUS = 0  
 Begin 
     -- Initialize Variables. Not adding it as a part of the cursor for perf issues
     SET @nextOccurence = (Select [ES_AES_NextOccurence] From #DateEvents Where ID = @Id)
	 SET @cycleID = (Select [ES_CL_CycleID] From #DateEvents Where ID = @Id)
	 SET @aes_EventSetupId = (Select [ES_AES_EventSetupID] From #DateEvents Where ID = @Id)
	 SET @ael_EventId = (Select [EL_AEL_EventID] From #DateEvents Where ID = @Id)
	 SET @aen_EventMessage = (Select [FinalMessage] From #DateEvents Where ID = @Id)
	 SET @aen_SchemeId = (Select [ES_AES_SchemeID] From #DateEvents Where ID = @Id)
     SET @aen_CreatedFor  = (Select [ES_AES_CreatedFor] From #DateEvents Where ID = @Id)
     SET @modes  = (Select [ES_AES_DeliveryMode] From #DateEvents Where ID = @Id)
	 SET @aen_CreatedBy = (Select [ES_AES_CreatedBy] From #DateEvents Where ID = @Id)
	 -- 3(a)Call the Stored Procedure to make entries into the AlertEventNotification.
	 -- This stored procedure will take the basic columns and the mode as an input

	 
	 exec sproc_AlertInsertModeBasedNotification @aes_EventSetupId, @ael_EventId, @aen_EventMessage, @aen_SchemeId, @modes, @aen_CreatedBy, @aen_CreatedFor
	

	 -- 3(b) Calculate NextOccurence
	Update #DateEvents
	SET [NextCalculatedOccurene] = (Select dbo.fnGetNextOccurenceForCycle(@nextOccurence,@cycleID))
	Where ID = @Id

	 -- 3(c) Now Set the Processed = 1 for the particular row as it will tell which records were processed
	 Update #DateEvents
	 Set Processed = 1,ModifiedDate = GETDATE() 
	 Where ID = @Id
	 --- Fetch Next  
	  Fetch Next From SL_Cursor Into @Id --, @nextOccurence, @cycleID
 End  

 Close SL_Cursor  
 Deallocate SL_Cursor 
  /* 

 Step 4 -  After Insertion  Modify Table AlertEventSetup and SET AES_SentToQueue = 1
  Do this for records for which Processed = 1*/

Update AlertEventSetup
Set AES_SentToQueue = 1,AES_LastOccurence = DE.ModifiedDate
From [#DateEvents] DE
Inner Join AlertEventSetup ES ON DE.[ES_AES_EventSetupID] = ES.[AES_EventSetupID]
Where DE.Processed = 1



 --Step 5 -  Re-calculate the NextOccurence Depending on the Cycle Id. Also Set AES_SentToQueue = 0*/
Update AlertEventSetup
Set AES_SentToQueue = 0,AES_NextOccurence =  DE.[NextCalculatedOccurene]
From [#DateEvents] DE
Inner Join AlertEventSetup ES ON DE.[ES_AES_EventSetupID] = ES.[AES_EventSetupID]
Where DE.Processed = 1

/* Step - 6 - Check Data */
Select * From AlertEventSetup ES INNER JOIN  AlertEventLookUP EL ON ES.AEL_EventID = EL.AEL_EventID
Where AEL_EventType = 'Date'



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



 