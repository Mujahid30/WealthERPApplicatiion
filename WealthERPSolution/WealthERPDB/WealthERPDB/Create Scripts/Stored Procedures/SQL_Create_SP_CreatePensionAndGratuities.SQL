/****** Object:  StoredProcedure [dbo].[SP_CreatePensionAndGratuities]    Script Date: 06/24/2009 11:13:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePensionAndGratuities]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreatePensionAndGratuities]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreatePensionAndGratuities]    Script Date: 06/24/2009 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePensionAndGratuities]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_CreatePensionAndGratuities]
(
	@CPGA_AccountId	int,
	@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
	@PAG_AssetGroupCode VARCHAR(2),
	@CPGNP_OrganizationName	varchar(50),
	@CPGNP_DepositAmount numeric(18,4),
	@XFY_FiscalYearCode	VARCHAR(5),
	@CPGNP_EmployeeContri numeric(18,4),
	@CPGNP_EmployerContri numeric(18,4),
	@CPGNP_InterestRate	numeric(6, 3),
	@XIB_InterestBasisCode VARCHAR(5),
	@XF_CompoundInterestFrequencyCode VARCHAR(5),
	@XF_InterestPayableFrequencyCode VARCHAR(5),
	@CPGNP_IsInterestAccumalated TINYINT,
	@CPGNP_InterestAmtAccumalated NUMERIC(18, 4),
	@CPGNP_InterestAmtPaidOut NUMERIC(18, 4),
	@CPGNP_CurrentValue	NUMERIC(18, 4),
	@CPGNP_Remark VARCHAR(100),
	@CPGNP_CreatedBy INT,
	@CPGNP_ModifiedBy INT 
)

AS


BEGIN
	
	IF (@XIB_InterestBasisCode = ''CI'')
	BEGIN
		
		INSERT INTO CustomerPensionandGratuitiesNetPosition
		(
			CPGA_AccountId,
			PAIC_AssetInstrumentCategoryCode,
			PAG_AssetGroupCode,
			XFY_FiscalYearCode,
			--XF_InterestPayableFrequencyCode,
			XF_CompoundInterestFrequencyCode,
			CPGNP_InterestRate,
			CPGNP_OrganizationName,
			CPGNP_DepositAmount,
			CPGNP_CurrentValue,
			XIB_InterestBasisCode,
			CPGNP_IsInterestAccumalated,
			CPGNP_InterestAmtAccumalated,
			CPGNP_InterestAmtPaidOut,
			CPGNP_EmployeeContri,
			CPGNP_EmployerContri,
			CPGNP_Remark,
			CPGNP_CreatedBy,
			CPGNP_CreatedOn,
			CPGNP_ModifiedBy,
			CPGNP_ModifiedOn
		)
		Values
		(
			@CPGA_AccountId,
			@PAIC_AssetInstrumentCategoryCode,
			@PAG_AssetGroupCode,
			@XFY_FiscalYearCode,
			--@XF_InterestPayableFrequencyCode,
			@XF_CompoundInterestFrequencyCode,
			@CPGNP_InterestRate,
			@CPGNP_OrganizationName,
			@CPGNP_DepositAmount,
			@CPGNP_CurrentValue,
			@XIB_InterestBasisCode,
			@CPGNP_IsInterestAccumalated,
			@CPGNP_InterestAmtAccumalated,
			@CPGNP_InterestAmtPaidOut,
			@CPGNP_EmployeeContri,
			@CPGNP_EmployerContri,
			@CPGNP_Remark,
			@CPGNP_CreatedBy,
			CURRENT_TIMESTAMP,
			@CPGNP_ModifiedBy,
			CURRENT_TIMESTAMP
		)
	END
	ELSE
	BEGIN
		
		INSERT INTO CustomerPensionandGratuitiesNetPosition
		(
			CPGA_AccountId,
			PAIC_AssetInstrumentCategoryCode,
			PAG_AssetGroupCode,
			XFY_FiscalYearCode,
			XF_InterestPayableFrequencyCode,
			--XF_CompoundInterestFrequencyCode,
			CPGNP_InterestRate,
			CPGNP_OrganizationName,
			CPGNP_DepositAmount,
			CPGNP_CurrentValue,
			XIB_InterestBasisCode,
			CPGNP_IsInterestAccumalated,
			CPGNP_InterestAmtAccumalated,
			CPGNP_InterestAmtPaidOut,
			CPGNP_EmployeeContri,
			CPGNP_EmployerContri,
			CPGNP_Remark,
			CPGNP_CreatedBy,
			CPGNP_CreatedOn,
			CPGNP_ModifiedBy,
			CPGNP_ModifiedOn
		)
		Values
		(
			@CPGA_AccountId,
			@PAIC_AssetInstrumentCategoryCode,
			@PAG_AssetGroupCode,
			@XFY_FiscalYearCode,
			@XF_InterestPayableFrequencyCode,
			--@XF_CompoundInterestFrequencyCode,
			@CPGNP_InterestRate,
			@CPGNP_OrganizationName,
			@CPGNP_DepositAmount,
			@CPGNP_CurrentValue,
			@XIB_InterestBasisCode,
			@CPGNP_IsInterestAccumalated,
			@CPGNP_InterestAmtAccumalated,
			@CPGNP_InterestAmtPaidOut,
			@CPGNP_EmployeeContri,
			@CPGNP_EmployerContri,
			@CPGNP_Remark,
			@CPGNP_CreatedBy,
			CURRENT_TIMESTAMP,
			@CPGNP_ModifiedBy,
			CURRENT_TIMESTAMP
		)
	END
END




' 
END
GO
