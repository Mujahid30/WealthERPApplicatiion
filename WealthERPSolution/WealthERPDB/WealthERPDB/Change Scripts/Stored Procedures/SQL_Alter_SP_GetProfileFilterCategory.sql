-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetProfileFilterCategory
@WPFC_AssetClass VARCHAR(50),
@XCT_CustomerTypeCode VARCHAR(5),
@WPFC_KYFCompliantFlag TINYINT

AS

SELECT WPFC_FilterCategoryCode FROM dbo.WerpProfileFilterCategory
WHERE
WPFC_AssetClass=@WPFC_AssetClass
AND
XCT_CustomerTypeCode=@XCT_CustomerTypeCode
AND
WPFC_KYFCompliantFlag=@WPFC_KYFCompliantFlag 