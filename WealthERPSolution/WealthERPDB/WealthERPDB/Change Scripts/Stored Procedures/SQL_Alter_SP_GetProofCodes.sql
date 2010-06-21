-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetProofCodes
@WPFC_FilterCategoryCode VARCHAR(50)

AS

SELECT * FROM WerpProofMandatoryLookup
WHERE
WPFC_FilterCategoryCode=@WPFC_FilterCategoryCode 