
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCollectiblesNetPosition]
	@PAIC_AssetInstrumentCategoryCode char(5),
	@PAG_AssetGroupCode char(5),
	@CP_PortfolioId INT,	
	@CCNP_Name varchar(50),
	@CCNP_PurchaseDate datetime,
	@CCNP_PurchaseValue numeric(18, 3),
	@CCNP_CurrentValue numeric(18, 3),
	@CCNP_Remark VARCHAR(100),
	@CCNP_CreatedBy INT,
	@CCNP_ModifiedBy INT
	
AS

INSERT INTO dbo.CustomerCollectibleNetPosition (
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	CP_PortfolioId,
	CCNP_Name,
	CCNP_PurchaseDate,
	CCNP_PurchaseValue,
	CCNP_CurrentValue,
	CCNP_Remark,
	CCNP_CreatedBy,
	CCNP_CreatedOn,
	CCNP_ModifiedBy,
	CCNP_ModifiedOn
) VALUES ( 
	@PAIC_AssetInstrumentCategoryCode,
	@PAG_AssetGroupCode,
	@CP_PortfolioId,
	@CCNP_Name,
	@CCNP_PurchaseDate,
	@CCNP_PurchaseValue,
	@CCNP_CurrentValue,
	@CCNP_Remark,
	@CCNP_CreatedBy,
	CURRENT_TIMESTAMP,
	@CCNP_ModifiedBy,
	CURRENT_TIMESTAMP )  