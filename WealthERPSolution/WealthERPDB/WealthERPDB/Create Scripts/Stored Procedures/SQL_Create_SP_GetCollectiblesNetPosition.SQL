/****** Object:  StoredProcedure [dbo].[SP_GetCollectiblesNetPosition]    Script Date: 06/24/2009 11:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCollectiblesNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCollectiblesNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCollectiblesNetPosition]    Script Date: 06/24/2009 11:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCollectiblesNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCollectiblesNetPosition]
(
@CP_PortfolioId INT,
@CurrentPage INT = NULL,
@sortOrder VARCHAR(20) = ''PAIC_AssetInstrumentCategoryName ASC''
)
AS

BEGIN
  IF(@CurrentPage IS NULL)	
    BEGIN
	SELECT CCNP.*,PAIC.PAIC_AssetInstrumentCategoryName  FROM dbo.CustomerCollectibleNetPosition AS CCNP
	INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
	ON CCNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE CP_PortfolioId=@CP_PortfolioId
	END
   ELSE IF(@CurrentPage IS NOT NULL)
	 BEGIN
		DECLARE @intStartRow int; 
		DECLARE @intEndRow int;
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
		SET @intEndRow = @CurrentPage * 10;
	 	WITH ENTRIES AS
	 	(
		SELECT CCNP.*,PAIC.PAIC_AssetInstrumentCategoryName ,
			ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''InstrumentCategory DESC''
							THEN PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = ''InstrumentCategory ASC''
							THEN PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = ''Particulars ASC''
							THEN CCNP_Name END ASC,
							CASE WHEN @SortOrder = ''Particulars DESC''
							THEN CCNP_Name END DESC,
							CASE WHEN @SortOrder = ''PurchaseDate ASC''
							THEN CCNP_PurchaseDate END ASC,
							CASE WHEN @SortOrder = ''PurchaseDate DESC''
							THEN CCNP_PurchaseDate END DESC
							 ) as RowNum  
		FROM dbo.CustomerCollectibleNetPosition AS CCNP
		INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
		ON CCNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
		WHERE CP_PortfolioId=@CP_PortfolioId
	 	)
	 	Select * from ENTRIES where RowNum BETWEEN @intStartRow AND @intEndRow
	 	
		SELECT COUNT(*) FROM dbo.CustomerCollectibleNetPosition AS CCNP
	INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
	ON CCNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE CP_PortfolioId=@CP_PortfolioId
	 	
	 END	
 END' 
END
GO
