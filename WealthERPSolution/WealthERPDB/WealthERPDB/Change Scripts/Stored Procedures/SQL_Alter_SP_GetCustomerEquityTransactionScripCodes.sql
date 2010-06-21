  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER procedure [dbo].[SP_GetCustomerEquityTransactionScripCodes]  
@C_CustomerId int,  
@CET_TradeDate datetime  
as
/* 
SELECT D.C_CustomerId,A.CETA_AccountId,A.PEM_ScripCode,C.PEM_CompanyName,C.PEM_Ticker  
FROM CustomerEquityTransaction A,CustomerEquityTradeAccount B,ProductEquityMaster C, CustomerPortfolio D  
WHERE A.CETA_AccountId=B.CETA_AccountId AND B.CP_PortfolioId=D.CP_PortfolioId AND A.PEM_ScripCode=C.PEM_ScripCode   
AND D.C_CustomerId=@C_CustomerId AND (A.CET_TradeDate<@CET_TradeDate OR A.CET_TradeDate=@CET_TradeDate)  
GROUP BY D.C_CustomerId,A.CETA_AccountId,A.PEM_ScripCode,C.PEM_CompanyName,C.PEM_Ticker  
*/
SELECT D.C_CustomerId,A.CETA_AccountId,A.PEM_ScripCode,C.PEM_CompanyName,C.PEM_Ticker  
FROM CustomerEquityTransaction A
INNER JOIN CustomerEquityTradeAccount B ON A.CETA_AccountId=B.CETA_AccountId
INNER JOIN ProductEquityMaster C ON A.PEM_ScripCode=C.PEM_ScripCode 
INNER JOIN CustomerPortfolio D  ON B.CP_PortfolioId=D.CP_PortfolioId
WHERE
(
	D.C_CustomerId=@C_CustomerId AND 
   (A.CET_TradeDate <= @CET_TradeDate)  
)
GROUP BY D.C_CustomerId,A.CETA_AccountId,A.PEM_ScripCode,C.PEM_CompanyName,C.PEM_Ticker 
   