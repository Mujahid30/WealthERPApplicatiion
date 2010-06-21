
ALTER PROCEDURE [dbo].[SP_UpdatePensionAndGratuities]
@CPGNP_PensionGratutiesNPId INT,
@CPGNP_OrganizationName	varchar(50),
@CPGNP_DepositAmount	numeric(18,4),
@XFY_FiscalYearCode	VARCHAR(5),
@CPGNP_EmployeeContri numeric(18,4),
@CPGNP_EmployerContri numeric(18,4),
@CPGNP_InterestRate	numeric(6, 3),
@XIB_InterestBasisCode	VARCHAR(5),
@XF_CompoundInterestFrequencyCode	VARCHAR(5),
@XF_InterestPayableFrequencyCode	VARCHAR(5),
@CPGNP_IsInterestAccumalated	tinyint,
@CPGNP_InterestAmtAccumalated	numeric(18, 4),
@CPGNP_InterestAmtPaidOut	numeric(18, 4),
@CPGNP_CurrentValue	numeric(18, 4),
@CPGNP_Remark VARCHAR(100),
@CPGNP_ModifiedBy INT 

AS

BEGIN

	IF (@XIB_InterestBasisCode = 'CI')
	BEGIN
		
		UPDATE CustomerPensionandGratuitiesNetPosition
		SET
			XFY_FiscalYearCode = @XFY_FiscalYearCode,
			--XF_InterestPayableFrequencyCode = @XF_InterestPayableFrequencyCode,
			XF_CompoundInterestFrequencyCode = @XF_CompoundInterestFrequencyCode,
			CPGNP_InterestRate = @CPGNP_InterestRate,
			CPGNP_OrganizationName = @CPGNP_OrganizationName,
			CPGNP_DepositAmount = @CPGNP_DepositAmount,
			CPGNP_CurrentValue = @CPGNP_CurrentValue,
			XIB_InterestBasisCode = @XIB_InterestBasisCode,
			CPGNP_IsInterestAccumalated = @CPGNP_IsInterestAccumalated,
			CPGNP_InterestAmtAccumalated = @CPGNP_InterestAmtAccumalated,
			CPGNP_InterestAmtPaidOut = @CPGNP_InterestAmtPaidOut,
			CPGNP_EmployeeContri = @CPGNP_EmployeeContri,
			CPGNP_EmployerContri = @CPGNP_EmployerContri,
			CPGNP_Remark = @CPGNP_Remark,
			CPGNP_ModifiedBy = @CPGNP_ModifiedBy,
			CPGNP_ModifiedOn = CURRENT_TIMESTAMP
		WHERE
			CPGNP_PensionGratutiesNPId = @CPGNP_PensionGratutiesNPId
		
	END
	ELSE
	BEGIN
		
		UPDATE CustomerPensionandGratuitiesNetPosition
		SET
			XFY_FiscalYearCode = @XFY_FiscalYearCode,
			XF_InterestPayableFrequencyCode = @XF_InterestPayableFrequencyCode,
			--XF_CompoundInterestFrequencyCode = @XF_CompoundInterestFrequencyCode,
			CPGNP_InterestRate = @CPGNP_InterestRate,
			CPGNP_OrganizationName = @CPGNP_OrganizationName,
			CPGNP_DepositAmount = @CPGNP_DepositAmount,
			CPGNP_CurrentValue = @CPGNP_CurrentValue,
			XIB_InterestBasisCode = @XIB_InterestBasisCode,
			CPGNP_IsInterestAccumalated = @CPGNP_IsInterestAccumalated,
			CPGNP_InterestAmtAccumalated = @CPGNP_InterestAmtAccumalated,
			CPGNP_InterestAmtPaidOut = @CPGNP_InterestAmtPaidOut,
			CPGNP_EmployeeContri = @CPGNP_EmployeeContri,
			CPGNP_EmployerContri = @CPGNP_EmployerContri,
			CPGNP_Remark = @CPGNP_Remark,
			CPGNP_ModifiedBy = @CPGNP_ModifiedBy,
			CPGNP_ModifiedOn = CURRENT_TIMESTAMP
		WHERE
			CPGNP_PensionGratutiesNPId = @CPGNP_PensionGratutiesNPId
	END
	
END 