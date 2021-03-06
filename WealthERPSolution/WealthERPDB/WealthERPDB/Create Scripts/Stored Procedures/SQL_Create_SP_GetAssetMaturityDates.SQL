/****** Object:  StoredProcedure [dbo].[SP_GetAssetMaturityDates]    Script Date: 06/24/2009 11:58:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetMaturityDates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetMaturityDates]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetMaturityDates]    Script Date: 06/24/2009 11:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetMaturityDates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_GetAssetMaturityDates]  
@CP_PortfolioId INT  
  
AS  

/* 
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CPGNP_OrganizationName AS AssetParticulars, B.CPGNP_MaturityDate AS MaturityDate   
FROM ProductAssetGroup A, ViewPensionGratuities B  
WHERE CP_PortfolioId=@CP_PortfolioId and A.PAG_AssetGroupCode=B.PAG_AssetGroupCode  
  
UNION  
  
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CGSNP_Name AS AssetParticulars, B.CGSNP_MaturityDate AS MaturityDate  
FROM ProductAssetGroup A, ViewGovtSavingsNP B  
WHERE CP_PortfolioId=@CP_PortfolioId and A.PAG_AssetGroupCode=B.PAG_AssetGroupCode  
  
UNION  
  
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CFINP_Name AS AssetParticulars,B.CFINP_MaturityDate AS MaturityDate  
FROM ProductAssetGroup A, ViewFixedIncomeNP B  
WHERE CP_PortfolioId=@CP_PortfolioId and A.PAG_AssetGroupCode=B.PAG_AssetGroupCode
*/
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CPGNP_OrganizationName AS AssetParticulars, B.CPGNP_MaturityDate AS MaturityDate   
FROM ProductAssetGroup A
Inner Join ViewPensionGratuities B  ON A.PAG_AssetGroupCode=B.PAG_AssetGroupCode 
WHERE CP_PortfolioId=@CP_PortfolioId  
  
UNION  
  
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CGSNP_Name AS AssetParticulars, B.CGSNP_MaturityDate AS MaturityDate  
FROM ProductAssetGroup A
Inner Join ViewGovtSavingsNP B  ON A.PAG_AssetGroupCode=B.PAG_AssetGroupCode
WHERE CP_PortfolioId=@CP_PortfolioId   
  
UNION  
  
SELECT A.PAG_AssetGroupName AS AssetGroup, B.CFINP_Name AS AssetParticulars,B.CFINP_MaturityDate AS MaturityDate  
FROM ProductAssetGroup A
Inner Join ViewFixedIncomeNP B  ON A.PAG_AssetGroupCode=B.PAG_AssetGroupCode
WHERE CP_PortfolioId=@CP_PortfolioId ' 
END
GO
