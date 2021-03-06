/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentSubSubCat]    Script Date: 06/24/2009 11:58:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentSubSubCat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetInstrumentSubSubCat]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentSubSubCat]    Script Date: 06/24/2009 11:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentSubSubCat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAssetInstrumentSubSubCat]
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
	PAISC_AssetInstrumentSubCategoryCode=@PAISC_AssetInstrumentSubCategoryCode' 
END
GO
