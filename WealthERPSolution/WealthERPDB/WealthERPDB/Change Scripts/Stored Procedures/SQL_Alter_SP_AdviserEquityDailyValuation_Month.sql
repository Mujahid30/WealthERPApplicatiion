-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_Month]

@WTD_Year INT

AS

SELECT  
	--DISTINCT MONTH (ToDate( FormatNumber([WTD_Month];"00") ;"MM")) 
	 --CAST(MONTH(WTD_Month) AS VARCHAR(10))  AS TradeMonth
	DISTINCT CONVERT(VARCHAR(3),WTD_Date,100) AS TradeMonth,
	WTD_Month
FROM
     WerpTradeDate 
WHERE WTD_Year=@WTD_Year
GROUP BY 
WTD_Date,
	WTD_Month
	
ORDER BY WTD_Month ASC;


--exec  [dbo].[SP_AdviserEquityDailyValuation_Month] 2008

 