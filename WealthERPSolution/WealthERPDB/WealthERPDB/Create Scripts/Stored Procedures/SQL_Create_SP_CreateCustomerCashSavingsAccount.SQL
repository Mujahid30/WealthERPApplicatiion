/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerCashSavingsAccount]    Script Date: 06/24/2009 11:12:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerCashSavingsAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerCashSavingsAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerCashSavingsAccount]    Script Date: 06/24/2009 11:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerCashSavingsAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerCashSavingsAccount]

@CP_PortfolioId int,
@CCSA_AccountNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@CCSA_BankName varchar(30),
@CCSA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode varchar(5),
@CCSA_CreatedBy int,
@CCSA_ModifiedBy INT,
@CCSA_AccountOpeningDate DATETIME,
@CCSA_AccountId INT OUTPUT


AS

INSERT INTO CustomerCashSavingsAccount
(
CP_PortfolioId,
CCSA_AccountNum,
PAG_AssetGroupCode,
PAIC_AssetInstrumentCategoryCode,
CCSA_BankName,
CCSA_ISHeldJointly,
XMOH_ModeOfHoldingCode,
CCSA_CreatedBy,
CCSA_CreatedOn,
CCSA_ModifiedOn,
CCSA_ModifiedBy,
CCSA_AccountOpeningDate
)
VALUES
(
@CP_PortfolioId,
@CCSA_AccountNum,
@PAG_AssetGroupCode,
@PAIC_AssetInstrumentCategoryCode,
@CCSA_BankName,
@CCSA_ISHeldJointly,
@XMOH_ModeOfHoldingCode,
@CCSA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CCSA_ModifiedBy,
@CCSA_AccountOpeningDate
)
SELECT @CCSA_AccountId=SCOPE_IDENTITY()' 
END
GO
