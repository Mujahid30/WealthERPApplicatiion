/****** Object:  StoredProcedure [dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]    Script Date: 06/24/2009 12:07:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]    Script Date: 06/24/2009 12:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]
AS
   update CustomerEquityXtrnlProfileStaging
     set CEXPS_IsCustomerNew=0,C_CustomerId = c.C_CustomerId, CP_PortfolioId = cp.CP_PortfolioId
     from CustomerEquityXtrnlProfileStaging s,Customer c , CustomerPortfolio cp
     where s.CEXPS_PANNum=c.C_PANNum and s.CEXPS_IsCustomerNew=1
     and (c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio = 1)

' 
END
GO
