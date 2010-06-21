
ALTER PROCEDURE [dbo].[SP_UpdateMoneyBackEpisode]

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
	CIMBE_EpisodeId = @CIMBE_EpisodeId 