
ALTER PROCEDURE [dbo].[SP_CreateInsuranceULIPPlan]
@CINP_InsuranceNPId	INT,
@WUSP_ULIPSubPlanCode INT,
@CIUP_AllocationPer	NUMERIC(5, 2),
@CIUP_Unit	NUMERIC(10, 3),
@CIUP_PurchasePrice	NUMERIC(18, 3),
@CIUP_CreatedBy	INT,
@CIUP_ModifiedBy INT
AS
INSERT INTO CustomerInsurabceULIPPlan 
(
	CINP_InsuranceNPId,
	WUSP_ULIPSubPlanCode,
	CIUP_AllocationPer,
	CIUP_Unit,
	CIUP_PurchasePrice,
	CIUP_CreatedBy,
	CIUP_CreatedOn,
	CIUP_ModifiedBy,
	CIUP_ModifiedOn
)
VALUES 
(
	@CINP_InsuranceNPId,
	@WUSP_ULIPSubPlanCode,
	@CIUP_AllocationPer,
	@CIUP_Unit,
	@CIUP_PurchasePrice,
	@CIUP_CreatedBy,
	CURRENT_TIMESTAMP,
	@CIUP_ModifiedBy,
	CURRENT_TIMESTAMP
) 