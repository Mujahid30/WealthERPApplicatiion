-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerInsuranceAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode	varchar(2)
AS

BEGIN
	
	SELECT 
		CIA.*,
		PC.PAIC_AssetInstrumentCategoryName
	FROM 
		dbo.CustomerInsuranceAccount AS CIA
		INNER JOIN
		dbo.ProductAssetInstrumentCategory AS PC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CIA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
		
	WHERE 
		CIA.CP_PortfolioId = @CP_PortfolioId 
		AND 
		CIA.PAG_AssetGroupCode = @PAG_AssetGroupCode	
	
END

 