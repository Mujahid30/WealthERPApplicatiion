
ALTER PROCEDURE [dbo].[SP_UploadGetNewFolios]
@adviserId int
AS
	select CMFKXCS_FolioNumber,Max(CMFKXCS_CustomerId) CMFKXCS_CustomerId, Max(CP_PortfolioId) CP_PortfolioId  from CustomerMFKarvyXtrnlCombinationStaging where CMFKXCS_IsFolioNew=1 and CMFKXCS_IsRejected=0 and CMFKXCS_AdviserId = @adviserId group by CMFKXCS_FolioNumber

 