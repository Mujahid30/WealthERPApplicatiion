ALTER PROCEDURE [dbo].[SP_GetAdvisorBranchEQ_MF_IN_CurrentAggr]
@A_AdviserId INT

AS

BEGIN
	
	WITH CurrentAggr AS
	(
		SELECT
			A_AdviserId,
			AB_BranchName,
			AB_BranchCode,
			[dbo].[AdviserBranch].[AB_BranchId],
			dbo.Fn_GetBranchEQAssetAgr(AB_BranchId)AS EquityAggr,
			dbo.Fn_GetBranchMFAssetAgr(AB_BranchId) AS MFAggr,
			dbo.Fn_GetBranchInsuranceAssetAgr(AB_BranchId) AS InsuranceAggr
			
		FROM
		dbo.AdviserBranch	
		GROUP BY [dbo].[AdviserBranch].[AB_BranchId],
		[dbo].[AdviserBranch].[A_AdviserId],
		AB_BranchName,
			AB_BranchCode,
			[dbo].[AdviserBranch].[AB_BranchId],
 dbo.Fn_GetBranchEQAssetAgr(AB_BranchId),
		 	dbo.Fn_GetBranchMFAssetAgr(AB_BranchId),
		 dbo.Fn_GetBranchInsuranceAssetAgr(AB_BranchId)
		
	)
	
	SELECT * FROM CurrentAggr WHERE A_AdviserId = @A_AdviserId;
	
END
 