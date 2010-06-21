-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_Year]

AS

SELECT  
	DISTINCT  WTD_Year AS TradeYear
FROM
     WerpTradeDate ;
 