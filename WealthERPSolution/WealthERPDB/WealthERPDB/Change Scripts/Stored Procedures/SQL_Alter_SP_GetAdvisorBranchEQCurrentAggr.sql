ALTER PROCEDURE [dbo].[SP_GetAdvisorBranchEQCurrentAggr]  
@A_AdviserId INT  
  
AS  
  
BEGIN  
   
 
  SELECT  
   A_AdviserId,  
   AB_BranchName,  
   AB_BranchCode,  
   dbo.Fn_GetBranchEQAssetAgr(AB_BranchId)AS EquityAggr,  
   dbo.Fn_GetBranchMFAssetAgr(AB_BranchId) AS MFAggr,  
   dbo.Fn_GetBranchInsuranceAssetAgr(AB_BranchId) AS InsuranceAggr  
     
  FROM  
  dbo.AdviserBranch   
  
 WHERE A_AdviserId = @A_AdviserId;  
   
END  