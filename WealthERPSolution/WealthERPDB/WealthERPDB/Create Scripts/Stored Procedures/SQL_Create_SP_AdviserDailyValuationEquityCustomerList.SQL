/****** Object:  StoredProcedure [dbo].[SP_AdviserDailyValuationEquityCustomerList]    Script Date: 06/24/2009 10:59:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserDailyValuationEquityCustomerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AdviserDailyValuationEquityCustomerList]
GO
/****** Object:  StoredProcedure [dbo].[SP_AdviserDailyValuationEquityCustomerList]    Script Date: 06/24/2009 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserDailyValuationEquityCustomerList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[SP_AdviserDailyValuationEquityCustomerList]

@A_AdviserId INT

AS

SELECT
	 
	DISTINCT  dbo.CustomerPortfolio.C_CustomerId AS CustomerId	
  -- dbo.Adviser.A_AdviserId
FROM    
  dbo.Customer 
INNER JOIN
   dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId 
INNER JOIN
   dbo.CustomerEquityTradeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerEquityTradeAccount.CP_PortfolioId 
INNER JOIN
   dbo.CustomerEquityTransaction ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityTransaction.CETA_AccountId 
INNER JOIN
   dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN
   dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId

		

WHERE dbo.Adviser.A_AdviserId=@A_AdviserId

--EXEC dbo.SP_AdviserDailyValuationEquityCustomerList 1004
--	@A_AdviserId = 0 -- INT




' 
END
GO
