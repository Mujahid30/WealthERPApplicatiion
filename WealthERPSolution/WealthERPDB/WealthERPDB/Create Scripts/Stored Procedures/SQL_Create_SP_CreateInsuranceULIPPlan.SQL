/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceULIPPlan]    Script Date: 06/24/2009 11:13:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceULIPPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateInsuranceULIPPlan]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceULIPPlan]    Script Date: 06/24/2009 11:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceULIPPlan]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_CreateInsuranceULIPPlan]
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
)' 
END
GO
