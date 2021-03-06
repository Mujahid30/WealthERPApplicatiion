/****** Object:  StoredProcedure [dbo].[SP_DeleteInsuranceULIPPlan]    Script Date: 06/24/2009 11:14:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteInsuranceULIPPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteInsuranceULIPPlan]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteInsuranceULIPPlan]    Script Date: 06/24/2009 11:14:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteInsuranceULIPPlan]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_DeleteInsuranceULIPPlan]
@CINP_InsuranceNPId INT

AS

BEGIN
	DELETE FROM CustomerInsuranceULIPPlan
	WHERE CINP_InsuranceNPId = @CINP_InsuranceNPId
END' 
END
GO
