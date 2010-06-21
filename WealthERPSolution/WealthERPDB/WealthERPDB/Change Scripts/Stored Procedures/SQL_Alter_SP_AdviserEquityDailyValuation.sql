-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_AdviserEquityDailyValuation

@A_AdviserId INT

AS

SELECT     dbo.CustomerEquityTransaction.CETA_AccountId,
		   dbo.CustomerEquityTradeAccount.CP_PortfolioId,
		   dbo.CustomerEquityTransaction.CET_TradeDate, 
           dbo.Adviser.A_AdviserId
FROM       
		  dbo.Adviser 
			INNER JOIN 
          dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId 
			INNER JOIN
          dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId 
			INNER JOIN
          dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId 
			INNER JOIN
          dbo.CustomerEquityTradeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerEquityTradeAccount.CP_PortfolioId
            INNER JOIN
          dbo.CustomerEquityTransaction ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityTransaction.CETA_AccountId
          
          WHERE dbo.Adviser.A_AdviserId=@A_AdviserId
          
          
           