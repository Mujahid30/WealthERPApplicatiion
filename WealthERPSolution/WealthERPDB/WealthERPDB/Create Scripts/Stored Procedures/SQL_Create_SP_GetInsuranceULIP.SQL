/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceULIP]    Script Date: 06/24/2009 12:03:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceULIP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInsuranceULIP]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceULIP]    Script Date: 06/24/2009 12:03:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceULIP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetInsuranceULIP]
@CIUP_ULIPPlanId INT
AS
SELECT * FROM CustomerInsurabceULIPPlan WHERE @CIUP_ULIPPlanId=CIUP_ULIPPlanId' 
END
GO
