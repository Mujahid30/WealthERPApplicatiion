
ALTER PROCEDURE [dbo].[SP_UpdateCamsProfileStagingIsCustomerNew]
@adviserId INT,
@processId INT
AS
    update CustomerMFCAMSXtrnlProfileStaging
     set CMGCXPS_IsCustomerNew = 0,C_CustomerId = c.C_CustomerId,CP_PortfolioId = cp.CP_PortfolioId
     from CustomerMFCAMSXtrnlProfileStaging s,Customer c,CustomerPortfolio cp,AdviserRM ar
     where s.CMGCXPS_PAN_NO=c.C_PANNum and s.CMGCXPS_IsCustomerNew=1 
     and (ar.A_AdviserId=@adviserId and c.AR_RMID=ar.AR_RMId and c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio=1)
     AND s.ADUL_ProcessId = @processId
     

 