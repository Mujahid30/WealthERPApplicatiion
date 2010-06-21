-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateInsuranceAccount]

@CP_PortfolioId	INT,
@PAIC_AssetInstrumentCategoryCode	varchar(4),
@PAG_AssetGroupCode	varchar(2),
@CIA_PolicyNum  VARCHAR(30),
@CIA_AccountNum VARCHAR(30),
@CIA_CreatedBy	INT,
@CIA_ModifiedBy	INT,
@AccountId INT OUTPUT

AS
INSERT INTO dbo.CustomerInsuranceAccount (
	CP_PortfolioId,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	CIA_PolicyNum,
	CIA_AccountNum,
	CIA_CreatedBy,
	CIA_CreatedOn,
	CIA_ModifiedBy,
	CIA_ModifiedOn
) 
VALUES
 (  @CP_PortfolioId,
@PAIC_AssetInstrumentCategoryCode,
@PAG_AssetGroupCode,
@CIA_PolicyNum,
@CIA_AccountNum,
@CIA_CreatedBy,
CURRENT_TIMESTAMP,
@CIA_ModifiedBy,
CURRENT_TIMESTAMP)
SELECT @AccountId=SCOPE_IDENTITY()
 