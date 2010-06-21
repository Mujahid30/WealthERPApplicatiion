
ALTER PROCEDURE [dbo].[SP_CreateCustomerInsuranceAccount]
@CP_PortfolioId INT,
@CIA_PolicyNum	varchar(30),
@PAIC_AssetInstrumentCategoryCode	VARCHAR(4),
@CIA_AccountNum	varchar(30),
@PAG_AssetGroupCode	VARCHAR(2),
@CIA_CreatedBy	INT,
@CIA_ModifiedBy	INT,
@CIA_AccountId INT output

AS

INSERT INTO CustomerInsuranceAccount
(
	CP_PortfolioId,
	CIA_PolicyNum,
	PAIC_AssetInstrumentCategoryCode,
	CIA_AccountNum,
	PAG_AssetGroupCode,
	CIA_CreatedBy,
	CIA_CreatedOn,
	CIA_ModifiedBy,
	CIA_ModifiedOn
)
VALUES
(
	@CP_PortfolioId,
	@CIA_PolicyNum,
	@PAIC_AssetInstrumentCategoryCode,
	@CIA_AccountNum,
	@PAG_AssetGroupCode,
	@CIA_CreatedBy,
	CURRENT_TIMESTAMP,
	@CIA_ModifiedBy,
	CURRENT_TIMESTAMP
)

SELECT @CIA_AccountId=SCOPE_IDENTITY() 