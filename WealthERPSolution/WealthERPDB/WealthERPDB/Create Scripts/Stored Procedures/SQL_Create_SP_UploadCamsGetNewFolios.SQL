/****** Object:  StoredProcedure [dbo].[SP_UploadCamsGetNewFolios]    Script Date: 06/24/2009 12:07:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadCamsGetNewFolios]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadCamsGetNewFolios]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadCamsGetNewFolios]    Script Date: 06/24/2009 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadCamsGetNewFolios]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UploadCamsGetNewFolios]
@processId int
AS
	select CMGCXPS_FOLIOCHK,Max(C_CustomerId) CMGCXPS_CustomerId,Max(CP_PortfolioId) CP_PortfolioId,MAX(PA_AMCCode) PA_AMCCode
	from CustomerMFCamsXtrnlProfileStaging
	where CMGCXPS_IsFolioNew=1 and CMGCXPS_IsRejected=0 AND CMGCXPS_IsAMCNew = 0 and ADUL_ProcessId = @processId  group by CMGCXPS_FOLIOCHK

' 
END
GO
