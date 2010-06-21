-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerCashSavingsAccount]

@CP_PortfolioId int,
@CCSA_AccountNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@CCSA_BankName varchar(30) = NULL,
@CCSA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode varchar(5),
@CCSA_CreatedBy int,
@CCSA_ModifiedBy INT,
@CCSA_AccountOpeningDate DATETIME = NULL,
@CCSA_AccountId INT OUTPUT


AS

SET NOCOUNT ON

INSERT INTO CustomerCashSavingsAccount
(
	CP_PortfolioId,
	CCSA_AccountNum,
	PAG_AssetGroupCode,
	PAIC_AssetInstrumentCategoryCode,
	CCSA_BankName,
	CCSA_ISHeldJointly,
	XMOH_ModeOfHoldingCode,
	CCSA_CreatedBy,
	CCSA_CreatedOn,
	CCSA_ModifiedOn,
	CCSA_ModifiedBy,
	CCSA_AccountOpeningDate
)
VALUES
(
	@CP_PortfolioId,
	@CCSA_AccountNum,
	@PAG_AssetGroupCode,
	@PAIC_AssetInstrumentCategoryCode,
	@CCSA_BankName,
	@CCSA_ISHeldJointly,
	@XMOH_ModeOfHoldingCode,
	@CCSA_CreatedBy,
	CURRENT_TIMESTAMP,
	CURRENT_TIMESTAMP,
	@CCSA_ModifiedBy,
	@CCSA_AccountOpeningDate
)
SELECT @CCSA_AccountId=SCOPE_IDENTITY()

SET NOCOUNT OFF
 