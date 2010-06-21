
ALTER PROCEDURE [dbo].[SP_GetCustomerPortfolioMFInvstDashboard]  
@PortfolioId INT,  
@A_AdviserId INT 
  
AS  
SET NOCOUNT ON   

 --  
 -- Retrieving Data for the Grid  
 --   
   

 SELECT  
  TOP 5 
  PAIC_AssetInstrumentCategoryName AS SchemeType,  
  PASP_SchemePlanName AS Scheme,  
  ISNULL(CAST((CMFNP_NetHoldings * CMFNP_AveragePrice) AS INT), 0) AS AmortisedCost,  
  ISNULL(CAST((CMFNP_CurrentValue) AS INT), 0) AS CurrentValue  
 FROM  
  dbo.ViewMutualFundNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  AND
  CMFNP_ValuationDate in (
							select top 1(ADEL_ProcessDate)
							FROM  
							dbo.AdviserDailyEODLog  
							WHERE  
								A_AdviserId =@A_AdviserId AND ADEL_AssetGroup='MF'
							order by ADEL_ProcessDate desc
							)
   order by CurrentValue
   
    
 --  
 -- Retrieving Data for the Chart  
 --   
 /* Retrieve MF Equity Current Values */  
 SELECT  
  'Equity' AS MFType,  
  ISNULL(SUM(CMFNP_CurrentValue), 0.0) AS AggrCurrentValue  
 FROM  
  dbo.ViewMutualFundNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  AND  
  PAIC_AssetInstrumentCategoryCode = 'MFEQ'  
    
 UNION  
 /* Retrieve MF Hybrid Current Values */  
 SELECT  
  'Hybrid' AS MFType,  
  ISNULL(SUM(CMFNP_CurrentValue), 0.0) AS AggrCurrentValue  
 FROM  
  dbo.ViewMutualFundNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  AND  
  PAIC_AssetInstrumentCategoryCode = 'MFHY'  
   
 UNION  
 /* Retrieve MF Debt Current Values */  
 SELECT  
  'Debt' AS MFType,  
  ISNULL(SUM(CMFNP_CurrentValue), 0.0) AS AggrCurrentValue  
 FROM  
  dbo.ViewMutualFundNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  AND  
  PAIC_AssetInstrumentCategoryCode = 'MFDT'  
  
 UNION  
 /* Retrieve MF Other Current Values */  
 SELECT  
  'Other' AS MFType,  
  ISNULL(SUM(CMFNP_CurrentValue), 0.0) AS AggrCurrentValue  
 FROM  
  dbo.ViewMutualFundNP  
 WHERE  
  CP_PortfolioId = @PortfolioId  
  AND  
  PAIC_AssetInstrumentCategoryCode = 'MFOT' 

SET NOCOUNT OFF 


--EXEC [dbo].[SP_GetCustomerPortfolioMFInvstDashboard] 2272,1004
	 