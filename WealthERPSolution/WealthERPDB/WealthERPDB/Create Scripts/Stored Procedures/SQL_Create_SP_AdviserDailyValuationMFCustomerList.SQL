/****** Object:  StoredProcedure [dbo].[SP_AdviserDailyValuationMFCustomerList]    Script Date: 06/24/2009 10:59:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserDailyValuationMFCustomerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AdviserDailyValuationMFCustomerList]
GO
/****** Object:  StoredProcedure [dbo].[SP_AdviserDailyValuationMFCustomerList]    Script Date: 06/24/2009 10:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AdviserDailyValuationMFCustomerList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[SP_AdviserDailyValuationMFCustomerList]

@A_AdviserId INT

AS


SELECT
	 DISTINCT dbo.Customer.C_CustomerId AS CustomerId

FROM
      dbo.Adviser
      
INNER JOIN

      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId
INNER JOIN
      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId 
INNER JOIN
      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId 
INNER JOIN
      dbo.CustomerMutualFundAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerMutualFundAccount.CP_PortfolioId 
INNER JOIN
      dbo.CustomerMutualFundTransaction ON dbo.CustomerMutualFundAccount.CMFA_AccountId = dbo.CustomerMutualFundTransaction.CMFA_AccountId
      
WHERE  dbo.Adviser.A_AdviserId=@A_AdviserId







----EXEC dbo.SP_AdviserDailyValuationMFCustomerList 1004





' 
END
GO
