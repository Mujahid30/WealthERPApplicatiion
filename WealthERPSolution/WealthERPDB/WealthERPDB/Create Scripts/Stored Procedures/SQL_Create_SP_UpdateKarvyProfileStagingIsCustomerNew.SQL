/****** Object:  StoredProcedure [dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]    Script Date: 06/24/2009 12:07:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]    Script Date: 06/24/2009 12:07:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]
@adviserId int
AS
    update CustomerMFKarvyXtrnlProfileStaging
     set CMFKXPS_IsCustomerNew = 0,C_CustomerId = c.C_CustomerId,CP_PortfolioId = cp.CP_PortfolioId
     from CustomerMFKarvyXtrnlProfileStaging s,Customer c,CustomerPortfolio cp ,AdviserRM ar
     where s.CMFKXPS_PANNumber=c.C_PANNum and s.CMFKXPS_IsCustomerNew=1 
     and (ar.A_AdviserId=@adviserId and c.AR_RMID=ar.AR_RMId and c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio=1)


' 
END
GO
