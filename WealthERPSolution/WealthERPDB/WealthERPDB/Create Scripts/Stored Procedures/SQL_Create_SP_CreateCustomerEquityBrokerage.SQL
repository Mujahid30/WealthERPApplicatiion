/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerEquityBrokerage]    Script Date: 06/24/2009 11:12:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerEquityBrokerage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerEquityBrokerage]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerEquityBrokerage]    Script Date: 06/24/2009 11:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerEquityBrokerage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerEquityBrokerage]


@C_CustomerId	INT,
@CEB_BuySell	char(1),
@CEB_Brokerage	numeric(10, 5),
--@CEB_ServiceTax	numeric(10, 5),
--@CEB_Clearing	numeric(10, 5),
@CEB_STT	numeric(10, 4),
@CEB_IsSpeculative	TINYINT,
--@CEB_Class	char(1),
--@CEB_CalculationBasis	char(1),
@CEB_CreatedBy	INT,
@CEB_ModifiedBy	INT

AS

INSERT INTO dbo.CustomerEquityBrokerage (
	C_CustomerId,
	CEB_BuySell,
	CEB_Brokerage,
--	CEB_ServiceTax,
--	CEB_Clearing,
	CEB_STT,
	CEB_IsSpeculative,
--	CEB_Class,
--	CEB_CalculationBasis,
	CEB_CreatedBy,
	CEB_CreatedOn,
	CEB_ModifiedOn,
	CEB_ModifiedBy
) VALUES 
( 
@C_CustomerId,
@CEB_BuySell,
@CEB_Brokerage,
--@CEB_ServiceTax,
--@CEB_Clearing,
@CEB_STT,
@CEB_IsSpeculative,
--@CEB_Class,
--@CEB_CalculationBasis,
@CEB_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CEB_ModifiedBy
 ) 
	' 
END
GO
