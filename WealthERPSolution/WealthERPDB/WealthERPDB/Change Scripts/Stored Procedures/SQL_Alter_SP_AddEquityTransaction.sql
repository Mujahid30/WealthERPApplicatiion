ALTER PROCEDURE [dbo].[SP_AddEquityTransaction]  
  
@CETA_AccountId int,  
@PEM_ScripCode int,  
@CET_BuySell char(1),  
@CET_TradeNum numeric(15,0),  
@CET_OrderNum numeric(15,0),  
@CET_IsSpeculative tinyint,  
@XE_ExchangeCode varchar(5),  
@CET_TradeDate datetime,  
@CET_Rate numeric(18,4),  
@CET_Quantity numeric(18,4),  
@CET_Brokerage numeric(18,4),  
@CET_ServiceTax numeric(18,4),  
@CET_EducationCess numeric(18,4),  
@CET_STT numeric(18,4),  
@CET_OtherCharges numeric(18,4),  
@CET_RateInclBrokerage numeric(18,4),  
@CET_TradeTotal numeric(18,4),  
@XB_BrokerCode varchar(5),  
@CET_IsSplit tinyint,  
@CET_SplitCustEqTransId int,  
@XES_SourceCode varchar(5),  
@WETT_TransactionCode tinyint,  
@CET_IsSourceManual tinyint,  
@CET_ModifiedBy int,  
@CET_CreatedBy int,  
@CET_EqTransId INT OUTPUT  
     
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
  
INSERT INTO dbo.CustomerEquityTransaction (CETA_AccountId  
           ,PEM_ScripCode  
           ,CET_BuySell  
           ,CET_TradeNum  
           ,CET_OrderNum  
           ,CET_IsSpeculative  
           ,XE_ExchangeCode  
           ,CET_TradeDate  
           ,CET_Rate  
           ,CET_Quantity  
           ,CET_Brokerage  
           ,CET_ServiceTax  
           ,CET_EducationCess  
           ,CET_STT  
           ,CET_OtherCharges  
           ,CET_RateInclBrokerage  
           ,CET_TradeTotal  
           ,XB_BrokerCode  
           ,CET_IsSplit  
           ,CET_SplitCustEqTransId  
           ,XES_SourceCode  
           ,WETT_TransactionCode  
           ,CET_IsSourceManual  
           ,CET_ModifiedBy  
           ,CET_ModifiedOn  
           ,CET_CreatedBy  
           ,CET_CreatedOn)  
   
 VALUES  
 (  
@CETA_AccountId,  
@PEM_ScripCode,  
@CET_BuySell,  
@CET_TradeNum,  
@CET_OrderNum,  
@CET_IsSpeculative,  
@XE_ExchangeCode,  
@CET_TradeDate,  
@CET_Rate,  
@CET_Quantity,  
@CET_Brokerage,  
@CET_ServiceTax,  
@CET_EducationCess,  
@CET_STT,  
@CET_OtherCharges,  
@CET_RateInclBrokerage,  
@CET_TradeTotal,  
@XB_BrokerCode,  
@CET_IsSplit,  
@CET_SplitCustEqTransId ,  
@XES_SourceCode,  
@WETT_TransactionCode,  
@CET_IsSourceManual,  
@CET_ModifiedBy,  
CURRENT_TIMESTAMP,  
@CET_CreatedBy,   
CURRENT_TIMESTAMP  
)  

If (@@Error <> 0)                    
	Begin                    
	  Set @lErrCode = 1006 -- This is an error code set by the application     
	  Goto Error                    
	End  

SELECT @CET_EqTransId=SCOPE_IDENTITY()  

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
 