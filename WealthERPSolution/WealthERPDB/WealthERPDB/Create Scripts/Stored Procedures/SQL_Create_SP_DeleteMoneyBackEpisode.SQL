/****** Object:  StoredProcedure [dbo].[SP_DeleteMoneyBackEpisode]    Script Date: 06/24/2009 11:15:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMoneyBackEpisode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteMoneyBackEpisode]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteMoneyBackEpisode]    Script Date: 06/24/2009 11:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMoneyBackEpisode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_DeleteMoneyBackEpisode]
@CIMBE_EpisodeId INT

AS

BEGIN
	DELETE FROM CustomerInsuranceMoneyBackEpisodes
	WHERE CIMBE_EpisodeId = @CIMBE_EpisodeId
END' 
END
GO
