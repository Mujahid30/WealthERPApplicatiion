

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreatePersonalNetPosition]
@CP_PortfolioId INT,
@PAISC_AssetInstrumentSubCategoryCode varchar(6),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@PAG_AssetGroupCode varchar(2),
@CPNP_Name varchar(50),
@CPNP_PurchaseDate DATETIME,
@CPNP_Quantity numeric(5, 0),
@CPNP_PurchasePrice numeric(18, 3),
@CPNP_PurchaseValue numeric(18, 3),
@CPNP_CurrentPrice numeric(18, 3),
@CPNP_CurrentValue numeric(18, 3),
@CPNP_CreatedBy INT,
@CPNP_ModifiedBy INT 

AS
 INSERT INTO dbo.CustomerPersonalNetPosition (
 	CP_PortfolioId,
 	PAISC_AssetInstrumentSubCategoryCode,
 	PAIC_AssetInstrumentCategoryCode,
 	PAG_AssetGroupCode,
 	CPNP_Name,
 	CPNP_PurchaseDate,
 	CPNP_Quantity,
 	CPNP_PurchasePrice,
 	CPNP_PurchaseValue,
 	CPNP_CurrentPrice,
 	CPNP_CurrentValue,
 	CPNP_CreatedBy,
 	CPNP_CreatedOn,
 	CPNP_ModifiedBy,
 	CPNP_ModifiedOn
 )
 VALUES ( 
	@CP_PortfolioId,
 	@PAISC_AssetInstrumentSubCategoryCode,
 	@PAIC_AssetInstrumentCategoryCode,
 	@PAG_AssetGroupCode,
 	@CPNP_Name,
 	@CPNP_PurchaseDate,
 	@CPNP_Quantity,
 	@CPNP_PurchasePrice,
 	@CPNP_PurchaseValue,
 	@CPNP_CurrentPrice,
 	@CPNP_CurrentValue,
 	@CPNP_CreatedBy,
 	CURRENT_TIMESTAMP,
 	@CPNP_ModifiedBy,
 	CURRENT_TIMESTAMP
 
 	) 

 