    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
ALTER PROCEDURE [dbo].[SP_UpdateGovtSavingsNP]    
    
@CGSNP_GovtSavingNPId int,    
@XDI_DebtIssuerCode varchar(5),    
@XIB_InterestBasisCode varchar(5),    
@XF_CompoundInterestFrequencyCode varchar(5),    
@XF_InterestPayableFrequencyCode varchar(5),    
@CGSNP_Name varchar(50),    
@CGSNP_PurchaseDate datetime,    
@CGSNP_CurrentValue numeric(18, 4),    
@CGSNP_MaturityDate datetime,    
@CGSNP_DepositAmount numeric(18, 4),    
@CGSNP_MaturityValue numeric(18, 4),    
@CGSNP_IsInterestAccumalated tinyint,    
@CGSNP_InterestAmtAccumalated numeric(18, 4),    
@CGSNP_InterestAmtPaidOut numeric(18, 4),    
@CGSNP_InterestRate numeric(7, 4),    
@CGSNP_Remark varchar(100),    
@CGSNP_ModifiedBy int     
AS   
  
SET NOCOUNT ON     
  
Declare    
  @bTran AS INT,        
  @lErrCode AS INT  
  
-- Set Variables  
SET @lErrCode  = 0  
  
-- Begin Tran  
If (@@Trancount = 0)    
 Begin    
  Set @bTran = 1    
  Begin Transaction    
 End     
  
update CustomerGovtSavingNetPosition set    
    
XDI_DebtIssuerCode=@XDI_DebtIssuerCode,    
XIB_InterestBasisCode=@XIB_InterestBasisCode,    
XF_CompoundInterestFrequencyCode=@XF_CompoundInterestFrequencyCode,    
XF_InterestPayableFrequencyCode=@XF_InterestPayableFrequencyCode,    
CGSNP_Name=@CGSNP_Name,    
CGSNP_PurchaseDate=@CGSNP_PurchaseDate,    
CGSNP_CurrentValue=@CGSNP_CurrentValue,    
CGSNP_MaturityDate=@CGSNP_MaturityDate,    
CGSNP_DepositAmount=@CGSNP_DepositAmount,    
CGSNP_MaturityValue=@CGSNP_MaturityValue,    
CGSNP_IsInterestAccumalated=@CGSNP_IsInterestAccumalated,    
CGSNP_InterestAmtAccumalated=@CGSNP_InterestAmtAccumalated,    
CGSNP_InterestAmtPaidOut=@CGSNP_InterestAmtPaidOut,    
CGSNP_InterestRate=@CGSNP_InterestRate,    
CGSNP_Remark=@CGSNP_Remark,    
CGSNP_ModifiedBy=@CGSNP_ModifiedBy,    
CGSNP_ModifiedOn=current_timestamp    
where    
CGSNP_GovtSavingNPId=@CGSNP_GovtSavingNPId    
  
If (@@Error <> 0)                      
 Begin                      
   Set @lErrCode = 1004 -- This is an error code set by the application       
   Goto Error                      
 End    
    
Success:        
 If (@bTran = 1 And @@Trancount > 0)        
 Begin                                      
  Commit Tran        
 End        
 Return 0        
        
 Goto Done        
        
Error:        
 If (@bTran = 1 And @@Trancount > 0)        
 Begin        
  Rollback Transaction        
 End        
 Return @lErrCode        
  
          
Done:              
SET NOCOUNT OFF         
  
   