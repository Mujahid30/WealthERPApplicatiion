 -- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAssetInstrumentCat]
@PAG_AssetGroupCode VARCHAR(2)
AS
SELECT 
	LTRIM(RTRIM(PAIC_AssetInstrumentCategoryCode))AS PAIC_AssetInstrumentCategoryCode,
	PAIC_AssetInstrumentCategoryName
FROM 
	dbo.ProductAssetInstrumentCategory 
WHERE 
	PAG_AssetGroupCode = @PAG_AssetGroupCode