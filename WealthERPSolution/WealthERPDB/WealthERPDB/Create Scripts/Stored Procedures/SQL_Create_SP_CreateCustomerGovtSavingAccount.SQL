/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerGovtSavingAccount]    Script Date: 06/24/2009 11:12:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerGovtSavingAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerGovtSavingAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerGovtSavingAccount]    Script Date: 06/24/2009 11:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerGovtSavingAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerGovtSavingAccount]

@CP_PortfolioId int,
@CGSA_AccountNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@CGSA_AccountSource varchar(30),
@CGSA_IsHeldJointly tinyint,
@XMOH_ModeOfHoldingCode VARCHAR(5),
@CGSA_CreatedBy int,
@CGSA_ModifiedBy INT,
@CGSA_AccountId INT OUTPUT 

AS
	
	
INSERT INTO CustomerGovtSavingAccount
(
CP_PortfolioId,
CGSA_AccountNum,
PAG_AssetGroupCode,
PAIC_AssetInstrumentCategoryCode,
CGSA_AccountSource,
CGSA_ISHeldJointly,
XMOH_ModeOfHoldingCode,
CGSA_CreatedBy,
CGSA_CreatedOn,
CGSA_ModifiedOn,
CGSA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CGSA_AccountNum,
@PAG_AssetGroupCode,
@PAIC_AssetInstrumentCategoryCode,
@CGSA_AccountSource,
@CGSA_ISHeldJointly,
@XMOH_ModeOfHoldingCode,
@CGSA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CGSA_ModifiedBy
)
SELECT @CGSA_AccountId=SCOPE_IDENTITY()' 
END
GO
