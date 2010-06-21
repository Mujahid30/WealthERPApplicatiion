-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerEQAccount]

@CP_PortfolioId int,
@CETA_TradeAccountNum numeric(10),
@PAG_AssetGroupCode varchar(2),
@XB_BrokerCode varchar(5),
@CETA_AccountOpeningDate DATETIME,
@CETA_CreatedBy int,
@CETA_ModifiedBy INT,
@CETA_AccountId INT OUTPUT


AS
	
	
INSERT INTO CustomerEquityTradeAccount
(
CP_PortfolioId,
CETA_TradeAccountNum ,
PAG_AssetGroupCode,
XB_BrokerCode,
CETA_AccountOpeningDate,
CETA_CreatedBy,
CETA_CreatedOn,
CETA_ModifiedOn,
CETA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CETA_TradeAccountNum ,
@PAG_AssetGroupCode,
@XB_BrokerCode,
@CETA_AccountOpeningDate,
@CETA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CETA_ModifiedBy
)
SELECT @CETA_AccountId=SCOPE_IDENTITY() 