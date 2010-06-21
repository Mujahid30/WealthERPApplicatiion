-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetInsuranceAggregateValue
@A_AdviserId INT

AS


SELECT SUM(CINP_PremiumAccumalated) FROM dbo.CustomerInsuranceNetPosition WHERE CIA_AccountId IN  
(SELECT cia_accountId FROM dbo.CustomerInsuranceAccount WHERE CP_PortfolioId = 
(SELECT CP_PortfolioId FROM dbo.CustomerPortfolio WHERE C_CustomerId=
(SELECT c_customerId  FROM dbo.Customer WHERE AR_RMId =
(SELECT AR_RMId FROM AdviserRMBranch WHERE ab_branchId=
(SELECT ab_branchId FROM dbo.AdviserBranch WHERE A_AdviserId=@A_AdviserId))))) 