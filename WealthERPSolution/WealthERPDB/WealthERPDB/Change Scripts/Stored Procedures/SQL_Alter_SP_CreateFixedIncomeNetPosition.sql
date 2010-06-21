-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateFixedIncomeNetPosition]

@CFIA_AccountId INT,
@PAIC_AssetInstrumentCategoryCode char(5),
@PAG_AssetGroupCode char(5),
@XDI_DebtIssuerCode char(5),
@XIB_InterestBasisCode char(5),
@XF_CompoundInterestFrequencyCode char(5),
@XF_InterestPayableFrequencyCode char(5),
@CFINP_Name varchar(50),
@CFINP_IssueDate DATETIME,
@CFINP_PrincipalAmount numeric(18,3),
@CFINP_InterestAmtPaidOut numeric(18,3),
@CFINP_InterestAmtAcculumated numeric(18,3),
@CFINP_InterestRate numeric(18,3),
@CFINP_FaceValue numeric(18,3),
@CFINP_MaturityFaceValue NUMERIC(18,3),
@CFINP_PurchasePrice numeric(18, 3),
@CFINP_SubsequentDepositAmount numeric(18, 3),
@XF_DepositFrquencycode CHAR(5),
@CFINP_DebentureNum NUMERIC(5,0),
@CFINP_PurchaseValue numeric(18, 3),
@CFINP_PurchaseDate DATETIME,
@CFINP_MaturityDate DATETIME,
@CFINP_MaturityValue numeric(18, 3),
@CFINP_IsInterestAccumulated TINYINT,
@CFINP_CurrentPrice numeric(18, 3),
@CFINP_CurrentValue numeric(18, 3),
@CFINP_Remark VARCHAR(100),
@CFINP_CreatedBy INT,
@CFINP_ModifiedBy INT
AS

INSERT INTO CustomerFixedIncomeNetPosition 
( 

CFIA_AccountId,
PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode,
XDI_DebtIssuerCode,
XIB_InterestBasisCode,
XF_CompoundInterestFrequencyCode,
XF_InterestPayableFrequencyCode,
CFINP_Name,
CFINP_IssueDate,
CFINP_PrincipalAmount,
CFINP_InterestAmtPaidOut,
CFINP_InterestAmtAcculumated,
CFINP_InterestRate,
CFINP_FaceValue,
CFINP_MaturityFaceValue,
CFINP_PurchasePrice,
CFINP_SubsequentDepositAmount,
XF_DepositFrquencycode,
CFINP_DebentureNum,
CFINP_PurchaseValue,
CFINP_PurchaseDate,
CFINP_MaturityDate,
CFINP_MaturityValue,
CFINP_IsInterestAccumulated,
CFINP_CurrentPrice,
CFINP_CurrentValue,
CFINP_Remark,
CFINP_CreatedBy,
CFINP_CreatedOn,
CFINP_ModifiedBy,
CFINP_ModifiedOn
)

VALUES
( 

@CFIA_AccountId,
@PAIC_AssetInstrumentCategoryCode,
@PAG_AssetGroupCode,
@XDI_DebtIssuerCode,
@XIB_InterestBasisCode,
@XF_CompoundInterestFrequencyCode,
@XF_InterestPayableFrequencyCode,
@CFINP_Name,
@CFINP_IssueDate,
@CFINP_PrincipalAmount,
@CFINP_InterestAmtPaidOut,
@CFINP_InterestAmtAcculumated,
@CFINP_InterestRate,
@CFINP_FaceValue,
@CFINP_MaturityFaceValue,
@CFINP_PurchasePrice,
@CFINP_SubsequentDepositAmount,
@XF_DepositFrquencycode,
@CFINP_DebentureNum,
@CFINP_PurchaseValue,
@CFINP_PurchaseDate,
@CFINP_MaturityDate,
@CFINP_MaturityValue,
@CFINP_IsInterestAccumulated,
@CFINP_CurrentPrice,
@CFINP_CurrentValue,
@CFINP_Remark,
@CFINP_CreatedBy,
CURRENT_TIMESTAMP,
@CFINP_ModifiedBy,
CURRENT_TIMESTAMP
)

 