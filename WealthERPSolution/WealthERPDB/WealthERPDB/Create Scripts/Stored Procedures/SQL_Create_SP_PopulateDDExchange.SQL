/****** Object:  StoredProcedure [dbo].[SP_PopulateDDExchange]    Script Date: 06/24/2009 12:05:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_PopulateDDExchange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_PopulateDDExchange]
GO
/****** Object:  StoredProcedure [dbo].[SP_PopulateDDExchange]    Script Date: 06/24/2009 12:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_PopulateDDExchange]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_PopulateDDExchange

@CP_PortfolioId INT

AS

SELECT
	 DISTINCT XE_ExchangeCode 
FROM  
	dbo.CustomerEquityTransaction 
INNER JOIN 
	dbo.CustomerEquityTradeAccount
ON 
	dbo.CustomerEquityTransaction.CETA_AccountId = dbo.CustomerEquityTradeAccount.CETA_AccountId
WHERE 
	CP_PortfolioId=@CP_PortfolioId' 
END
GO
