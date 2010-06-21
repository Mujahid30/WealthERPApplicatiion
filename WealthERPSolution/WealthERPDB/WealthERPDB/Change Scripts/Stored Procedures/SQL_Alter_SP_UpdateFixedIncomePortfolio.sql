-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateFixedIncomePortfolio]
@CIFIP_FITransactionId INT,
@C_CustomerId INT,
@CFIA_AccountId INT,
@PAIC_AssetInstrumentCategoryCode char(5),
@PAG_AssetGroupCode char(5),
@WDI_DebtIssuerCode char(5),
@WIB_InterestBasisCode char(5),
@CIFIP_CompoundInterestFrequencyCode char(5),
@CIFIP_InterestPayableFrequencyCode char(5),
@CIFIP_Name varchar(50),
@CIFIP_IssueDate DATETIME,
@CIFIP_PrincipalAmount numeric(18,3),
@CIFIP_InterestAmtPaidOut numeric(18,3),
@CIFIP_InterestAmtAcculumated numeric(18,3),
@CIFIP_InterestRate numeric(18,3),
@CIFIP_FaceValue numeric(18,3),
@CIFIP_MaturityFaceValue NUMERIC(18,3),
@CIFIP_PurchasePrice numeric(18, 3),
@CIFIP_SubsequentDepositAmount numeric(18, 3),
@CIFIP_DepositFrquencycode CHAR(5),
@CIFIP_DebentureNum NUMERIC(5,0),
@CIFIP_PurchaseValue numeric(18, 3),
@CIFIP_PurchaseDate DATETIME,
@CIFIP_MaturityDate DATETIME,
@CIFIP_MaturityValue numeric(18, 3),
@CIFIP_IsInterestAccumulated TINYINT,
@CIFIP_CurrentPrice numeric(18, 3),
@CIFIP_CurrentValue numeric(18, 3),
@CIFIP_Remark VARCHAR(100),
@CIFIP_ModifiedBy INT

AS

UPDATE dbo.CustomerInvestmentFixedIncomePortfolio SET


C_CustomerId=@C_CustomerId,
CFIA_AccountId=@CFIA_AccountId,
PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode=@PAG_AssetGroupCode,
WDI_DebtIssuerCode=@WDI_DebtIssuerCode,
WIB_InterestBasisCode=@WIB_InterestBasisCode,
CIFIP_CompoundInterestFrequencyCode=@CIFIP_CompoundInterestFrequencyCode,
CIFIP_InterestPayableFrequencyCode=@CIFIP_InterestPayableFrequencyCode,
CIFIP_Name=@CIFIP_Name,
CIFIP_IssueDate=@CIFIP_IssueDate,
CIFIP_PrincipalAmount=@CIFIP_PrincipalAmount,
CIFIP_InterestAmtPaidOut=@CIFIP_InterestAmtPaidOut,
CIFIP_InterestAmtAcculumated=@CIFIP_InterestAmtAcculumated,
CIFIP_InterestRate=@CIFIP_InterestRate,
CIFIP_FaceValue=@CIFIP_FaceValue,
CIFIP_MaturityFaceValue=@CIFIP_MaturityFaceValue,
CIFIP_PurchasePrice=@CIFIP_PurchasePrice,
CIFIP_SubsequentDepositAmount=@CIFIP_SubsequentDepositAmount,
CIFIP_DepositFrquencycode=@CIFIP_DepositFrquencycode,
CIFIP_DebentureNum=@CIFIP_DebentureNum,
CIFIP_PurchaseValue=@CIFIP_PurchaseValue,
CIFIP_PurchaseDate=@CIFIP_PurchaseDate,
CIFIP_MaturityDate=@CIFIP_MaturityDate,
CIFIP_MaturityValue=@CIFIP_MaturityValue,
CIFIP_IsInterestAccumulated=@CIFIP_IsInterestAccumulated,
CIFIP_CurrentPrice=@CIFIP_CurrentPrice,
CIFIP_CurrentValue=@CIFIP_CurrentValue,
CIFIP_Remark=@CIFIP_Remark,
CIFIP_ModifiedBy=@CIFIP_ModifiedBy,
CIFIP_ModifiedOn=CURRENT_TIMESTAMP


WHERE CIFIP_FITransactionId=@CIFIP_FITransactionId 