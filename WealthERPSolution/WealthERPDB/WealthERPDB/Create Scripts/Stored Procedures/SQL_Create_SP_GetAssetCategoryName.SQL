/****** Object:  StoredProcedure [dbo].[SP_GetAssetCategoryName]    Script Date: 06/24/2009 11:58:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetCategoryName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetCategoryName]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetCategoryName]    Script Date: 06/24/2009 11:58:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetCategoryName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetAssetCategoryName

@PAIC_AssetInstrumentCategoryCode VARCHAR(4)

AS

SELECT PAIC_AssetInstrumentCategoryName FROM dbo.ProductAssetInstrumentCategory WHERE PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode' 
END
GO
