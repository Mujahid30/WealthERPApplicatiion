/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceULIPList]    Script Date: 06/24/2009 12:03:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceULIPList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInsuranceULIPList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceULIPList]    Script Date: 06/24/2009 12:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceULIPList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetInsuranceULIPList]
@CINP_InsuranceNPId INT
AS
SELECT 
	* 
FROM 
	CustomerInsurabceULIPPlan 
WHERE 
	CINP_InsuranceNPId = @CINP_InsuranceNPId
' 
END
GO
