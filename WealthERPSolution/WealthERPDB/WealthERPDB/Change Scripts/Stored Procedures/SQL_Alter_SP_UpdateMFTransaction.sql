


ALTER PROCEDURE [dbo].[SP_UpdateMFTransaction]
	@CMFT_MFTransId int,
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
	@CMFT_SwitchSourceTrxId int,
	@WMTT_TransactionClassificationCode varchar(3),
	@CMFT_ModifiedBy int

AS

UPDATE dbo.CustomerMutualFundTransaction SET
	CMFA_AccountId=@CMFA_AccountId,
	PASP_SchemePlanCode=@PASP_SchemePlanCode ,
	CMFT_TransactionDate=@CMFT_TransactionDate,
	CMFT_BuySell=@CMFT_BuySell,
	CMFT_DividendRate=@CMFT_DividendRate,
	CMFT_NAV=@CMFT_NAV,
	CMFT_Price=@CMFT_Price,
	CMFT_Amount=@CMFT_Amount,
	CMFT_Units=@CMFT_Units,
	CMFT_STT=@CMFT_STT,
	CMFT_IsSourceManual=@CMFT_IsSourceManual,
	XES_SourceCode=@XES_SourceCode,
	CMFT_SwitchSourceTrxId =@CMFT_SwitchSourceTrxId,
	WMTT_TransactionClassificationCode=@WMTT_TransactionClassificationCode,
	CMFT_ModifiedBy=@CMFT_ModifiedBy,
	CMFT_ModifiedOn=CURRENT_TIMESTAMP
WHERE CMFT_MFTransId=@CMFT_MFTransId

 