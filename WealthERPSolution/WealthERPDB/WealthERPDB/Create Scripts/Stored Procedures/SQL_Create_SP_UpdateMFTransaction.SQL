/****** Object:  StoredProcedure [dbo].[SP_UpdateMFTransaction]    Script Date: 06/24/2009 12:07:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateMFTransaction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateMFTransaction]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMFTransaction]    Script Date: 06/24/2009 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateMFTransaction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SP_UpdateMFTransaction]
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

' 
END
GO
