ALTER PROCEDURE SP_UpdateCombinationStagingTable
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsCustomerNew=0,CMFKXCS_CustomerId = c.C_CustomerId ,CP_PortfolioId = cp.CP_PortfolioId
     from CustomerMFKarvyXtrnlCombinationStaging s,Customer c,CustomerPortfolio cp 
     where s.CMFKXCS_PANNumber=c.C_PANNum and s.CMFKXCS_IsCustomerNew=1 
     and (c.C_CustomerId = cp.C_CustomerId and cp.CP_IsMainPortfolio=1)
 