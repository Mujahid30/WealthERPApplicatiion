/****** Object:  StoredProcedure [dbo].[SP_AdviserEquityDailyValuation_TradeDate]    Script Date: 06/24/2009 10:59:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserEquityDailyValuation_TradeDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_TradeDate]
GO
/****** Object:  StoredProcedure [dbo].[SP_AdviserEquityDailyValuation_TradeDate]    Script Date: 06/24/2009 10:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserEquityDailyValuation_TradeDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_TradeDate]

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
' 
END
GO
