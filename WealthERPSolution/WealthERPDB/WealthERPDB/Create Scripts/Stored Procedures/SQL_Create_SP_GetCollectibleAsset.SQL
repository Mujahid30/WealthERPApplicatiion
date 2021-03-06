/****** Object:  StoredProcedure [dbo].[SP_GetCollectibleAsset]    Script Date: 06/24/2009 11:58:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCollectibleAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCollectibleAsset]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCollectibleAsset]    Script Date: 06/24/2009 11:58:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCollectibleAsset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCollectibleAsset]

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


' 
END
GO
