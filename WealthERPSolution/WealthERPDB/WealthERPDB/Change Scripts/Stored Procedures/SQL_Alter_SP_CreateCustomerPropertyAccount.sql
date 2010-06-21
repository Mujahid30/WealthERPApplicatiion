-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerPropertyAccount]

@CP_PortfolioId int,
@CPA_AccountNum VARCHAR(30),
@PAG_AssetGroupCode varchar(2),
@PAISC_AssetInstrumentSubCategoryCode varchar(6),
@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
@CPA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode varchar(5),
@CPA_CreatedBy int,
@CPA_ModifiedBy INT,
@CPA_AccountId INT OUTPUT


AS
	
	
INSERT INTO CustomerPropertyAccount
(
CP_PortfolioId,
CPA_AccountNum ,
PAG_AssetGroupCode,
PAISC_AssetInstrumentSubCategoryCode,
PAIC_AssetInstrumentCategoryCode,
CPA_IsHeldJointly,
XMOH_ModeOfHoldingCode,
CPA_CreatedBy,
CPA_CreatedOn,
CPA_ModifiedOn,
CPA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CPA_AccountNum ,
@PAG_AssetGroupCode,
@PAISC_AssetInstrumentSubCategoryCode,
@PAIC_AssetInstrumentCategoryCode,
@CPA_IsHeldJointly,
@XMOH_ModeOfHoldingCode,
@CPA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CPA_ModifiedBy
)
SELECT @CPA_AccountId=SCOPE_IDENTITY() 