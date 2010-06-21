
ALTER PROCEDURE [dbo].[SP_UpdatePersonalNetPosition]
@CP_PortfolioId INT,
@CPNP_PersonalNPId INT,
@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
@PAISC_AssetInstrumentSubCategoryCode VARCHAR(6),
@PAG_AssetGroupCode VARCHAR(2),
@CPNP_Name VARCHAR(50),
@CPNP_PurchaseDate DATETIME,
@CPNP_PurchasePrice NUMERIC(18,3),
@CPNP_Quantity NUMERIC(5,0),
@CPNP_PurchaseValue NUMERIC(18,3),
@CPNP_CurrentPrice NUMERIC(18,3),
@CPNP_CurrentValue NUMERIC(18,3),
@CPNP_ModifiedBy INT

AS

BEGIN
	UPDATE dbo.CustomerPersonalNetPosition
	SET
		PAIC_AssetInstrumentCategoryCode = @PAIC_AssetInstrumentCategoryCode,
		PAISC_AssetInstrumentSubCategoryCode = 	@PAISC_AssetInstrumentSubCategoryCode,
		CPNP_Name = @CPNP_Name,
		CPNP_PurchaseDate = @CPNP_PurchaseDate,
		CPNP_Quantity = @CPNP_Quantity,
		CPNP_PurchasePrice = @CPNP_PurchasePrice,
		CPNP_PurchaseValue = @CPNP_PurchaseValue,
		CPNP_CurrentPrice = @CPNP_CurrentPrice,
		CPNP_CurrentValue = @CPNP_CurrentValue,
		CPNP_ModifiedBy = @CPNP_ModifiedBy,
		CPNP_ModifiedOn = CURRENT_TIMESTAMP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		AND
		CPNP_PersonalNPId = @CPNP_PersonalNPId
		AND
		PAG_AssetGroupCode = @PAG_AssetGroupCode
END
 