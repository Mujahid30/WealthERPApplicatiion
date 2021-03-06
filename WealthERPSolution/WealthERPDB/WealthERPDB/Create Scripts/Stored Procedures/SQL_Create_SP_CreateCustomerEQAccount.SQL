/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerEQAccount]    Script Date: 06/24/2009 11:12:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerEQAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerEQAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerEQAccount]    Script Date: 06/24/2009 11:12:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerEQAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerEQAccount]

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
SELECT @CETA_AccountId=SCOPE_IDENTITY()' 
END
GO
