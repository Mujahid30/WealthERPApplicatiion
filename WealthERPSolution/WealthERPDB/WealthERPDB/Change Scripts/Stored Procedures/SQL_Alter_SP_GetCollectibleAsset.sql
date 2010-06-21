
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCollectibleAsset]

@CCNP_CollectibleNPId	int	

AS

BEGIN
	
	SELECT 
		CCNP.*,
		PAIC.PAIC_AssetInstrumentCategoryName
	FROM 
		dbo.CustomerCollectibleNetPosition AS CCNP
		INNER JOIN 
		dbo.ProductAssetInstrumentCategory AS PAIC 
		ON CCNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE 
		@CCNP_CollectibleNPId=CCNP_CollectibleNPId
	
END


 