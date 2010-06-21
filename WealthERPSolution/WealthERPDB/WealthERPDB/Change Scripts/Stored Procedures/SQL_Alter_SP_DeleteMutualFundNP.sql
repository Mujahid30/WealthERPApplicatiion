

ALTER PROCEDURE [dbo].[SP_DeleteMutualFundNP]
@C_CustomerId INT,
@CMFNP_ValuationDate datetime
AS
Delete A from CustomerMutualFundNetPosition A,CustomerMutualFundAccount B,CustomerPortfolio C
  where A.CMFA_AccountId=B.CMFA_AccountId and B.CP_PortfolioId=C.CP_PortfolioId
  and C.C_CustomerId=@C_CustomerId and A.CMFNP_ValuationDate=@CMFNP_ValuationDate

 