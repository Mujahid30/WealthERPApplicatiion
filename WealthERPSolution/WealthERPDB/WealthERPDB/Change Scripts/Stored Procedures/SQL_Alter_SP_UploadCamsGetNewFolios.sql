ALTER PROCEDURE [dbo].[SP_UploadCamsGetNewFolios]
@processId int
AS
	select CMGCXPS_FOLIOCHK,Max(C_CustomerId) C_CustomerId,Max(CP_PortfolioId) CP_PortfolioId,MAX(PA_AMCCode) PA_AMCCode
	from CustomerMFCamsXtrnlProfileStaging
	where CMGCXPS_IsFolioNew=1 and CMGCXPS_IsRejected=0 AND CMGCXPS_IsAMCNew = 0 and ADUL_ProcessId = @processId  group by CMGCXPS_FOLIOCHK

 