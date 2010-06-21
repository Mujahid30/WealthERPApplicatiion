-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetMFAggreateValue

@A_AdviserId int

as


select  sum(CMFNP_MarketPrice * CMFNP_NetHoldings) from CustomerMutualFundNetPosition where CMFA_AccountId in 
(Select CMFA_AccountId from CustomerMutualFundAccount where CP_PortfolioId=
(Select CP_PortfolioId from CustomerPortfolio where C_CustomerId=
(Select C_CustomerId from Customer where AR_RMId=
(select AR_RMId from AdviserRMBranch where AB_BranchId =
(Select AB_BranchId from AdviserBranch where A_AdviserId=@A_AdviserId))))) 