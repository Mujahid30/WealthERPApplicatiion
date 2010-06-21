ALTER PROCEDURE [dbo].[SP_GetCustomerCashSavingsList]
(
@CP_PortfolioId INT,
@CurrentPage INT = null,
@SortOrder varchar(50)='CCSNP_Name ASC'
)
AS
BEGIN

IF(@CurrentPage IS NULL)	
BEGIN	
	select 
	CCSN.*,PAIC.PAIC_AssetInstrumentCategoryName
from 
	CustomerCashSavingsNetPosition AS CCSN
	INNER JOIN
	dbo.CustomerCashSavingsAccount AS CCSA
	ON CCSN.CCSA_AccountId = CCSA.CCSA_AccountId
	INNER JOIN 
	dbo.ProductAssetInstrumentCategory AS PAIC
	ON CCSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
where 
	CCSA.CP_PortfolioId = @CP_PortfolioId
END

ELSE IF(@CurrentPage IS NOT NULL)
BEGIN
DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
WITH Entries AS
(
	select 
	CCSN.*,PAIC.PAIC_AssetInstrumentCategoryName,
	ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'CCSNP_Name DESC'
							THEN CCSNP_Name END DESC,
							CASE WHEN @SortOrder = 'CCSNP_Name ASC'
							THEN CCSNP_Name END ASC ) as RowNum 
	from 
	CustomerCashSavingsNetPosition AS CCSN
	INNER JOIN
	dbo.CustomerCashSavingsAccount AS CCSA
	ON CCSN.CCSA_AccountId = CCSA.CCSA_AccountId
	INNER JOIN 
	dbo.ProductAssetInstrumentCategory AS PAIC
	ON CCSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	where 
	CCSA.CP_PortfolioId = @CP_PortfolioId
)
	  Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow
	
	
	SELECT COUNT(*) from 
	CustomerCashSavingsNetPosition AS CCSN
	INNER JOIN
	dbo.CustomerCashSavingsAccount AS CCSA
	ON CCSN.CCSA_AccountId = CCSA.CCSA_AccountId
	INNER JOIN 
	dbo.ProductAssetInstrumentCategory AS PAIC
	ON CCSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	where 
	CCSA.CP_PortfolioId = @CP_PortfolioId
	
END
END 