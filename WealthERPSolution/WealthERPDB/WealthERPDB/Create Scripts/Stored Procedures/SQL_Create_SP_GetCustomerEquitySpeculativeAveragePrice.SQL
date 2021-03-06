/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]    Script Date: 06/24/2009 11:59:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]    Script Date: 06/24/2009 11:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetCustomerEquitySpeculativeAveragePrice]
@C_CustomerId int,
@PEM_ScripCode int,
@CET_TradeDate datetime
as
select C.C_CustomerId,A.PEM_ScripCode,AVG(A.CET_Rate+A.CET_Brokerage+A.CET_ServiceTax+A.CET_EducationCess+A.CET_STT+A.CET_OtherCharges) AvgPrice
from CustomerEquityTransaction A,CustomerEquityTradeAccount B,CustomerPortfolio C
where  A.CETA_AccountId=B.CETA_AccountId and B.CP_PortfolioId=C.CP_PortfolioId
and C.C_CustomerId=@C_CustomerId and A.PEM_ScripCode=@PEM_ScripCode and A.CET_TradeDate=@CET_TradeDate
and CET_IsSpeculative=1 and CET_BuySell=''B''
group by C_CustomerId,PEM_ScripCode



' 
END
GO
