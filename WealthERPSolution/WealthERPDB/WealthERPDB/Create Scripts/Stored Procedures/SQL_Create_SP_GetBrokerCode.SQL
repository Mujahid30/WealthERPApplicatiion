/****** Object:  StoredProcedure [dbo].[SP_GetBrokerCode]    Script Date: 06/24/2009 11:58:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetBrokerCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetBrokerCode]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrokerCode]    Script Date: 06/24/2009 11:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetBrokerCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetBrokerCode

@CP_PortfolioId INT,
@CETA_TradeAccountNum VARCHAR(20)


AS


SELECT * FROM dbo.CustomerEquityTradeAccount

WHERE CP_PortfolioId=@CP_PortfolioId AND
	  CETA_TradeAccountNum=@CETA_TradeAccountNum
' 
END
GO
