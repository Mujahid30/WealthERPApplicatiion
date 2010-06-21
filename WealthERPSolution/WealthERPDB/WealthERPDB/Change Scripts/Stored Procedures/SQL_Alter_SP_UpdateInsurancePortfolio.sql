
ALTER PROCEDURE [dbo].[SP_UpdateInsurancePortfolio]

@CINP_InsuranceNPId INT,
@XII_InsuranceIssuerCode	VARCHAR(5),
@XF_PremiumFrequencyCode	VARCHAR(5),
@CINP_Name	VARCHAR(50),
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
@CINP_ModifiedBy	INT,
@CINP_ApplicationNum	VARCHAR(20),
@CINP_ApplicationDate	DATETIME,
@CINP_PremiumPaymentDate	DATETIME

AS

UPDATE dbo.CustomerInsuranceNetPosition
SET
	XII_InsuranceIssuerCode= @XII_InsuranceIssuerCode,
	XF_PremiumFrequencyCode= @XF_PremiumFrequencyCode,
	CINP_Name=@CINP_Name,
	CINP_PremiumAmount=@CINP_PremiumAmount,
	CINP_PremiumDuration=@CINP_PremiumDuration,
	CINP_SumAssured=@CINP_SumAssured,
	CINP_StartDate=@CINP_StartDate,
	CINP_PolicyPeriod=@CINP_PolicyPeriod,
	CINP_PremiumAccumalated=@CINP_PremiumAccumalated,
	CINP_BonusAccumalated=@CINP_BonusAccumalated,
	CINP_PolicyEpisode=@CINP_PolicyEpisode,
	CINP_SurrenderValue=@CINP_SurrenderValue,
	CINP_MaturityValue=@CINP_MaturityValue,
	CINP_EndDate=@CINP_EndDate,
	CINP_Remark=@CINP_Remark,
	CINP_ULIPCharges=@CINP_ULIPCharges,
	CINP_GracePeriod	=@CINP_GracePeriod,
	CINP_PremiumPaymentDate=@CINP_PremiumPaymentDate,
	CINP_ApplicationNum=@CINP_ApplicationNum,
	CINP_ApplicationDate=@CINP_ApplicationDate,
	CINP_ModifiedBy=@CINP_ModifiedBy,
	CINP_ModifiedOn=CURRENT_TIMESTAMP
WHERE
	CINP_InsuranceNPId = @CINP_InsuranceNPId 