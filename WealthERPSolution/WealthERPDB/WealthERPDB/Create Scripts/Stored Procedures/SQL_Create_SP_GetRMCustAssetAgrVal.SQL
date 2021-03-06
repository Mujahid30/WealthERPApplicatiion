/****** Object:  StoredProcedure [dbo].[SP_GetRMCustAssetAgrVal]    Script Date: 06/24/2009 12:04:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustAssetAgrVal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMCustAssetAgrVal]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMCustAssetAgrVal]    Script Date: 06/24/2009 12:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustAssetAgrVal]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetRMCustAssetAgrVal]        
@AR_RmId INT        
        
AS        
SET NOCOUNT ON      
      
SELECT TOP 5      
	C.C_CustomerId CustomerId,      
	C.C_FirstName + '' '' + C.C_LastName as Customer_Name,      
	C.AR_RMID,        
	dbo.Fn_GetCustEquityCurValAggr(C.C_CustomerId, ARM.A_AdviserId) AS EQCurrentVal,        
	dbo.Fn_GetCustMFCurValAggr(C.C_CustomerId, ARM.A_AdviserId) AS MFCurrentVal
FROM 
	dbo.Customer C
    INNER JOIN
    dbo.AdviserRM ARM
    ON C.AR_RMId = ARM.AR_RMId
WHERE    
	C.AR_RMId = @AR_RmId  
ORDER BY EQCurrentVal DESC, MFCurrentVal DESC    
    
SET NOCOUNT OFF   
  ' 
END
GO
GRANT EXECUTE ON [dbo].[SP_GetRMCustAssetAgrVal] TO [public] AS [dbo]
GO
