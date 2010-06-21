
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPersonalNetPoition]
(
@CP_PortfolioId INT,
@CurrentPage INT = NULL,
@SortOrder VARCHAR(20) ='CPNP_Name ASC'
)
AS
BEGIN
IF(@CurrentPage IS NULL)
 BEGIN
	SELECT 
	CPNP.*,
	PAIC.PAIC_AssetInstrumentCategoryName,
	PAISC.PAISC_AssetInstrumentSubCategoryName 
	FROM
	dbo.CustomerPersonalNetPosition AS CPNP
	INNER JOIN 
	dbo.ProductAssetInstrumentCategory AS PAIC
	ON CPNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	INNER JOIN dbo.ProductAssetInstrumentSubCategory AS PAISC
	ON CPNP.PAISC_AssetInstrumentSubCategoryCode=PAISC.PAISC_AssetInstrumentSubCategoryCode 
	WHERE CP_PortfolioId = @CP_PortfolioId 
 END
 
ELSE IF(@CurrentPage IS NOT NULL)
  BEGIN
	DECLARE @intStartRow int; 
	DECLARE @intEndRow int;
	SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
	SET @intEndRow = @CurrentPage * 10;
  	WITH Entries AS
  	(
  	SELECT 
	CPNP.*,
	PAIC.PAIC_AssetInstrumentCategoryName,
	PAISC.PAISC_AssetInstrumentSubCategoryName,
	ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'Name DESC'
							THEN CPNP_Name END DESC,
							CASE WHEN @SortOrder = 'Name ASC'
							THEN CPNP_Name END ASC,
							CASE WHEN @SortOrder = 'Category DESC'
							THEN PAISC.PAISC_AssetInstrumentSubCategoryName END DESC,
							CASE WHEN @SortOrder = 'Category ASC'
							THEN PAISC.PAISC_AssetInstrumentSubCategoryName END ASC,
							CASE WHEN @SortOrder = 'CurrentValue DESC'
							THEN CPNP_CurrentValue END DESC,
							CASE WHEN @SortOrder = 'CurrentValue ASC'
							THEN CPNP_CurrentValue END ASC,
							CASE WHEN @SortOrder = 'PurchaseValue DESC'
							THEN CPNP_PurchaseValue END DESC,
							CASE WHEN @SortOrder = 'PurchaseValue ASC'
							THEN CPNP_PurchaseValue END ASC,
							CASE WHEN @SortOrder = 'PurchaseDate DESC'
							THEN CPNP_PurchaseDate END DESC,
							CASE WHEN @SortOrder = 'PurchaseDate ASC'
							THEN CPNP_PurchaseDate END ASC
						) as RowNum 
	FROM dbo.CustomerPersonalNetPosition AS CPNP
	INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
	ON CPNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	INNER JOIN dbo.ProductAssetInstrumentSubCategory AS PAISC
	ON CPNP.PAISC_AssetInstrumentSubCategoryCode=PAISC.PAISC_AssetInstrumentSubCategoryCode 
	WHERE CP_PortfolioId = @CP_PortfolioId 
  	 )
  	 
  	Select * from ENTRIES where RowNum BETWEEN @intStartRow AND @intEndRow
  	  
  	SELECT COUNT(*) FROM
	dbo.CustomerPersonalNetPosition AS CPNP
	INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
	ON CPNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	INNER JOIN dbo.ProductAssetInstrumentSubCategory AS PAISC
	ON CPNP.PAISC_AssetInstrumentSubCategoryCode=PAISC.PAISC_AssetInstrumentSubCategoryCode 
  END 
  
END
 