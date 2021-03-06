/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingTable]    Script Date: 06/24/2009 12:06:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCombinationStagingTable]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingTable]    Script Date: 06/24/2009 12:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingTable]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_UpdateCombinationStagingTable
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsCustomerNew=0,CMFKXCS_CustomerId = c.C_CustomerId ,CP_PortfolioId = cp.CP_PortfolioId
     from CustomerMFKarvyXtrnlCombinationStaging s,Customer c,CustomerPortfolio cp 
     where s.CMFKXCS_PANNumber=c.C_PANNum and s.CMFKXCS_IsCustomerNew=1 
     and (c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio=1)
' 
END
GO
