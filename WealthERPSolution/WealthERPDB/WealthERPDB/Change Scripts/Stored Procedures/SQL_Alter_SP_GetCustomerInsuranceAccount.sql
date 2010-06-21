
ALTER PROCEDURE [dbo].[SP_GetCustomerInsuranceAccount]
@CIA_AccountId INT

AS
BEGIN
	SELECT 
		CIA.*,
		PIC.PAIC_AssetInstrumentCategoryName
	FROM 
		CustomerInsuranceAccount AS CIA
		INNER JOIN
		dbo.ProductAssetInstrumentCategory AS PIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PIC.PAIC_AssetInstrumentCategoryCode AND CIA.PAG_AssetGroupCode = PIC.PAG_AssetGroupCode
	WHERE 
		@CIA_AccountId = CIA_AccountId	
END
 