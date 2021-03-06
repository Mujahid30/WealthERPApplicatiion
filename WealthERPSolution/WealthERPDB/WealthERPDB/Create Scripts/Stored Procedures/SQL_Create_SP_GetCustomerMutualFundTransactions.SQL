/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMutualFundTransactions]    Script Date: 06/24/2009 12:00:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMutualFundTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerMutualFundTransactions]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMutualFundTransactions]    Script Date: 06/24/2009 12:00:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMutualFundTransactions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerMutualFundTransactions] 
(
@C_CustomerId INT,
@Flag VARCHAR(2)=''N'',
@CurrentPage INT =1
)
AS

BEGIN

DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
	
IF(@Flag=''C'')
	BEGIN
		SELECT 
			COUNT(*) 
		FROM  
			  CustomerMutualFundTransaction A,CustomerMutualFundAccount B,
			  CustomerPortfolio C,WerpMutualFundTransactionType D,ProductAMCSchemePlan E 
			  WHERE A.CMFA_AccountId=B.CMFA_AccountId AND B.CP_PortfolioId=C.CP_PortfolioId
			  AND A.WMTT_TransactionClassificationCode=D.WMTT_TransactionClassificationCode
			  AND A.PASP_SchemePlanCode=E.PASP_SchemePlanCode
			  AND C.C_CustomerId=@C_CustomerId
	END
	


ELSE IF (@Flag=''N'')
BEGIN

WITH Entries AS (

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
      ,D.WMTT_FinancialFlag,
      ROW_NUMBER () OVER (ORDER BY E.PASP_SchemePlanName) AS RowNum  
  FROM 
  CustomerMutualFundTransaction A,CustomerMutualFundAccount B,
  CustomerPortfolio C,WerpMutualFundTransactionType D,ProductAMCSchemePlan E 
  WHERE A.CMFA_AccountId=B.CMFA_AccountId AND B.CP_PortfolioId=C.CP_PortfolioId
  AND A.WMTT_TransactionClassificationCode=D.WMTT_TransactionClassificationCode
  AND A.PASP_SchemePlanCode=E.PASP_SchemePlanCode
  AND C.C_CustomerId=@C_CustomerId
)

SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow
	
END
	
END

' 
END
GO
