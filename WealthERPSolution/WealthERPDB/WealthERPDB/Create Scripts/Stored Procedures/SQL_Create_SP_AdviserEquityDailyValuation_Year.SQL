/****** Object:  StoredProcedure [dbo].[SP_AdviserEquityDailyValuation_Year]    Script Date: 06/24/2009 10:59:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserEquityDailyValuation_Year]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_Year]
GO
/****** Object:  StoredProcedure [dbo].[SP_AdviserEquityDailyValuation_Year]    Script Date: 06/24/2009 10:59:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserEquityDailyValuation_Year]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AdviserEquityDailyValuation_Year]

AS

SELECT  
	DISTINCT  WTD_Year AS TradeYear
FROM
     WerpTradeDate ;
' 
END
GO
