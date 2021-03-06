/****** Object:  View [dbo].[ProductAMCValueResearchNonMapped]    Script Date: 06/24/2009 14:45:08 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductAMCValueResearchNonMapped]'))
DROP VIEW [dbo].[ProductAMCValueResearchNonMapped]
GO
/****** Object:  View [dbo].[ProductAMCValueResearchNonMapped]    Script Date: 06/24/2009 14:45:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductAMCValueResearchNonMapped]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW ProductAMCValueResearchNonMapped 
AS  
SELECT  PASP_SchemePlanCode SchemePlanCode, PASP_SchemePlanName SchemePlan
FROM    ProductAMCSchemePlan
EXCEPT
SELECT   A.PASP_SchemePlanCode, A.PASP_SchemePlanName
FROM     ProductAMCSchemePlan A, ProductAMCSchemeMapping B
WHERE    A.PASP_SchemePlanCode = B.PASP_SchemePlanCode AND B.PASC_AMC_ExternalType = ''ValueResearch''' 
GO
