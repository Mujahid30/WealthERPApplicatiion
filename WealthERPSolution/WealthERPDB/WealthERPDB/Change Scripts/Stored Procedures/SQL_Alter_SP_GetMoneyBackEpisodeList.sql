
ALTER PROCEDURE [dbo].[SP_GetMoneyBackEpisodeList]
@CINP_InsuranceNPId INT

AS

BEGIN
	
	SELECT * FROM
		CustomerInsuranceMoneyBackEpisodes
	WHERE
		CINP_InsuranceNPId = @CINP_InsuranceNPId
	ORDER BY CIMBE_CreatedOn
	
END 