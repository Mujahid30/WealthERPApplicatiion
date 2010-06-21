-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerFixedIncomeAccount]

@CP_PortfolioId int,
@CFIA_AccountNum varchar(30),
@PAG_AssetGroupCode char(2),
@PAIC_AssetInstrumentCategoryCode char(4),
@CFIA_AccountSource varchar(30),
@CFIA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode CHAR(5),
@CFIA_CreatedBy int,
@CFIA_ModifiedBy INT,
@CFIA_AccountId INT OUTPUT



AS
	
	
INSERT INTO CustomerFixedIncomeAccount
(
CP_PortfolioId,
CFIA_AccountNum,
PAG_AssetGroupCode,
PAIC_AssetInstrumentCategoryCode,
CFIA_AccountSource,
CFIA_ISHeldJointly,
XMOH_ModeOfHoldingCode,
CFIA_CreatedBy,
CFIA_CreatedOn,
CFIA_ModifiedOn,
CFIA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CFIA_AccountNum,
@PAG_AssetGroupCode,
@PAIC_AssetInstrumentCategoryCode,
@CFIA_AccountSource,
@CFIA_ISHeldJointly,
@XMOH_ModeOfHoldingCode,
@CFIA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CFIA_ModifiedBy
)
SELECT @CFIA_AccountId=SCOPE_IDENTITY() 