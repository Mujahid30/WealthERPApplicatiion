-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetBrokerCode

@CP_PortfolioId INT,
@CETA_TradeAccountNum VARCHAR(20)


AS


SELECT * FROM dbo.CustomerEquityTradeAccount

WHERE CP_PortfolioId=@CP_PortfolioId AND
	  CETA_TradeAccountNum=@CETA_TradeAccountNum
 