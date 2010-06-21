ALTER PROCEDURE [dbo].[SP_UpdateCombinationStagingIsCustomerNew]
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsCustomerNew=0,CMFKXCS_CustomerId = c.C_CustomerId 
     from CustomerMFKarvyXtrnlCombinationStaging s,Customer c 
     where s.CMFKXCS_PANNumber=c.C_PANNum and s.CMFKXCS_IsCustomerNew=1
 