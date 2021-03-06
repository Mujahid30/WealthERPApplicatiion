/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingIsFolioNew]    Script Date: 06/24/2009 12:06:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingIsFolioNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCombinationStagingIsFolioNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingIsFolioNew]    Script Date: 06/24/2009 12:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingIsFolioNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UpdateCombinationStagingIsFolioNew]
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsFolioNew=0,CMFKXCS_AccountId = c.CMFA_AccountId 
     from CustomerMFKarvyXtrnlCombinationStaging s,CustomerMutualFundAccount c 
     where s.CMFKXCS_FolioNumber=c.CMFA_FolioNum and s.CMFKXCS_IsFolioNew=1
     
' 
END
GO
