/****** Object:  StoredProcedure [dbo].[SP_UpdateCamsProfileStagingIsFolioNew]    Script Date: 06/24/2009 12:06:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCamsProfileStagingIsFolioNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCamsProfileStagingIsFolioNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCamsProfileStagingIsFolioNew]    Script Date: 06/24/2009 12:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCamsProfileStagingIsFolioNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UpdateCamsProfileStagingIsFolioNew]
@processId INT
AS
   update CustomerMFCAMSXtrnlProfileStaging 
     set CMGCXPS_IsFolioNew=0,CMFA_AccountId = c.CMFA_AccountId 
     from CustomerMFCAMSXtrnlProfileStaging s,CustomerMutualFundAccount c 
     where s.CMGCXPS_FOLIOCHK=c.CMFA_FolioNum and s.CMGCXPS_IsFolioNew=1
     AND s.ADUL_ProcessId = @processId
     
     

' 
END
GO
