-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerGovtSavingAccount]

@CP_PortfolioId int,
@CGSA_AccountNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@CGSA_AccountSource varchar(30),
@CGSA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode VARCHAR(5),
@CGSA_CreatedBy int,
@CGSA_ModifiedBy INT,
@CGSA_AccountId INT OUTPUT 

AS
	
	
INSERT INTO CustomerGovtSavingAccount
(
CP_PortfolioId,
CGSA_AccountNum,
PAG_AssetGroupCode,
PAIC_AssetInstrumentCategoryCode,
CGSA_AccountSource,
CGSA_ISHeldJointly,
XMOH_ModeOfHoldingCode,
CGSA_CreatedBy,
CGSA_CreatedOn,
CGSA_ModifiedOn,
CGSA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CGSA_AccountNum,
@PAG_AssetGroupCode,
@PAIC_AssetInstrumentCategoryCode,
@CGSA_AccountSource,
@CGSA_ISHeldJointly,
@XMOH_ModeOfHoldingCode,
@CGSA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CGSA_ModifiedBy
)
SELECT @CGSA_AccountId=SCOPE_IDENTITY() 