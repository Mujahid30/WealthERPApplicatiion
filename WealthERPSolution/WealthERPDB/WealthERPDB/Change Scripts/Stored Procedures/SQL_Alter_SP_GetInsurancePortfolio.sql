
ALTER PROCEDURE [dbo].[SP_GetInsurancePortfolio]
(
	@CP_PortfolioId INT,
	@CurrentPage INT =1,
	@SortOrder VARCHAR(20) ='PAIC_AssetInstrumentCategoryName ASC'
)
AS

SET NOCOUNT ON

DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;

WITH Entries AS
(	
	SELECT 
		CIN.*,PAIC.PAIC_AssetInstrumentCategoryName,
		ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'Category DESC'
							THEN PAIC.PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = 'Category ASC'
							THEN PAIC.PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = 'Particulars DESC'
							THEN CINP_Name END DESC,
							CASE WHEN @SortOrder = 'Particulars ASC'
							THEN CINP_Name END ASC
							 ) as RowNum    
	FROM 
		CustomerInsuranceNetPosition AS CIN
		INNER JOIN
		CustomerInsuranceAccount AS CIA
		ON CIN.CIA_AccountId = CIA.CIA_AccountId
		INNER JOIN 
		dbo.ProductAssetInstrumentCategory AS PAIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE
		CIA.CP_PortfolioId = @CP_PortfolioId
)
	
SELECT * FROM Entries WHERE  RowNum BETWEEN @intStartRow AND @intEndRow 	

SELECT COUNT(*) 	FROM 
		CustomerInsuranceNetPosition AS CIN
		INNER JOIN
		CustomerInsuranceAccount AS CIA
		ON CIN.CIA_AccountId = CIA.CIA_AccountId
		INNER JOIN 
		dbo.ProductAssetInstrumentCategory AS PAIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE
		CIA.CP_PortfolioId = @CP_PortfolioId
	
SET NOCOUNT OFF
 