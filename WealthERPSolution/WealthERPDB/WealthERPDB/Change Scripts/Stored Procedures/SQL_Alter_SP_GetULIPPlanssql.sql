
ALTER PROCEDURE [dbo].[SP_GetULIPPlans]
@XII_InsuranceIssuerCode VARCHAR(5)
AS
SELECT 
* 
FROM 
dbo.WerpULIPPlan 
WHERE 
XII_InsuranceIssuerCode=@XII_InsuranceIssuerCode
 