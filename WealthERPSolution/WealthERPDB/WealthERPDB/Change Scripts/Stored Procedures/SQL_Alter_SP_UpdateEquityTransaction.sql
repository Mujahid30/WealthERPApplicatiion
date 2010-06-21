


ALTER PROCEDURE [dbo].[SP_UpdateEquityTransaction]
@CET_EqTransId	INT,
@CETA_AccountId	INT,
@PEM_ScripCode	INT,
@CET_BuySell	char(1),
@CET_TradeNum	numeric(15, 0),
@CET_OrderNum	numeric(15, 0),
@CET_IsSpeculative	TINYINT,
@XE_ExchangeCode	varchar(5),
@CET_TradeDate	DATETIME,
@CET_Rate	numeric(18, 4),
@CET_Quantity	numeric(18, 4),
@CET_Brokerage	numeric(18, 4),
@CET_ServiceTax	numeric(18, 4),
@CET_EducationCess	numeric(18, 4),
@CET_STT	numeric(18, 4),
@CET_OtherCharges	numeric(18, 4),
@CET_RateInclBrokerage	numeric(18, 4),
@CET_TradeTotal	numeric(18, 4),
@XB_BrokerCode	varchar(5),
@CET_IsSplit	TINYINT,
@CET_SplitCustEqTransId	INT,
@XES_SourceCode	varchar(5),
@WETT_TransactionCode	TINYINT,
@CET_IsSourceManual	TINYINT,
@CET_ModifiedBy	INT

	

AS

UPDATE dbo.CustomerEquityTransaction
SET

CETA_AccountId=@CETA_AccountId,
PEM_ScripCode=@PEM_ScripCode,
CET_BuySell=@CET_BuySell,
CET_TradeNum=@CET_TradeNum,
CET_OrderNum=@CET_OrderNum,
CET_IsSpeculative=@CET_IsSpeculative,
XE_ExchangeCode=@XE_ExchangeCode,
CET_TradeDate=@CET_TradeDate,
CET_Rate=@CET_Rate,
CET_Quantity=@CET_Quantity,
CET_Brokerage=@CET_Brokerage,
CET_ServiceTax=@CET_ServiceTax,
CET_EducationCess=@CET_EducationCess,
CET_STT=@CET_STT,
CET_OtherCharges=@CET_OtherCharges,
CET_RateInclBrokerage=@CET_RateInclBrokerage,
CET_TradeTotal=@CET_TradeTotal,
XB_BrokerCode=@XB_BrokerCode,
CET_IsSplit=@CET_IsSplit,
CET_SplitCustEqTransId=@CET_SplitCustEqTransId,
XES_SourceCode=@XES_SourceCode,
WETT_TransactionCode=@WETT_TransactionCode,
CET_IsSourceManual=@CET_IsSourceManual,
CET_ModifiedBy=@CET_ModifiedBy,
CET_ModifiedOn=CURRENT_TIMESTAMP


WHERE CET_EqTransId=@CET_EqTransId

 