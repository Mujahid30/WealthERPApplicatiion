/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioEQInvstDashboard]    Script Date: 06/24/2009 12:00:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioEQInvstDashboard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPortfolioEQInvstDashboard]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioEQInvstDashboard]    Script Date: 06/24/2009 12:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioEQInvstDashboard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetCustomerPortfolioEQInvstDashboard]    
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
        A_AdviserId = @A_AdviserId  AND ADEL_AssetGroup=''EQ''  
       order by ADEL_ProcessDate desc  
       )    
   ORDER BY CurrentValue      
     
SET NOCOUNT OFF   
  
--EXEC dbo.SP_GetCustomerPortfolioEQInvstDashboard 1632,1004  
-- @PortfolioId = 0, -- INT  
-- @A_AdviserId = 0 -- INT  
  ' 
END
GO
GRANT EXECUTE ON [dbo].[SP_GetCustomerPortfolioEQInvstDashboard] TO [public] AS [dbo]
GO
