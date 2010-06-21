-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE SP_DeleteLOB  
  
@AL_LOBId INT  
  
AS  

SET NOCOUNT ON   

Declare  
  @bTran AS INT,      
  @lErrCode AS INT

-- Set Variables
SET @lErrCode  = 0

-- Begin Tran
If (@@Trancount = 0)  
 Begin  
  Set @bTran = 1  
  Begin Transaction  
 End   
 
DELETE FROM dbo.AdviserLOB WHERE AL_LOBId=@AL_LOBId  

If (@@Error <> 0)                    
	Begin                    
	  Set @lErrCode = 1005 -- This is an error code set by the application     
	  Goto Error                    
	End  

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


 