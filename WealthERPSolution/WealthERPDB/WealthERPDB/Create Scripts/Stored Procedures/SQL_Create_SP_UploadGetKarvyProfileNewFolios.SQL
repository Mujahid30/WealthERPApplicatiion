/****** Object:  StoredProcedure [dbo].[SP_UploadGetKarvyProfileNewFolios]    Script Date: 06/24/2009 12:07:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetKarvyProfileNewFolios]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetKarvyProfileNewFolios]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetKarvyProfileNewFolios]    Script Date: 06/24/2009 12:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetKarvyProfileNewFolios]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SP_UploadGetKarvyProfileNewFolios]
@adviserId int
AS
	select CMFKXPS_Folio,Max(C_CustomerId) C_CustomerId, Max(CP_PortfolioId) CP_PortfolioId  from CustomerMFKarvyXtrnlProfileStaging where CMFKXPS_IsFolioNew = 1 and CMFKXPS_IsRejected=0 and A_AdviserId = @adviserId group by CMFKXPS_Folio



' 
END
GO
