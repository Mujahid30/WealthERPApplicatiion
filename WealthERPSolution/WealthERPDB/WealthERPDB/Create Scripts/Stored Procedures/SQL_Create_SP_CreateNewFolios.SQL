/****** Object:  StoredProcedure [dbo].[SP_CreateNewFolios]    Script Date: 06/24/2009 11:13:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateNewFolios]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateNewFolios]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateNewFolios]    Script Date: 06/24/2009 11:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateNewFolios]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SP_CreateNewFolios]
@portfolioId int,
@folioNum VARCHAR(50),
@userId int
AS
BEGIN
insert into CustomerMutualFundAccount(PAG_AssetGroupCode,CP_PortfolioId,CMFA_FolioNum,CMFA_CreatedOn,CMFA_CreatedBy,CMFA_ModifiedBy,CMFA_ModifiedOn) values(''MF'',@portfolioId,@folioNum,current_timestamp,@userId,@userId,current_timestamp)
	
END

' 
END
GO
