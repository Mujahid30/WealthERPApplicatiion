/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquityTransactionScripCodes]    Script Date: 06/24/2009 11:59:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquityTransactionScripCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerEquityTransactionScripCodes]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquityTransactionScripCodes]    Script Date: 06/24/2009 11:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquityTransactionScripCodes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE procedure [dbo].[SP_GetCustomerEquityTransactionScripCodes]  
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
  ' 
END
GO
