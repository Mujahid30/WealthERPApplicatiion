
  
ALTER PROCEDURE [dbo].[SP_AddMutualFundTransaction]  
   
 @CMFA_AccountId int,  
 @PASP_SchemePlanCode int,  
 @CMFT_TransactionDate datetime,  
 @CMFT_BuySell char(1),  
 @CMFT_DividendRate numeric(10, 5),  
 @CMFT_NAV numeric(18, 5),  
 @CMFT_Price numeric(18, 5),  
 @CMFT_Amount numeric(18, 5),  
 @CMFT_Units numeric(18, 5),  
 @CMFT_STT numeric(10, 5),  
 @CMFT_IsSourceManual tinyint,  
 @XES_SourceCode varchar(5),  
 @CMFT_SwitchSourceTrxId int ,  
 @WMTT_TransactionClassificationCode varchar(3),  
 @CMFT_ModifiedBy int,  
 @CMFT_CreatedBy int,  
 @CMFT_MFTransId INT OUTPUT  
   
as  
  
INSERT INTO [dbo].[CustomerMutualFundTransaction]  
           (CMFA_AccountId  
           ,PASP_SchemePlanCode  
           ,CMFT_TransactionDate  
           ,CMFT_BuySell  
           ,CMFT_DividendRate  
           ,CMFT_NAV  
           ,CMFT_Price  
           ,CMFT_Amount  
           ,CMFT_Units  
           ,CMFT_STT  
           ,CMFT_IsSourceManual  
           ,XES_SourceCode  
           ,CMFT_SwitchSourceTrxId  
           ,WMTT_TransactionClassificationCode  
           ,CMFT_ModifiedBy  
           ,CMFT_CreatedBy  
           ,CMFT_CreatedOn  
           ,CMFT_ModifiedOn)  
     VALUES  
(@CMFA_AccountId,  
 @PASP_SchemePlanCode,  
 @CMFT_TransactionDate,  
 @CMFT_BuySell,  
 @CMFT_DividendRate,  
 @CMFT_NAV,  
 @CMFT_Price,  
 @CMFT_Amount,  
 @CMFT_Units,  
 @CMFT_STT,  
 @CMFT_IsSourceManual,  
 @XES_SourceCode,  
 @CMFT_SwitchSourceTrxId,  
 @WMTT_TransactionClassificationCode,   
 @CMFT_ModifiedBy,   
 @CMFT_CreatedBy,  
 CURRENT_TIMESTAMP,  
 CURRENT_TIMESTAMP)  
  
SELECT @CMFT_MFTransId=SCOPE_IDENTITY()  
  