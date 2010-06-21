

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerMutualFundTransactions] 
(
@C_CustomerId INT,
@Flag VARCHAR(2)='N',
@CurrentPage INT =1
)
AS

BEGIN

DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
	
IF(@Flag='C')
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
	


ELSE IF (@Flag='N')
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

 