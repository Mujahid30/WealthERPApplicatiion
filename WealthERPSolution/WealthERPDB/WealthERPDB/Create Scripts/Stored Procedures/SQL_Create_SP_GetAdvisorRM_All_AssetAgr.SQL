/****** Object:  StoredProcedure [dbo].[SP_GetAdvisorRM_All_AssetAgr]    Script Date: 06/24/2009 11:58:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdvisorRM_All_AssetAgr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdvisorRM_All_AssetAgr]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdvisorRM_All_AssetAgr]    Script Date: 06/24/2009 11:58:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdvisorRM_All_AssetAgr]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetAdvisorRM_All_AssetAgr]
@A_AdviserId INT

AS

BEGIN
	
	WITH CurrentAggr AS
	(
		SELECT
			A_AdviserId,
			AR_RMId,
			AR_FirstName,
			AR_LastName,						
			dbo.Fn_GetRMCashAndSavingsAssetAgr(AR_RMId) AS CashAggr,
			dbo.Fn_GetRMCollectiblesAssetAgr(AR_RMId) AS ColAggr	,		
			dbo.Fn_GetRMEQAssetAgr(AR_RMId) AS EQAggr,
			dbo.Fn_GetRMFixedIncomeAssetAgr(AR_RMId) AS FIAggr,
			dbo.Fn_GetRMGoldAssetAgr(AR_RMId) AS GoldAggr,
			dbo.Fn_GetRMGovtSavingsAssetAgr(AR_RMId) AS GSAggr,
			dbo.Fn_GetRMInsuranceAssetAgr(AR_RMId) AS INSAgree,
			dbo.Fn_GetRMMFAssetAgr(AR_RMId) AS MFAgree,
			dbo.Fn_GetRMPensionAssetAgr(AR_RMId) AS PensionAggr,
			dbo.Fn_GetRMPersonalAssetAgr(AR_RMId) AS PerAggr,
			dbo.Fn_GetRMPropertyAssetAgr(AR_RMId) AS ProAggr
		
		FROM
		dbo.AdviserRM
	)
	
	SELECT 
	 
		AR_FirstName,
		AR_LastName,
		CAST((CashAggr + ColAggr + EQAggr + FIAggr + GoldAggr +GSAggr+INSAgree+MFAgree+PensionAggr+PerAggr+ProAggr) as NUMERIC(18,4)  )  AS result  
	FROM CurrentAggr WHERE A_AdviserId = @A_AdviserId;

END
' 
END
GO
