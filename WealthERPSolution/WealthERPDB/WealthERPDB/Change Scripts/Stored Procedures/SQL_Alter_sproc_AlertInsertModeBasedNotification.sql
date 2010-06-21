ALTER Procedure [dbo].[sproc_AlertInsertModeBasedNotification]  
@aes_EventSetupId   BIGINT,  
@ael_EventId SMALLINT,  
@aen_EventMessage VARCHAR(500),  
@aen_SchemeId VARCHAR(50),    
@modes VARCHAR(8),
@aen_CreatedBy Varchar(50),
@aen_CreatedFor Varchar(50)
  
AS  
  
Declare    
  @bTran AS INT,        
  @lErrCode AS INT,  
  @modeIndex AS INT  
  
-- Step 0  - Set Defaults  
  
SET @modeIndex = -1;-- negative value as zero can show a presence  
  
-- Step 1  - Check For Mode 1  
SET @modeIndex = (SELECT CHARINDEX('1',@modes))  

  
-- Start the Transaction                          
If (@@Trancount = 0)                        
Begin                        
 Set @bTran = 1                        
 Begin Transaction                        
End   
  
IF(@modeIndex >=0)  
BEGIN
INSERT INTO AlertEventNotification   
 (AES_EventSetupID,AEL_EventId,AEN_EventMessage,AEN_SchemeId,AEN_TargetId,ADML_ModeId,AEN_IsAlerted,AEN_PopulatedDate,AEN_CreatedBy )  
Values  
 (@aes_EventSetupId,@ael_EventId,@aen_EventMessage,@aen_SchemeId,@aen_CreatedFor,1,0,GETDATE(),@aen_CreatedBy)  
END  
  
-- Step 1  - Check For Mode 2
SET @modeIndex = (SELECT CHARINDEX('2',@modes))  
  
IF(@modeIndex >=0)  
BEGIN  
INSERT INTO AlertEventNotification   
 (AES_EventSetupID,AEL_EventId,AEN_EventMessage,AEN_SchemeId,AEN_TargetId,ADML_ModeId,AEN_IsAlerted,AEN_PopulatedDate,AEN_CreatedBy)  
Values  
 (@aes_EventSetupId,@ael_EventId,@aen_EventMessage,@aen_SchemeId,@aen_CreatedFor,2,0,GETDATE(),@aen_CreatedBy)  
END  
  
-- Step 1  - Check For Mode 1 3
SET @modeIndex = (SELECT CHARINDEX('3',@modes))  
  
IF(@modeIndex >=0)  
BEGIN  
INSERT INTO AlertEventNotification   
 (AES_EventSetupID,AEL_EventId,AEN_EventMessage,AEN_SchemeId,AEN_TargetId,ADML_ModeId,AEN_IsAlerted,AEN_PopulatedDate,AEN_CreatedBy)  
Values  
 (@aes_EventSetupId,@ael_EventId,@aen_EventMessage,@aen_SchemeId,@aen_CreatedFor,3,0,GETDATE(),@aen_CreatedBy)  
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
  
   