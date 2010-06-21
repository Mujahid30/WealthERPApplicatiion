
ALTER PROCEDURE [dbo].[SP_GetCustomerPortfolioEQInvstDashboard]    
@PortfolioId INT,    
@A_AdviserId INT   
    
AS    
SET NOCOUNT ON    
     
 SELECT    
  TOP 5    
  PEM_CompanyName AS Script,    
  ISNULL((CENP_NetHoldings) , 0.00) AS NetHoldings,    
  ISNULL((CENP_NetHoldings * CENP_AveragePrice) , 0.00) AS AmortisedCost,    
  ISNULL((CENP_CurrentValue) , 0.00) AS CurrentValue    
 FROM    
  dbo.ViewEquityNP    
 WHERE    
  CP_PortfolioId = @PortfolioId  
  and  
 CENP_ValuationDate in (  
       select top 1(ADEL_ProcessDate)  
       FROM    
       dbo.AdviserDailyEODLog    
       WHERE    
        A_AdviserId = @A_AdviserId  AND ADEL_AssetGroup='EQ'  
       order by ADEL_ProcessDate desc  
       )    
   ORDER BY CurrentValue      
     
SET NOCOUNT OFF   
  
--EXEC dbo.SP_GetCustomerPortfolioEQInvstDashboard 1632,1004  
-- @PortfolioId = 0, -- INT  
-- @A_AdviserId = 0 -- INT  
   