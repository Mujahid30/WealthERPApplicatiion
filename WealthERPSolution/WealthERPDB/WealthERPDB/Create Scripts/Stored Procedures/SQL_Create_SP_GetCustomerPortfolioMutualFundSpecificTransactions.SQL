/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions]    Script Date: 06/24/2009 12:01:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions]    Script Date: 06/24/2009 12:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerPortfolioMutualFundSpecificTransactions] 
@C_CustomerId int,
@CP_PortfolioId int,
@PASP_SchemePlanCode int,
@CMFA_AccountId int,
@CMFT_TransactionDate datetime
AS

SELECT A.CMFT_MFTransId
      ,C.C_CustomerId
      ,B.CP_PortfolioId
      ,A.CMFA_AccountId
      ,A.PASP_SchemePlanCode
      ,E.PASP_SchemePlanName
      ,A.CMFT_TransactionDate
      ,A.CMFT_BuySell
      ,A.CMFT_DividendRate
      ,A.CMFT_NAV
      ,A.CMFT_Price
      ,A.CMFT_Amount
      ,A.CMFT_Units
      ,A.CMFT_STT
      ,A.CMFT_IsSourceManual
      ,A.XES_SourceCode
      ,A.CMFT_SwitchSourceTrxId
      ,A.WMTT_TransactionClassificationCode
      ,D.WMTT_TransactionClassificationName
      ,D.WMTT_Trigger
      ,D.WMTT_FinancialFlag
      
  FROM 
  CustomerMutualFundTransaction A,CustomerMutualFundAccount B,
  CustomerPortfolio C,WerpMutualFundTransactionType D,ProductAMCSchemePlan E 
  WHERE A.CMFA_AccountId=B.CMFA_AccountId AND B.CP_PortfolioId=C.CP_PortfolioId
  AND A.WMTT_TransactionClassificationCode=D.WMTT_TransactionClassificationCode
  AND A.PASP_SchemePlanCode=E.PASP_SchemePlanCode
  AND C.C_CustomerId=@C_CustomerId AND B.CP_PortfolioId=@CP_PortfolioId
  AND A.CMFA_AccountId=@CMFA_AccountId
  AND A.PASP_SchemePlanCode=@PASP_SchemePlanCode
  AND (A.CMFT_TransactionDate<@CMFT_TransactionDate OR A.CMFT_TransactionDate=@CMFT_TransactionDate)
  


' 
END
GO
