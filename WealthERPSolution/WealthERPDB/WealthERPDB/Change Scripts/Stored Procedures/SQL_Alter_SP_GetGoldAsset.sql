
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetGoldAsset]

@CGNP_GoldNPId int

as

select CGNP.*,PAIC.PAIC_AssetInstrumentCategoryName from CustomerGoldNetPosition  AS CGNP
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
ON CGNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode


where CGNP_GoldNPId=@CGNP_GoldNPId

 