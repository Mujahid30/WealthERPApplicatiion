
ALTER PROCEDURE [dbo].[SP_UpdateInsuranceULIPPlan]
@CIUP_ULIPPlanId INT,
@CIUP_AllocationPer NUMERIC(5,2),
@CIUP_Unit NUMERIC(10,3),
@CIUP_PurchasePrice NUMERIC(18,3),
@CIUP_PurchaseDate DATETIME,
@CIUP_ModifiedBy INT

AS

BEGIN
	
	UPDATE dbo.CustomerInsurabceULIPPlan
		SET
			CIUP_AllocationPer = @CIUP_AllocationPer,
			CIUP_Unit = @CIUP_Unit,
			CIUP_PurchasePrice = @CIUP_PurchasePrice,
			CIUP_PurchaseDate = @CIUP_PurchaseDate,
			CIUP_ModifiedBy = @CIUP_ModifiedBy
	WHERE
		CIUP_ULIPPlanId = @CIUP_ULIPPlanId
END 