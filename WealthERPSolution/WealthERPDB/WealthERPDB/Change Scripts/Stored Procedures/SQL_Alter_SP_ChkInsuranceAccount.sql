 
ALTER PROCEDURE [dbo].[SP_ChkInsuranceAccount]
@C_CustomerId INT
AS
SELECT 
	COUNT(*) 
FROM 
	dbo.CustomerInsuranceAccount as CIA
	INNER JOIN
	CustomerPortfolio AS CP
	ON CIA.CP_PortfolioId = CP.CP_PortfolioId
WHERE
	CP.C_CustomerId=@C_CustomerId
