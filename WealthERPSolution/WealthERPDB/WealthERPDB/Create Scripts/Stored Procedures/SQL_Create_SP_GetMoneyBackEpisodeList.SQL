/****** Object:  StoredProcedure [dbo].[SP_GetMoneyBackEpisodeList]    Script Date: 06/24/2009 12:03:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetMoneyBackEpisodeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetMoneyBackEpisodeList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMoneyBackEpisodeList]    Script Date: 06/24/2009 12:03:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetMoneyBackEpisodeList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetMoneyBackEpisodeList]
@CINP_InsuranceNPId INT

AS

BEGIN
	
	SELECT * FROM
		CustomerInsuranceMoneyBackEpisodes
	WHERE
		CINP_InsuranceNPId = @CINP_InsuranceNPId
	ORDER BY CIMBE_CreatedOn
	
END' 
END
GO
