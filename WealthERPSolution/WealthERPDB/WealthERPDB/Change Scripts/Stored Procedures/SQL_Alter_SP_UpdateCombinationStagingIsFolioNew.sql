ALTER PROCEDURE [dbo].[SP_UpdateCombinationStagingIsFolioNew]
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsFolioNew=0,CMFKXCS_AccountId = c.CMFA_AccountId 
     from CustomerMFKarvyXtrnlCombinationStaging s,CustomerMutualFundAccount c 
     where s.CMFKXCS_FolioNumber=c.CMFA_FolioNum and s.CMFKXCS_IsFolioNew=1
     
 