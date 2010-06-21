
ALTER PROCEDURE [dbo].[SP_DeleteMoneyBackEpisode]
@CIMBE_EpisodeId INT

AS

BEGIN
	DELETE FROM CustomerInsuranceMoneyBackEpisodes
	WHERE CIMBE_EpisodeId = @CIMBE_EpisodeId
END 