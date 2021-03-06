/****** Object:  View [dbo].[ProductAMCNonMappedWERP]    Script Date: 06/24/2009 14:45:05 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductAMCNonMappedWERP]'))
DROP VIEW [dbo].[ProductAMCNonMappedWERP]
GO
/****** Object:  View [dbo].[ProductAMCNonMappedWERP]    Script Date: 06/24/2009 14:45:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductAMCNonMappedWERP]'))
EXEC dbo.sp_executesql @statement = N'
CREATE view [dbo].[ProductAMCNonMappedWERP]
as 
  select pamcp.PASP_SchemePlanCode SchemePlanCode,pamcp.PASP_SchemePlanName SchemePlan
  From ProductAMCSchemePlan as pamcp 
  left outer join ProductAMCSchemeMapping as pamcm 
  on pamcm.PASP_SchemePlanCode = pamcp.PASP_SchemePlanCode 
  where pamcm.PASP_SchemePlanCode is null
' 
GO
