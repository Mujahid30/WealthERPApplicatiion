/****** Object:  StoredProcedure [dbo].[SP_UpdateInsuranceULIPPlan]    Script Date: 06/24/2009 12:07:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateInsuranceULIPPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateInsuranceULIPPlan]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateInsuranceULIPPlan]    Script Date: 06/24/2009 12:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateInsuranceULIPPlan]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UpdateInsuranceULIPPlan]
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
END' 
END
GO
