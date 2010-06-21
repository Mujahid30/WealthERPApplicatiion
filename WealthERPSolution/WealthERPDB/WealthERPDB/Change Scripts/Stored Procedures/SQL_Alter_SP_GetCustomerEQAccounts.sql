-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerEQAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode varchar(5)


AS

SELECT * FROM CustomerEquityTradeAccount WHERE CP_PortfolioId=@CP_PortfolioId AND PAG_AssetGroupCode=@PAG_AssetGroupCode 