/****** Object:  StoredProcedure [dbo].[SP_CreateInsurancePortfolio]    Script Date: 06/24/2009 11:13:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsurancePortfolio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateInsurancePortfolio]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateInsurancePortfolio]    Script Date: 06/24/2009 11:13:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsurancePortfolio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_CreateInsurancePortfolio]

@PAIC_AssetInstrumentCategoryCode	VARCHAR(6),
@PAG_AssetGroupCode	VARCHAR(4),
@CIA_AccountId	INT,
@XII_InsuranceIssuerCode	VARCHAR(5),
@XF_PremiumFrequencyCode	VARCHAR(5),
@CINP_Name	varchar(50),
@CINP_PremiumAmount	numeric(18, 3),
@CINP_PremiumDuration	numeric(5, 0),
@CINP_SumAssured	numeric(18, 3),
@CINP_StartDate	DATETIME,
@CINP_PolicyPeriod	numeric(5, 0),
@CINP_PremiumAccumalated	numeric(18, 3),
@CINP_BonusAccumalated	numeric(18, 3),
@CINP_PolicyEpisode	numeric(5, 0),
@CINP_SurrenderValue	numeric(18, 3),
@CINP_MaturityValue	numeric(18, 3),
@CINP_EndDate	DATETIME,
@CINP_Remark	varchar(100),
@CINP_ULIPCharges	numeric(18, 3),
@CINP_GracePeriod	numeric(5, 0),
@CINP_CreatedBy	INT,
@CINP_ModifiedBy	INT,
@CINP_ApplicationNum	varchar(20),
@CINP_ApplicationDate	DATETIME,
@CINP_PremiumPaymentDate	DATETIME,
@InsuranceId int output

AS

BEGIN
	
	INSERT INTO dbo.CustomerInsuranceNetPosition (
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	CIA_AccountId,
	XII_InsuranceIssuerCode,
	XF_PremiumFrequencyCode,
	CINP_Name,
	CINP_PremiumAmount,
	CINP_PremiumDuration,
	CINP_SumAssured,
	CINP_StartDate,
	CINP_PolicyPeriod,
	CINP_PremiumAccumalated,
	CINP_BonusAccumalated,
	CINP_PolicyEpisode,
	CINP_SurrenderValue,
	CINP_MaturityValue,
	CINP_EndDate,
	CINP_Remark	,
	CINP_ULIPCharges,
	CINP_GracePeriod,
	CINP_PremiumPaymentDate	,
	CINP_ApplicationNum,
	CINP_ApplicationDate,
	CINP_CreatedOn,
	CINP_CreatedBy,
	CINP_ModifiedBy,
	CINP_ModifiedOn
) VALUES ( 
	@PAIC_AssetInstrumentCategoryCode,
	@PAG_AssetGroupCode,
	@CIA_AccountId,
	@XII_InsuranceIssuerCode,
	@XF_PremiumFrequencyCode,
	@CINP_Name,
	@CINP_PremiumAmount,
	@CINP_PremiumDuration,
	@CINP_SumAssured,
	@CINP_StartDate,
	@CINP_PolicyPeriod,
	@CINP_PremiumAccumalated,
	@CINP_BonusAccumalated,
	@CINP_PolicyEpisode,
	@CINP_SurrenderValue,
	@CINP_MaturityValue,
	@CINP_EndDate,
	@CINP_Remark,
	@CINP_GracePeriod,
	@CINP_ULIPCharges,
	@CINP_PremiumPaymentDate,
	@CINP_ApplicationNum,
	@CINP_ApplicationDate,
	CURRENT_TIMESTAMP,
	@CINP_CreatedBy,
	@CINP_ModifiedBy,
	CURRENT_TIMESTAMP ) 

	SELECT @InsuranceId=SCOPE_IDENTITY()
	
	
END


' 
END
GO
