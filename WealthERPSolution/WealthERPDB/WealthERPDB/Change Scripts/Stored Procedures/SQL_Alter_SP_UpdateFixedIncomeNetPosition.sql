-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateFixedIncomeNetPosition]
@CFINP_FINPId INT,
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
@CFINP_ModifiedBy INT

AS

UPDATE dbo.CustomerFixedIncomeNetPosition SET



CFIA_AccountId=@CFIA_AccountId,
PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode=@PAG_AssetGroupCode,
XDI_DebtIssuerCode=@XDI_DebtIssuerCode,
XIB_InterestBasisCode=@XIB_InterestBasisCode,
XF_CompoundInterestFrequencyCode=@XF_CompoundInterestFrequencyCode,
XF_InterestPayableFrequencyCode=@XF_InterestPayableFrequencyCode,
CFINP_Name=@CFINP_Name,
CFINP_IssueDate=@CFINP_IssueDate,
CFINP_PrincipalAmount=@CFINP_PrincipalAmount,
CFINP_InterestAmtPaidOut=@CFINP_InterestAmtPaidOut,
CFINP_InterestAmtAcculumated=@CFINP_InterestAmtAcculumated,
CFINP_InterestRate=@CFINP_InterestRate,
CFINP_FaceValue=@CFINP_FaceValue,
CFINP_MaturityFaceValue=@CFINP_MaturityFaceValue,
CFINP_PurchasePrice=@CFINP_PurchasePrice,
CFINP_SubsequentDepositAmount=@CFINP_SubsequentDepositAmount,
XF_DepositFrquencycode=@XF_DepositFrquencycode,
CFINP_DebentureNum=@CFINP_DebentureNum,
CFINP_PurchaseValue=@CFINP_PurchaseValue,
CFINP_PurchaseDate=@CFINP_PurchaseDate,
CFINP_MaturityDate=@CFINP_MaturityDate,
CFINP_MaturityValue=@CFINP_MaturityValue,
CFINP_IsInterestAccumulated=@CFINP_IsInterestAccumulated,
CFINP_CurrentPrice=@CFINP_CurrentPrice,
CFINP_CurrentValue=@CFINP_CurrentValue,
CFINP_Remark=@CFINP_Remark,
CFINP_ModifiedBy=@CFINP_ModifiedBy,
CFINP_ModifiedOn=CURRENT_TIMESTAMP


WHERE CFINP_FINPId=@CFINP_FINPId 