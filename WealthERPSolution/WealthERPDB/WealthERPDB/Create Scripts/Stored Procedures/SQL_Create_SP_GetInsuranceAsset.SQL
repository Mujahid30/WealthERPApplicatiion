/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceAsset]    Script Date: 06/24/2009 12:03:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInsuranceAsset]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceAsset]    Script Date: 06/24/2009 12:03:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceAsset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetInsuranceAsset]
@CINP_InsuranceNPId INT

AS

BEGIN
	
	SELECT 
		* 
	FROM 
		CustomerInsuranceNetPosition
	WHERE 
		CINP_InsuranceNPId = @CINP_InsuranceNPId
		
END' 
END
GO
