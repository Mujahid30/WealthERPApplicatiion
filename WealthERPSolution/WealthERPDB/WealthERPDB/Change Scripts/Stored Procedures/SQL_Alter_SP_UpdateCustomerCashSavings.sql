-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateCustomerCashSavings]

@CCSNP_CashSavingsNPId	INT,
@PAIC_AssetInstrumentCategoryCode	varchar(4),
@PAG_AssetGroupCode	varchar(2),
@CCSA_AccountId	INT,
@XDI_DebtIssuerCode	varchar(5),
@XIB_InterestBasisCode	varchar(5),
@XF_CompoundInterestFrequencyCode	varchar(5),
@XF_InterestPayoutFrequencyCode	varchar(5),
@CCSNP_Name	varchar(50),
@CCSNP_DepositAmount	numeric(18, 4),
@CCSNP_DepositDate	DATETIME,
@CCSNP_CurrentValue	numeric(18, 4),
@CCSNP_InterestRate	numeric(10, 5),
@CCSNP_InterestAmntPaidOut	numeric(18, 4),
@CCSNP_IsInterestAccumulated	TINYINT,
@CCSNP_InterestAmntAccumulated	numeric(18, 4),
@CCSNP_Remark	varchar(100),
@CCSNP_ModifiedBy	INT


AS


UPDATE dbo.CustomerCashSavingsNetPosition SET




PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode=@PAG_AssetGroupCode,
CCSA_AccountId=@CCSA_AccountId,
XDI_DebtIssuerCode=@XDI_DebtIssuerCode,
XIB_InterestBasisCode=@XIB_InterestBasisCode,
XF_CompoundInterestFrequencyCode=@XF_CompoundInterestFrequencyCode,
XF_InterestPayoutFrequencyCode=@XF_InterestPayoutFrequencyCode,
CCSNP_Name=@CCSNP_Name,
CCSNP_DepositAmount=@CCSNP_DepositAmount,
CCSNP_DepositDate=@CCSNP_DepositDate,
CCSNP_CurrentValue=@CCSNP_CurrentValue,
CCSNP_InterestRate=@CCSNP_InterestRate,
CCSNP_InterestAmntPaidOut=@CCSNP_InterestAmntPaidOut,
CCSNP_IsInterestAccumulated=@CCSNP_IsInterestAccumulated,
CCSNP_InterestAmntAccumulated=@CCSNP_InterestAmntAccumulated,
CCSNP_Remark=@CCSNP_Remark,
CCSNP_ModifiedBy=@CCSNP_ModifiedBy,
CCSNP_ModifiedOn=CURRENT_TIMESTAMP




WHERE CCSNP_CashSavingsNPId=@CCSNP_CashSavingsNPId 