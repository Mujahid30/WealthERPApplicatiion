-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAssetInstrumentSubCat]
@PAG_AssetGroupCode VARCHAR(2),
@PAIC_AssetInstrumentCategoryCode VARCHAR(4)
AS
SELECT 
 	LTRIM(RTRIM(PAISC_AssetInstrumentSubCategoryCode)) AS PAISC_AssetInstrumentSubCategoryCode,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	PAISC_AssetInstrumentSubCategoryName
FROM 
	dbo.ProductAssetInstrumentSubCategory 
WHERE 
	PAG_AssetGroupCode=@PAG_AssetGroupCode 
	AND 
	PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode 