/****** Object:  StoredProcedure [dbo].[SP_GetEQDeliverySpecAggr]    Script Date: 06/24/2009 12:02:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEQDeliverySpecAggr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetEQDeliverySpecAggr]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEQDeliverySpecAggr]    Script Date: 06/24/2009 12:02:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEQDeliverySpecAggr]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_GetEQDeliverySpecAggr
@CP_PortfolioId INT

AS

BEGIN
	
	SELECT
		''Realised G/L - Deliv'' AS NetIncome,
		ISNULL(SUM([CENP_RealizedP/LForDelivery]), 0.0) AS AggrValue
	FROM
		ViewEquityNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	UNION
	
	SELECT
		''Realised G/L - Spec'' AS NetIncome,
		ISNULL(SUM([CENP_RealizedP/LForSpeculative]), 0.0) AS AggrValue
	FROM
		ViewEquityNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	UNION
	
	SELECT
		''Dividend Income'' AS NetIncome,
		ISNULL(SUM(CMFNP_DividendIncome), 0.0) AS AggrValue
	FROM
		ViewMutualFundNP
	WHERE
		CP_PortfolioId = @CP_PortfolioId
		
	--UNION
	
	--SELECT
	--	''Interest Income'' AS NetIncome,
	--	ISNULL(SUM(CENP_RealizedP/LForSpeculative), 0.0) AS AggrValue
	--FROM
	--	ViewEquityNP
	--WHERE
	--	CP_PortfolioId = @CP_PortfolioId
	
	
	
END' 
END
GO
