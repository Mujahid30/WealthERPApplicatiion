ALTER PROCEDURE [dbo].[SP_GetRejectedTransactions]
	-- Add the parameters for the stored procedure here
	@adviserId int
AS
	(select 'CAMS MF' as Type,CMCXTS_FolioNum as FolioNumber,CMCXTS_Scheme as Scheme,CMCXTS_PostDate as TransactionDate,CMCXTS_RejectedRemark as Remark from CustomerMFCAMSXtrnlTransactionStaging
	where CMCXTS_IsRejected=1 and A_AdviserId = @adviserId
	union
	select 'KARVY MF' as Type,CIMFKXTS_InvestorName as FolioNumber,CIMFKXTS_SchemeCode as Scheme,CIMFKXTS_TransactionDate as TransactionDate,CIMFKXTS_RejectionRemark as Remark from CustomerMFKarvyXtrnlTransactionStaging
	where CIMFKXTS_IsRejected=1 and A_AdviserId = @adviserId
	union
	select 'WERP MF' as Type,CMFXTS_FolioNum as FolioNumber,CMFXTS_SchemeName as Scheme,CMFXTS_TransactionDate as TransactionDate,CMFXTS_RejectedRemark as Remark from CustomerMFXtrnlTransactionStaging
	where CMFXTS_IsRejected=1 and A_AdviserId = @adviserId)
	order by Type
	 