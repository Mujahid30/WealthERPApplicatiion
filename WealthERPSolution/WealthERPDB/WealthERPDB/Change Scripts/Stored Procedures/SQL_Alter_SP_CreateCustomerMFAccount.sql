-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerMFAccount]

@CP_PortfolioId int,
@CMFA_FolioNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PA_AMCCode INT ,
@CMFA_IsJointlyHeld TINYINT,
@CMFA_AccountOpeningDate DATETIME,
@XMOH_ModeOfHoldingCode VARCHAR(5),
@CMFA_CreatedBy int,
@CMFA_ModifiedBy INT,
@CMFA_AccountId INT OUTPUT


AS

INSERT INTO CustomerMutualFundAccount
(
CP_PortfolioId,
CMFA_FolioNum,
PAG_AssetGroupCode,
PA_AMCCode,
CMFA_IsJointlyHeld,
XMOH_ModeOfHoldingCode,
CMFA_CreatedBy,
CMFA_CreatedOn,
CMFA_ModifiedOn,
CMFA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CMFA_FolioNum,
@PAG_AssetGroupCode,
@PA_AMCCode,
@CMFA_IsJointlyHeld,
@XMOH_ModeOfHoldingCode,
@CMFA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CMFA_ModifiedBy
)
SELECT @CMFA_AccountId=SCOPE_IDENTITY() 