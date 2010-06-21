


ALTER PROCEDURE [dbo].[SP_DeleteEquityNP]
@C_CustomerId INT,
@CENP_ValuationDate datetime
AS
Delete A from CustomerEquityNetPosition A,CustomerEquityTradeAccount B,CustomerPortfolio C
  where A.CETA_AccountId=B.CETA_AccountId and B.CP_PortfolioId=C.CP_PortfolioId
  and C.C_CustomerId=@C_CustomerId and A.CENP_ValuationDate=@CENP_ValuationDate


 