/****** Object:  StoredProcedure [dbo].[SP_GetULIPPlans]    Script Date: 06/24/2009 12:05:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULIPPlans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetULIPPlans]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetULIPPlans]    Script Date: 06/24/2009 12:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetULIPPlans]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetULIPPlans]
@XII_InsuranceIssuerCode VARCHAR(5)
AS
SELECT 
* 
FROM 
dbo.WerpULIPPlan 
WHERE 
XII_InsuranceIssuerCode=@XII_InsuranceIssuerCode
' 
END
GO
