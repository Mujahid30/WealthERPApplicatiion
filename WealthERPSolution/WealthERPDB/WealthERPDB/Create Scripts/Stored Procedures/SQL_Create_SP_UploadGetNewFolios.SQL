/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewFolios]    Script Date: 06/24/2009 12:08:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewFolios]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetNewFolios]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewFolios]    Script Date: 06/24/2009 12:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewFolios]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UploadGetNewFolios]
@adviserId int
AS
	select CMFKXCS_FolioNumber,Max(CMFKXCS_CustomerId) CMFKXCS_CustomerId, Max(CP_PortfolioId) CP_PortfolioId  from CustomerMFKarvyXtrnlCombinationStaging where CMFKXCS_IsFolioNew=1 and CMFKXCS_IsRejected=0 and CMFKXCS_AdviserId = @adviserId group by CMFKXCS_FolioNumber

' 
END
GO
