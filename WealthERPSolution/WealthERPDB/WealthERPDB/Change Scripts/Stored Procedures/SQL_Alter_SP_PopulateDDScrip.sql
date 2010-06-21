-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_PopulateDDScrip

@CP_PortfolioId int
 
AS

SELECT DISTINCT PEM_ScripCode FROM dbo.CustomerEquityTransaction 
INNER JOIN 
dbo.CustomerEquityTradeAccount
ON
dbo.CustomerEquityTransaction.CETA_AccountId = dbo.CustomerEquityTradeAccount.CETA_AccountId
WHERE CP_PortfolioId=@CP_PortfolioId 