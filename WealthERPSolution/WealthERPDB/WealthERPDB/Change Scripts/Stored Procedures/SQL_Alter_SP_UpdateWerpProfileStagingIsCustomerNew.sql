
ALTER PROCEDURE [dbo].[SP_UpdateWerpProfileStagingIsCustomerNew]
AS
   update CustomerEquityXtrnlProfileStaging
     set CEXPS_IsCustomerNew=0,C_CustomerId = c.C_CustomerId, CP_PortfolioId = cp.CP_PortfolioId
     from CustomerEquityXtrnlProfileStaging s,Customer c , CustomerPortfolio cp
     where s.CEXPS_PANNum=c.C_PANNum and s.CEXPS_IsCustomerNew=1
     and (c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio = 1)

 