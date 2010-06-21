 -- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[SP_AdviserDailyValuationMFCustomerList]

@A_AdviserId INT

AS


SELECT
	 DISTINCT dbo.Customer.C_CustomerId AS CustomerId

FROM
      dbo.Adviser
      
INNER JOIN

      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId
INNER JOIN
      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId 
INNER JOIN
      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId 
INNER JOIN
      dbo.CustomerMutualFundAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerMutualFundAccount.CP_PortfolioId 
INNER JOIN
      dbo.CustomerMutualFundTransaction ON dbo.CustomerMutualFundAccount.CMFA_AccountId = dbo.CustomerMutualFundTransaction.CMFA_AccountId
      
WHERE  dbo.Adviser.A_AdviserId=@A_AdviserId







----EXEC dbo.SP_AdviserDailyValuationMFCustomerList 1004





