/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentSubCat]    Script Date: 06/24/2009 11:58:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentSubCat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetInstrumentSubCat]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentSubCat]    Script Date: 06/24/2009 11:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentSubCat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAssetInstrumentSubCat]
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
	PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode' 
END
GO
