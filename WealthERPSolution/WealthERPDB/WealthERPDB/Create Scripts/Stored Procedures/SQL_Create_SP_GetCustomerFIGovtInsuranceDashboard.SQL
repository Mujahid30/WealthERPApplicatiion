/****** Object:  StoredProcedure [dbo].[SP_GetCustomerFIGovtInsuranceDashboard]    Script Date: 06/24/2009 11:59:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerFIGovtInsuranceDashboard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerFIGovtInsuranceDashboard]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerFIGovtInsuranceDashboard]    Script Date: 06/24/2009 11:59:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerFIGovtInsuranceDashboard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SP_GetCustomerFIGovtInsuranceDashboard]  
@PortfolioId INT  
  
AS  
  
SET NOCOUNT ON
   
 /* Retrieving Top 2 Fixed Income Assets */  
 SELECT  
  TOP 2  
  PAG_AssetGroupName AS AssetType,  
  CP_PortfolioId AS PortfolioId,  
  CFINP_Name AS AssetParticulars,  
  ISNULL(CAST(CFINP_PurchaseValue AS INT), 0) AS PurchaseCost,  
  ISNULL(CAST(CFINP_CurrentValue AS INT), 0) AS CurrentValue  
 FROM  
  ViewFixedIncomeNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  
 UNION  
 /* Retrieving Top 2 Govt Savings Assets */   
 SELECT  
  TOP 2  
  PAG_AssetGroupName AS AssetType,  
  CP_PortfolioId AS PortfolioId,  
  CGSNP_Name AS AssetParticulars,  
  0 AS PurchaseCost,  
  ISNULL(CAST(CGSNP_CurrentValue AS INT), 0) AS CurrentValue  
 FROM  
  ViewGovtSavingsNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
    
 UNION  
 /* Retrieving Top 2 Insurance Assets */   
 SELECT  
  TOP 2  
  PAG_AssetGroupName AS AssetType,  
  CP_PortfolioId AS PortfolioId,  
  CINP_Name AS AssetParticulars,  
  0 AS PurchaseCost,  
  ISNULL(CAST(CINP_SurrenderValue AS INT), 0) AS CurrentValue  
 FROM  
  ViewInsuranceNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  


SET NOCOUNT OFF 
' 
END
GO
GRANT EXECUTE ON [dbo].[SP_GetCustomerFIGovtInsuranceDashboard] TO [public] AS [dbo]
GO
