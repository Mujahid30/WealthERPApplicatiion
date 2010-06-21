ALTER PROCEDURE [dbo].[SP_UploadGetNewTransactions]
AS
BEGIN
SELECT A.CMFKXCS_CustomerId,A.CMFKXCS_AccountId,A.CMFKXCS_ProductCode,A.CMFKXCS_PANNumber,A.CMFKXCS_Fund,
A.CMFKXCS_FolioNumber,A.CMFKXCS_SchemeCode,A.CMFKXCS_DividendOption,A.CMFKXCS_FundDescription,
A.CMFKXCS_TransactionHead,A.CMFKXCS_TransactionNumber,A.CMFKXCS_Switch_RefNo,A.CMFKXCS_InstrumentNumber,
A.CMFKXCS_InvestorName,A.CMFKXCS_TransactionMode,A.CMFKXCS_TransactionStatus,A.CMFKXCS_BranchName,
A.CMFKXCS_BranchTransactionNo,A.CMFKXCS_TransactionDate,A.CMFKXCS_ProcessDate,A.CMFKXCS_Price,
A.CMFKXCS_LoadPercentage,A.CMFKXCS_Units,A.CMFKXCS_Amount,A.CMFKXCS_LoadAmount,A.CMFKXCS_AgentCode,
A.[CMFKXCS_Sub-BrokerCode],A.CMFKXCS_BrokeragePercentage,A.CMFKXCS_Commission,A.CMFKXCS_InvestorID,
A.CMFKXCS_ReportDate,A.CMFKXCS_ReportTime,A.CMFKXCS_TransactionSub,A.CMFKXCS_ApplicationNumber,
A.CMFKXCS_TransactionID,A.CMFKXCS_TransactionDescription,A.CMFKXCS_TransactionType,A.CMFKXCS_OrgPurchaseDate,
A.CMFKXCS_OrgPurchaseAmount,A.CMFKXCS_OrgPurchaseUnits,A.CMFKXCS_TrTypeFlag,A.CMFKXCS_SwitchFundDate,
A.CMFKXCS_InstrumentDate,A.CMFKXCS_InstrumentBank,A.CMFKXCS_NAV,A.CMFKXCS_OrginalPurchaseTrnxNo,
A.CMFKXCS_OrginalPurchaseBranch,A.CMFKXCS_IHNo,C.PASP_SchemePlanCode,'0' as IsSourceManual,
'KA' as SourceCode,B.WMTT_TransactionClassificationCode,1665 as CreatedBy,1665 as ModifiedBy,current_timestamp as CreatedOn,current_timestamp as ModifiedOn

FROM dbo.CustomerMFKarvyXtrnlCombinationStaging A, WerpKarvyDataTransalatorMapping B, dbo.ProductAMCSchemeMapping C
WHERE  (A.CMFKXCS_IsFolioNew=0 AND A.CMFKXCS_IsRejected=0)	
      AND (B.WKDTM_TransactionHead = A.CMFKXCS_TransactionHead   and
               B.WKDTM_TransactionDescription = A.CMFKXCS_TransactionDescription and
               B.WKDTM_TransactionType = A.CMFKXCS_TransactionType and
               B.WKDTM_TransactionTypeFlag = A.CMFKXCS_TrTypeFlag)
      AND
               C.PASC_AMC_ExternalCode=A.CMFKXCS_ProductCode AND C.PASC_AMC_ExternalType='KARVY'
END




 