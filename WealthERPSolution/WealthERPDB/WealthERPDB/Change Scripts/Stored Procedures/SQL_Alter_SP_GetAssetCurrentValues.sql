 ALTER PROCEDURE [dbo].[SP_GetAssetCurrentValues]
@CP_PortfolioId INT

AS

BEGIN
	
	/* 1 - Fixed Income Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CFINP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewFixedIncomeNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 2 - Govt Savings Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CGSNP_CurrentValue),0.0) AS AggrCurrentValue
	FROM
		ViewGovtSavingsNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 3 - Gold Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CGNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewGoldNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 4 - Insurance Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CINP_SurrenderValue), 0.0) AS AggrCurrentValue
	FROM
		ViewInsuranceNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 5 - Pension Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CPGNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewPensionGratuities
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 6 - Property Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CPNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewPropertyNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 7 - Cash n Savings Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CCSNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewCashSavingsNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 8 - Collectible Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CCNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewCollectiblesNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 9 - Personal Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CPNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewPersonalNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 10 - MF Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CMFNP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewMutualFundNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		and
		CMFNP_ValuationDate in (
							select top 1(CMFNP_ValuationDate)
							FROM  
							dbo.ViewMutualFundNP  
							WHERE  
								CP_PortfolioId = @CP_PortfolioId  
							order by CMFNP_ValuationDate desc
							)
	Group By
		PAG_AssetGroupName
	
	UNION
	/* 11 - Equity Aggregate Current Value*/
	SELECT
		PAG_AssetGroupName AS AssetType, 
		ISNULL(SUM(CENP_CurrentValue), 0.0) AS AggrCurrentValue
	FROM
		ViewEquityNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		and
		CENP_ValuationDate in (
							select top 1(CENP_ValuationDate)
							FROM  
							dbo.ViewEquityNP  
							WHERE  
								CP_PortfolioId = @CP_PortfolioId  
							order by CENP_ValuationDate desc
							)
	Group By
		PAG_AssetGroupName
		
END