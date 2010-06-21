


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetCustomerPortfolioEquitySpeculativeAveragePrice]
@C_CustomerId int,
@CP_PortfolioId int,
@PEM_ScripCode int,
@CET_TradeDate datetime
as
select C.C_CustomerId,A.PEM_ScripCode,AVG(A.CET_Rate+A.CET_Brokerage+A.CET_ServiceTax+A.CET_EducationCess+A.CET_STT+A.CET_OtherCharges) AvgPrice
from CustomerEquityTransaction A,CustomerEquityTradeAccount B,CustomerPortfolio C
where  A.CETA_AccountId=B.CETA_AccountId and B.CP_PortfolioId=C.CP_PortfolioId
and C.C_CustomerId=@C_CustomerId and B.CP_PortfolioId=@CP_PortfolioId 
and A.PEM_ScripCode=@PEM_ScripCode and A.CET_TradeDate=@CET_TradeDate
and CET_IsSpeculative=1 and CET_BuySell='B'
group by C_CustomerId,PEM_ScripCode



 