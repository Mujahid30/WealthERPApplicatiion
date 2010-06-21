-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER  PROCEDURE [dbo].[SP_AdviserDailyValuationEquityCustomerList]

@A_AdviserId INT

AS

SELECT
	 
	DISTINCT  dbo.CustomerPortfolio.C_CustomerId AS CustomerId	
  -- dbo.Adviser.A_AdviserId
FROM    
  dbo.Customer 
INNER JOIN
   dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId 
INNER JOIN
   dbo.CustomerEquityTradeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerEquityTradeAccount.CP_PortfolioId 
INNER JOIN
   dbo.CustomerEquityTransaction ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityTransaction.CETA_AccountId 
INNER JOIN
   dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN
   dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId

		

WHERE dbo.Adviser.A_AdviserId=@A_AdviserId

--EXEC dbo.SP_AdviserDailyValuationEquityCustomerList 1004
--	@A_AdviserId = 0 -- INT




 