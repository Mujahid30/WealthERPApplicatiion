/****** Object:  StoredProcedure [dbo].[SP_CheckIsFolioNew]    Script Date: 06/24/2009 10:59:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CheckIsFolioNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CheckIsFolioNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckIsFolioNew]    Script Date: 06/24/2009 10:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CheckIsFolioNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_CheckIsFolioNew
@FolioNumber VARCHAR(50)

AS

SET NOCOUNT ON

	SELECT COUNT(*) AS CNT
	FROM CustomerMutualFundAccount
	WHERE CMFA_FolioNum = @FolioNumber


SET NOCOUNT OFF
' 
END
GO
