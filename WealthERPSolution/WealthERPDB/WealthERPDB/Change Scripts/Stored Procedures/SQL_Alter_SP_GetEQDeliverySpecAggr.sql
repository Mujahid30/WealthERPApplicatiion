ALTER PROCEDURE SP_GetEQDeliverySpecAggr
@CP_PortfolioId INT

AS

BEGIN
	
	SELECT
		'Realised G/L - Deliv' AS NetIncome,
		ISNULL(SUM([CENP_RealizedP/LForDelivery]), 0.0) AS AggrValue
	FROM
		ViewEquityNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	UNION
	
	SELECT
		'Realised G/L - Spec' AS NetIncome,
		ISNULL(SUM([CENP_RealizedP/LForSpeculative]), 0.0) AS AggrValue
	FROM
		ViewEquityNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	UNION
	
	SELECT
		'Dividend Income' AS NetIncome,
		ISNULL(SUM(CMFNP_DividendIncome), 0.0) AS AggrValue
	FROM
		ViewMutualFundNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	--UNION
	
	--SELECT
	--	'Interest Income' AS NetIncome,
	--	ISNULL(SUM(CENP_RealizedP/LForSpeculative), 0.0) AS AggrValue
	--FROM
	--	ViewEquityNP
	--WHERE
	--	CP_PortfolioId = @CP_PortfolioId
	
	
	
END 