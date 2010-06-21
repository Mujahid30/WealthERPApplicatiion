-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_PopulateDDExchange

@CP_PortfolioId INT

AS

SELECT
	 DISTINCT XE_ExchangeCode 
FROM  
	dbo.CustomerEquityTransaction 
INNER JOIN 
	dbo.CustomerEquityTradeAccount
ON 
	dbo.CustomerEquityTransaction.CETA_AccountId = dbo.CustomerEquityTradeAccount.CETA_AccountId
WHERE 
	CP_PortfolioId=@CP_PortfolioId 