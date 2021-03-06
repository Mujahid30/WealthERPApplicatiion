/****** Object:  StoredProcedure [dbo].[SP_GetMFAggreateValue]    Script Date: 06/24/2009 12:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetMFAggreateValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetMFAggreateValue]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMFAggreateValue]    Script Date: 06/24/2009 12:03:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetMFAggreateValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetMFAggreateValue

@A_AdviserId int

as


select  sum(CMFNP_MarketPrice * CMFNP_NetHoldings) from CustomerMutualFundNetPosition where CMFA_AccountId in 
(Select CMFA_AccountId from CustomerMutualFundAccount where CP_PortfolioId=
(Select CP_PortfolioId from CustomerPortfolio where C_CustomerId=
(Select C_CustomerId from Customer where AR_RMId=
(select AR_RMId from AdviserRMBranch where AB_BranchId =
(Select AB_BranchId from AdviserBranch where A_AdviserId=@A_AdviserId)))))' 
END
GO
