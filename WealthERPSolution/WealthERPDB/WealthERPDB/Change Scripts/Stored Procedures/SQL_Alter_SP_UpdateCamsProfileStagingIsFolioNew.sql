ALTER PROCEDURE [dbo].[SP_UpdateCamsProfileStagingIsFolioNew]
@processId INT
AS
   update CustomerMFCAMSXtrnlProfileStaging 
     set CMGCXPS_IsFolioNew=0,CMFA_AccountId = c.CMFA_AccountId 
     from CustomerMFCAMSXtrnlProfileStaging s,CustomerMutualFundAccount c 
     where s.CMGCXPS_FOLIOCHK=c.CMFA_FolioNum and s.CMGCXPS_IsFolioNew=1
     AND s.ADUL_ProcessId = @processId
     
     

 