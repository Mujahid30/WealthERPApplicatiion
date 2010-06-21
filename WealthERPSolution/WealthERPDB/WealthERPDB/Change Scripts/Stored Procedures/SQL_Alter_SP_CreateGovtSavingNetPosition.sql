

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateGovtSavingNetPosition]

	@CGSA_AccountId INT ,
	@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
	@PAG_AssetGroupCode VARCHAR(2),
	@XDI_DebtIssuerCode VARCHAR(5),
	@XIB_InterestBasisCode VARCHAR(5) ,
	@XF_CompoundInterestFrequencyCode VARCHAR(5),
	@XF_InterestPayableFrequencyCode VARCHAR(5),
	@CGSNP_Name VARCHAR(50),
	@CGSNP_PurchaseDate DATETIME,	
	@CGSNP_CurrentValue NUMERIC(18,4),
	@CGSNP_MaturityDate DATETIME,
	@CGSNP_DepositAmount NUMERIC(18,4),
	@CGSNP_MaturityValue NUMERIC(18,4),
	@CGSNP_IsInterestAccumalated TINYINT,
	@CGSNP_InterestAmtAccumalated NUMERIC(18,4),
	@CGSNP_InterestAmtPaidOut NUMERIC(18,4),
	@CGSNP_InterestRate NUMERIC(7,4),
	@CGSNP_CreatedBy INT ,
	@CGSNP_ModifiedBy INT,
	@CGSNP_Remark VARCHAR(100)

AS

INSERT INTO dbo.CustomerGovtSavingNetPosition (
	CGSA_AccountId,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	XDI_DebtIssuerCode,
	XIB_InterestBasisCode,
	XF_CompoundInterestFrequencyCode,
	XF_InterestPayableFrequencyCode,
	CGSNP_Name,
	CGSNP_PurchaseDate,
	CGSNP_CurrentValue,
	CGSNP_MaturityDate,
	CGSNP_DepositAmount,
	CGSNP_MaturityValue,
	CGSNP_IsInterestAccumalated,
	CGSNP_InterestAmtAccumalated,
	CGSNP_InterestAmtPaidOut,
	CGSNP_InterestRate,
	CGSNP_CreatedBy,
	CGSNP_CreatedOn,
	CGSNP_ModifiedBy,
	CGSNP_ModifiedOn,
	CGSNP_Remark
) VALUES ( 
	@CGSA_AccountId,
	@PAIC_AssetInstrumentCategoryCode,
	@PAG_AssetGroupCode,
	@XDI_DebtIssuerCode,
	@XIB_InterestBasisCode,
	@XF_CompoundInterestFrequencyCode,
	@XF_InterestPayableFrequencyCode,
	@CGSNP_Name,
	@CGSNP_PurchaseDate,
	@CGSNP_CurrentValue,
	@CGSNP_MaturityDate,
	@CGSNP_DepositAmount,
	@CGSNP_MaturityValue,
	@CGSNP_IsInterestAccumalated,
	@CGSNP_InterestAmtAccumalated,
	@CGSNP_InterestAmtPaidOut,
	@CGSNP_InterestRate,
	@CGSNP_CreatedBy,
	CURRENT_TIMESTAMP,
	@CGSNP_ModifiedBy,
	CURRENT_TIMESTAMP,
	@CGSNP_Remark

 ) 



 