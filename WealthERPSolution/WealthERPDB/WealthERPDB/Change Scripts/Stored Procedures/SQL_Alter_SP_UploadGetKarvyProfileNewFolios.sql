


ALTER PROCEDURE [dbo].[SP_UploadGetKarvyProfileNewFolios]
@processId int
AS
	select CMFKXPS_Folio,Max(C_CustomerId) C_CustomerId, Max(CP_PortfolioId) CP_PortfolioId,MAX(PA_AMCCode) PA_AMCCode  
	from CustomerMFKarvyXtrnlProfileStaging where CMFKXPS_IsFolioNew = 1 and CMFKXPS_IsRejected=0 AND CMFKXPS_IsAMCNew = 0
	and ADUL_ProcessId = @processId group by CMFKXPS_Folio



 