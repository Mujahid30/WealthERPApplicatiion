/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceAggregateValue]    Script Date: 06/24/2009 12:03:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceAggregateValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInsuranceAggregateValue]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInsuranceAggregateValue]    Script Date: 06/24/2009 12:03:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsuranceAggregateValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetInsuranceAggregateValue
@A_AdviserId INT

AS


SELECT SUM(CINP_PremiumAccumalated) FROM dbo.CustomerInsuranceNetPosition WHERE CIA_AccountId IN  
(SELECT cia_accountId FROM dbo.CustomerInsuranceAccount WHERE CP_PortfolioId = 
(SELECT CP_PortfolioId FROM dbo.CustomerPortfolio WHERE C_CustomerId=
(SELECT c_customerId  FROM dbo.Customer WHERE AR_RMId =
(SELECT AR_RMId FROM AdviserRMBranch WHERE ab_branchId=
(SELECT ab_branchId FROM dbo.AdviserBranch WHERE A_AdviserId=@A_AdviserId)))))' 
END
GO
