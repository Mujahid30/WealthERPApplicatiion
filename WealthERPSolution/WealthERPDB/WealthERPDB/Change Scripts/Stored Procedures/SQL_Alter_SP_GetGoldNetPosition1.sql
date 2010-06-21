
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetGoldNetPosition]
(
@CP_PortfolioId INT,
@CurrentPage INT = NULL,
@SortOrder VARCHAR(20) ='PAIC_AssetInstrumentCategoryName ASC'
)
AS
BEGIN
IF(@CurrentPage IS NULL)
BEGIN
SELECT CGNP.*,PAIC.PAIC_AssetInstrumentCategoryName FROM CustomerGoldNetPosition  AS CGNP
INNER JOIN
dbo.ProductAssetInstrumentCategory  AS PAIC
ON CGNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
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
SELECT CGNP.*,PAIC.PAIC_AssetInstrumentCategoryName ,
	ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'InstrumentCategory DESC'
							THEN PAIC.PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = 'InstrumentCategory ASC'
							THEN PAIC.PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = 'PurchaseDate DESC'
							THEN CGNP_PurchaseDate END DESC,
							CASE WHEN @SortOrder = 'PurchaseDate ASC'
							THEN CGNP_PurchaseDate END ASC,
							CASE WHEN @SortOrder = 'PurchaseValue DESC'
							THEN CGNP_PurchaseValue END DESC,
							CASE WHEN @SortOrder = 'PurchaseValue ASC'
							THEN CGNP_PurchaseValue END ASC,
							CASE WHEN @SortOrder = 'CurrentValue DESC'
							THEN CGNP_CurrentValue END DESC,
							CASE WHEN @SortOrder = 'CurrentValue ASC'
							THEN CGNP_CurrentValue END ASC
							 ) as RowNum 

FROM CustomerGoldNetPosition  AS CGNP
INNER JOIN dbo.ProductAssetInstrumentCategory  AS PAIC
ON CGNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CP_PortfolioId=@CP_PortfolioId		
)
 Select * from ENTRIES where RowNum BETWEEN @intStartRow AND @intEndRow
SELECT COUNT(*) FROM CustomerGoldNetPosition  AS CGNP
INNER JOIN dbo.ProductAssetInstrumentCategory  AS PAIC
ON CGNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CP_PortfolioId=@CP_PortfolioId 

END

END



 