/****** Object:  StoredProcedure [dbo].[SP_DeleteMutualFundNP]    Script Date: 06/24/2009 11:15:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMutualFundNP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteMutualFundNP]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteMutualFundNP]    Script Date: 06/24/2009 11:15:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMutualFundNP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SP_DeleteMutualFundNP]
@C_CustomerId INT,
@CMFNP_ValuationDate datetime
AS
Delete A from CustomerMutualFundNetPosition A,CustomerMutualFundAccount B,CustomerPortfolio C
  where A.CMFA_AccountId=B.CMFA_AccountId and B.CP_PortfolioId=C.CP_PortfolioId
  and C.C_CustomerId=@C_CustomerId and A.CMFNP_ValuationDate=@CMFNP_ValuationDate

' 
END
GO
