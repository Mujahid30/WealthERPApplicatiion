/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMFTransactionSchemePlans]    Script Date: 06/24/2009 12:00:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMFTransactionSchemePlans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerMFTransactionSchemePlans]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMFTransactionSchemePlans]    Script Date: 06/24/2009 12:00:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMFTransactionSchemePlans]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetCustomerMFTransactionSchemePlans]
@C_CustomerId int,
@CMFT_TransactionDate datetime
AS
SELECT C.C_CustomerId,A.CMFA_AccountId,A.PASP_SchemePlanCode,D.PASP_SchemePlanName,B.CMFA_FolioNum 
FROM CustomerMutualFundTransaction A,CustomerMutualFundAccount B,CustomerPortfolio C,ProductAMCSchemePlan D 
WHERE A.CMFA_AccountId=B.CMFA_AccountId AND B.CP_PortfolioId=C.CP_PortfolioId 
AND A.PASP_SchemePlanCode=D.PASP_SchemePlanCode AND C.C_CustomerId=@C_CustomerId
AND (CMFT_TransactionDate<@CMFT_TransactionDate or CMFT_TransactionDate=@CMFT_TransactionDate)
GROUP BY C.C_CustomerId,A.CMFA_AccountId,A.PASP_SchemePlanCode,D.PASP_SchemePlanName,B.CMFA_FolioNum 
ORDER BY A.PASP_SchemePlanCode



' 
END
GO
