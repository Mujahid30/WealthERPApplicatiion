
ALTER PROCEDURE [dbo].[SP_GetCustomerPensionAndGratuitiesAccount]
@CPGA_AccountId INT
AS
SELECT 
	CPGA.*,
	PC.PAIC_AssetInstrumentCategoryName 
FROM 
	CustomerPensionandGratuitiesAccount AS CPGA
	INNER JOIN
	ProductAssetInstrumentCategory AS PC
	ON
	CPGA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CPGA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
WHERE 
	CPGA_AccountId = @CPGA_AccountId 