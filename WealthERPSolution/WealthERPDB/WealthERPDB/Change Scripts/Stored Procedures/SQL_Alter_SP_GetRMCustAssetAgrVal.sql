ALTER PROCEDURE [dbo].[SP_GetRMCustAssetAgrVal]        
@AR_RmId INT        
        
AS        
SET NOCOUNT ON      
      
SELECT TOP 5      
	C.C_CustomerId CustomerId,      
	C.C_FirstName + ' ' + C.C_LastName as Customer_Name,      
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
   