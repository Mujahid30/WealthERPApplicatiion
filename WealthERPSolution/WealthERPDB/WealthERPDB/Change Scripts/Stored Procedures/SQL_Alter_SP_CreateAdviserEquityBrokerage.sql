-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_CreateAdviserEquityBrokerage


@A_AdviserId	INT,
@AEB_BuySell	char(1),
@AEB_Brokerage	numeric(10, 5),
--@AEB_ServiceTax	numeric(10, 5),
--@AEB_Clearing	numeric(10, 5),
@AEB_STT	numeric(10, 4),
@AEB_IsSpeculative	TINYINT,
--@AEB_Class	char(1),
--@AEB_CalculationBasis	char(1),
@AEB_CreatedBy	INT,
@AEB_ModifiedBy	INT

AS

INSERT INTO dbo.AdviserEquityBrokerage (
	A_AdviserId,
	AEB_BuySell,
	AEB_Brokerage,
	--AEB_ServiceTax,
	--AEB_Clearing,
	AEB_STT,
	AEB_IsSpeculative,
	--AEB_Class,
	--AEB_CalculationBasis,
	AEB_CreatedBy,
	AEB_CreatedOn,
	AEB_ModifiedOn,
	AEB_ModifiedBy
) VALUES 
( 
@A_AdviserId,
@AEB_BuySell,
@AEB_Brokerage,
--@AEB_ServiceTax,
--@AEB_Clearing,
@AEB_STT,
@AEB_IsSpeculative,
--@AEB_Class,
--@AEB_CalculationBasis,
@AEB_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@AEB_ModifiedBy
 ) 
	 