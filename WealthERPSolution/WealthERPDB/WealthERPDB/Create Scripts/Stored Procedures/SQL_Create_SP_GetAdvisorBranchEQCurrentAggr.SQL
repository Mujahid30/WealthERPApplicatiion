/****** Object:  StoredProcedure [dbo].[SP_GetAdvisorBranchEQCurrentAggr]    Script Date: 06/24/2009 11:57:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdvisorBranchEQCurrentAggr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdvisorBranchEQCurrentAggr]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdvisorBranchEQCurrentAggr]    Script Date: 06/24/2009 11:57:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdvisorBranchEQCurrentAggr]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetAdvisorBranchEQCurrentAggr]  
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
   
END ' 
END
GO
