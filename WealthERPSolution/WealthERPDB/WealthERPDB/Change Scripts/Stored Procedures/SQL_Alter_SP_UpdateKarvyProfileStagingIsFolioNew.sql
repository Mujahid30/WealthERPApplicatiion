ALTER PROCEDURE [dbo].[SP_UpdateKarvyProfileStagingIsFolioNew]
@processId INT
AS
   update CustomerMFKarvyXtrnlProfileStaging 
     set CMFKXPS_IsFolioNew=0,CMFA_AccountId = c.CMFA_AccountId 
     from CustomerMFKarvyXtrnlProfileStaging s,CustomerMutualFundAccount c 
     where s.CMFKXPS_Folio=c.CMFA_FolioNum and s.CMFKXPS_IsFolioNew=1
     AND s.ADUL_ProcessId = @processId


 