-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerEQDPAccounts]
@CP_PortfolioId INT


AS

SELECT * FROM CustomerEquityDematAccount WHERE CP_PortfolioId=@CP_PortfolioId 