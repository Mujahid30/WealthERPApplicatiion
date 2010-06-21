-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAssetInstrumentSubSubCat]
@PAG_AssetGroupCode VARCHAR(2),
@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
@PAISC_AssetInstrumentSubCategoryCode VARCHAR(6)

AS
SELECT 
	LTRIM(RTRIM(PAISSC_AssetInstrumentSubSubCategoryCode)) AS PAISSC_AssetInstrumentSubSubCategoryCode,
	PAISC_AssetInstrumentSubCategoryCode,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	PAISSC_AssetInstrumentSubSubCategoryName
FROM 
	dbo.ProductAssetInstrumentSubSubCategory 
WHERE 
	PAG_AssetGroupCode=@PAG_AssetGroupCode 
	AND 
	PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode 
	AND 
	PAISC_AssetInstrumentSubCategoryCode=@PAISC_AssetInstrumentSubCategoryCode 