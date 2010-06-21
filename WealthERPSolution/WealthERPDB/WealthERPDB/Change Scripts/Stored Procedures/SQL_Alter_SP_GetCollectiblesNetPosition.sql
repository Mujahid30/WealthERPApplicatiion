
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCollectiblesNetPosition]
(
@CP_PortfolioId INT,
@CurrentPage INT = NULL,
@sortOrder VARCHAR(20) = 'PAIC_AssetInstrumentCategoryName ASC'
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
							CASE WHEN @SortOrder = 'InstrumentCategory DESC'
							THEN PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = 'InstrumentCategory ASC'
							THEN PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = 'Particulars ASC'
							THEN CCNP_Name END ASC,
							CASE WHEN @SortOrder = 'Particulars DESC'
							THEN CCNP_Name END DESC,
							CASE WHEN @SortOrder = 'PurchaseDate ASC'
							THEN CCNP_PurchaseDate END ASC,
							CASE WHEN @SortOrder = 'PurchaseDate DESC'
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
 END 