  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SP_GetCustomerEquitySpecificTransactions]   
@C_CustomerId int,  
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
        
FROM CustomerEquityTransaction A
Inner Join CustomerEquityTradeAccount C ON A.CETA_AccountId=C.CETA_AccountId 
Inner Join CustomerPortfolio B ON C.CP_PortfolioId=B.CP_PortfolioId 
Inner Join WerpEquityTransactionType D ON A.WETT_TransactionCode=D.WETT_TransactionCode
Inner Join ProductEquityMaster E ON A.PEM_ScripCode=E.PEM_ScripCode  

Where
	C_CustomerId=@C_CustomerId AND A.PEM_ScripCode=@PEM_ScripCode  
	AND (A.CET_TradeDate<@CET_TradeDate OR A.CET_TradeDate=@CET_TradeDate)  
   