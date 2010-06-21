-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER  PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_TradeDate]

@WTD_Year INT,
@WTD_Month INT,
@A_AdviserId INT ,
@ADEL_AssetGroup VARCHAR(50)



AS

SELECT  
	DISTINCT  CONVERT(VARCHAR(13),WTD_Date,106)  AS TradeDay
FROM
     WerpTradeDate 
WHERE WTD_Year=@WTD_Year AND WTD_Month=@WTD_Month AND WTD_Date NOT IN 
	( SELECT ADEL_ProcessDate FROM AdviserDailyEODLog WHERE A_AdviserId=@A_AdviserId AND ADEL_AssetGroup=@ADEL_AssetGroup)
 