  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SP_GetCustomerPortfolioEquitySpecificTransactions]   
@C_CustomerId int,  
@CP_PortfolioId int,  
@PEM_ScripCode int,  
@CET_TradeDate datetime  
AS  
  
SELECT A.CET_EqTransId  
   ,B.C_CustomerId  
   ,C.CP_PortfolioId  
      ,A.CETA_AccountId  
      ,A.PEM_ScripCode  
      ,E.PEM_CompanyName  
      ,E.PEM_Ticker  
      ,A.CET_BuySell  
      ,A.CET_TradeNum  
      ,A.CET_OrderNum  
      ,A.CET_IsSpeculative  
      ,A.XE_ExchangeCode  
      ,A.CET_TradeDate  
      ,A.CET_Rate  
      ,A.CET_Quantity  
      ,A.CET_Brokerage  
      ,A.CET_ServiceTax  
      ,A.CET_EducationCess  
      ,A.CET_STT  
      ,A.CET_OtherCharges  
      ,A.CET_RateInclBrokerage  
      ,A.CET_TradeTotal  
      ,A.XB_BrokerCode  
      ,A.CET_IsSplit  
      ,A.CET_SplitCustEqTransId  
      ,A.XES_SourceCode  
      ,A.WETT_TransactionCode  
      ,D.WETT_TransactionTypeName  
      ,D.WETT_IsCorpAxn  
      ,A.CET_IsSourceManual  
  /*   
  FROM CustomerEquityTransaction A,  
  CustomerPortfolio B,  
  CustomerEquityTradeAccount C,  
  WerpEquityTransactionType D,  
  ProductEquityMaster E  
  WHERE A.CETA_AccountId=C.CETA_AccountId AND C.CP_PortfolioId=B.CP_PortfolioId  
  AND A.WETT_TransactionCode=D.WETT_TransactionCode AND A.PEM_ScripCode=E.PEM_ScripCode  
    
  AND B.C_CustomerId=@C_CustomerId AND B.CP_PortfolioId=@CP_PortfolioId   
  AND A.PEM_ScripCode=@PEM_ScripCode  
  AND (A.CET_TradeDate<@CET_TradeDate or A.CET_TradeDate=@CET_TradeDate)  
  */
FROM CustomerEquityTransaction A 
INNER JOIN CustomerEquityTradeAccount C ON A.CETA_AccountId=C.CETA_AccountId
INNER JOIN CustomerPortfolio B ON B.CP_PortfolioId = C.CP_PortfolioId
INNER JOIN WerpEquityTransactionType D ON A.WETT_TransactionCode=D.WETT_TransactionCode
INNER JOIN ProductEquityMaster E  ON A.PEM_ScripCode=E.PEM_ScripCode 
WHERE
(
	B.C_CustomerId=@C_CustomerId AND B.CP_PortfolioId=@CP_PortfolioId AND
	A.PEM_ScripCode=@PEM_ScripCode  AND
	(A.CET_TradeDate <= @CET_TradeDate)  
)
 