
ALTER PROCEDURE [dbo].[SP_CreateMoneyBackEpisode]

@CIMBE_RepaymentDate	DATETIME,
@CIMBE_RepaidPer	numeric(5, 2),
@CINP_InsuranceNPId INT,
@CIMBE_CreatedBy	INT,
@CIMBE_ModifiedBy	INT

AS

INSERT INTO CustomerInsuranceMoneyBackEpisodes
(
	CIMBE_RepaymentDate,
	CIMBE_RepaidPer,
	CINP_InsuranceNPId,
	CIMBE_CreatedBy,
	CIMBE_ModifiedBy,
	CIMBE_CreatedOn,
	CIMBE_ModifiedOn
)
VALUES (
	@CIMBE_RepaymentDate,
	@CIMBE_RepaidPer,
	@CINP_InsuranceNPId,
	@CIMBE_CreatedBy,
	@CIMBE_ModifiedBy,
	CURRENT_TIMESTAMP,
	CURRENT_TIMESTAMP
) 