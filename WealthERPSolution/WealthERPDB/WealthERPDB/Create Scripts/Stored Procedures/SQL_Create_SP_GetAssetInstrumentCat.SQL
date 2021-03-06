/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentCat]    Script Date: 06/24/2009 11:58:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentCat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetInstrumentCat]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetInstrumentCat]    Script Date: 06/24/2009 11:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetInstrumentCat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAssetInstrumentCat]
@PAG_AssetGroupCode VARCHAR(2)
AS
SELECT 
	LTRIM(RTRIM(PAIC_AssetInstrumentCategoryCode))AS PAIC_AssetInstrumentCategoryCode,
	PAIC_AssetInstrumentCategoryName
FROM 
	dbo.ProductAssetInstrumentCategory 
WHERE 
	PAG_AssetGroupCode = @PAG_AssetGroupCode' 
END
GO
