-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetAssetCategoryName

@PAIC_AssetInstrumentCategoryCode VARCHAR(4)

AS

SELECT PAIC_AssetInstrumentCategoryName FROM dbo.ProductAssetInstrumentCategory WHERE PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode 