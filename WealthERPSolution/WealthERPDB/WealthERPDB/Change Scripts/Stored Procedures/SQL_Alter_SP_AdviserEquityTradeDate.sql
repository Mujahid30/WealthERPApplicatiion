-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE SP_AdviserEquityTradeDate  
  
@A_AdviserId INT  
  
AS  

SET NOCOUNT ON
SELECT DISTINCT   
                      CONVERT(varchar(10), dbo.CustomerEquityTransaction.CET_TradeDate, 101) AS Expr1, dbo.CustomerEquityTransaction.CETA_AccountId,   
                      dbo.CustomerEquityTradeAccount.CP_PortfolioId, dbo.Adviser.A_AdviserId  
FROM         dbo.Adviser INNER JOIN  
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN  
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId INNER JOIN  
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN  
                      dbo.CustomerEquityTradeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerEquityTradeAccount.CP_PortfolioId INNER JOIN  
                      dbo.CustomerEquityTransaction ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityTransaction.CETA_AccountId  
                        
                        
                       
  
--SELECT     
--   CONVERT(VARCHAR(10),dbo.CustomerEquityTransaction.CET_TradeDate,1) AS TradeDate,   
--    --CAST(dbo.CustomerEquityTransaction.CET_TradeDate AS )( )  AS TradeDate,   
--    dbo.CustomerEquityTransaction.CETA_AccountId,  
--       dbo.CustomerEquityTradeAccount.CP_PortfolioId,  
--       dbo.Adviser.A_AdviserId  
--FROM         
--    dbo.Adviser   
--   INNER JOIN   
--          dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId   
--   INNER JOIN  
--          dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId   
--   INNER JOIN  
--          dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId   
--   INNER JOIN  
--          dbo.CustomerEquityTradeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerEquityTradeAccount.CP_PortfolioId  
--            INNER JOIN  
--          dbo.CustomerEquityTransaction ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityTransaction.CETA_AccountId  
            
--          WHERE dbo.Adviser.A_AdviserId=@A_AdviserId  
            
            
--          EXEC SP_AdviserEquityDailyValuationTradeDate 1004  
-- @A_AdviserId = 0 -- INT  
  
SET NOCOUNT OFF        
            
           
  
             