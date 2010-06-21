-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SP_GetAssetMaturityDates]  
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
WHERE CP_PortfolioId=@CP_PortfolioId  