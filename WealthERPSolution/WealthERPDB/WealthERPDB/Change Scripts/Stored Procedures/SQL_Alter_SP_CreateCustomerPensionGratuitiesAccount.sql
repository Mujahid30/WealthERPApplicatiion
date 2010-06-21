-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerPensionGratuitiesAccount]

@CP_PortfolioId int,
@CPGA_AccountNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@CPGA_AccountSource varchar(30),
@CPGA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode VARCHAR(5),
@CPGA_CreatedBy int,
@CPGA_ModifiedBy INT,
@CPGA_AccountOpeningDate datetime,
@CPGA_AccountId INT OUTPUT


AS
	
	
INSERT INTO dbo.CustomerPensionandGratuitiesAccount
(
CP_PortfolioId,
CPGA_AccountNum,
PAG_AssetGroupCode,
PAIC_AssetInstrumentCategoryCode,
CPGA_AccountSource,
CPGA_ISHeldJointly,
XMOH_ModeOfHoldingCode,
CPGA_CreatedBy,
CPGA_CreatedOn,
CPGA_ModifiedOn,
CPGA_ModifiedBy,
CPGA_AccountOpeningDate
)
VALUES
(
@CP_PortfolioId,
@CPGA_AccountNum,
@PAG_AssetGroupCode,
@PAIC_AssetInstrumentCategoryCode,
@CPGA_AccountSource,
@CPGA_ISHeldJointly,
@XMOH_ModeOfHoldingCode,
@CPGA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CPGA_ModifiedBy,
@CPGA_AccountOpeningDate
)
SELECT @CPGA_AccountId=SCOPE_IDENTITY() 