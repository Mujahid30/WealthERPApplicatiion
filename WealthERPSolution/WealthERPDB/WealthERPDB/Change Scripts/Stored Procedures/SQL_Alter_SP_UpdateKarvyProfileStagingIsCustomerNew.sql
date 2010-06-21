

ALTER PROCEDURE [dbo].[SP_UpdateKarvyProfileStagingIsCustomerNew]
@adviserId INT,
@processId INT
AS
    update CustomerMFKarvyXtrnlProfileStaging
     set CMFKXPS_IsCustomerNew = 0,C_CustomerId = c.C_CustomerId,CP_PortfolioId = cp.CP_PortfolioId
     from CustomerMFKarvyXtrnlProfileStaging s,Customer c,CustomerPortfolio cp ,AdviserRM ar
     where s.CMFKXPS_PANNumber=c.C_PANNum and s.CMFKXPS_IsCustomerNew=1 
     and (ar.A_AdviserId=@adviserId and c.AR_RMID=ar.AR_RMId and c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio=1)
     AND s.ADUL_ProcessId = @processId


 