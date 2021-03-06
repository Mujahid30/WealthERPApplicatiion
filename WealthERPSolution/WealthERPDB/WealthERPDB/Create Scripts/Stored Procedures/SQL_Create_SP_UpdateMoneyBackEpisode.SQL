/****** Object:  StoredProcedure [dbo].[SP_UpdateMoneyBackEpisode]    Script Date: 06/24/2009 12:07:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateMoneyBackEpisode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateMoneyBackEpisode]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMoneyBackEpisode]    Script Date: 06/24/2009 12:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateMoneyBackEpisode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UpdateMoneyBackEpisode]

@CIMBE_RepaymentDate DATETIME,
@CIMBE_RepaidPer numeric(5, 2),
@CIMBE_EpisodeId INT,
@CIMBE_ModifiedBy INT

AS

UPDATE CustomerInsuranceMoneyBackEpisodes
SET
	CIMBE_RepaymentDate =@CIMBE_RepaymentDate,
	CIMBE_RepaidPer = @CIMBE_RepaidPer,
	CIMBE_ModifiedBy = @CIMBE_ModifiedBy,
	CIMBE_ModifiedOn = CURRENT_TIMESTAMP
WHERE
	CIMBE_EpisodeId = @CIMBE_EpisodeId' 
END
GO
