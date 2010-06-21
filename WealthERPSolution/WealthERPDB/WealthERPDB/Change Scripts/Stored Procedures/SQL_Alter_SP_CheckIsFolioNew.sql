ALTER PROCEDURE SP_CheckIsFolioNew
@FolioNumber VARCHAR(50)

AS

SET NOCOUNT ON

	SELECT COUNT(*) AS CNT
	FROM CustomerMutualFundAccount
	WHERE CMFA_FolioNum = @FolioNumber


SET NOCOUNT OFF
 