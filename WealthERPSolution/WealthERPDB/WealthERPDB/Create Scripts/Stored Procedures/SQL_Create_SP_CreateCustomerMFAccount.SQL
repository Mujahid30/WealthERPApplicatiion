/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerMFAccount]    Script Date: 06/24/2009 11:13:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerMFAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerMFAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerMFAccount]    Script Date: 06/24/2009 11:13:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerMFAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerMFAccount]

@CP_PortfolioId int,
@CMFA_FolioNum varchar(30),
@PAG_AssetGroupCode varchar(2),
@PA_AMCCode INT ,
@CMFA_IsJointlyHeld TINYINT,
@CMFA_AccountOpeningDate DATETIME,
@XMOH_ModeOfHoldingCode VARCHAR(5),
@CMFA_CreatedBy int,
@CMFA_ModifiedBy INT,
@CMFA_AccountId INT OUTPUT


AS

INSERT INTO CustomerMutualFundAccount
(
CP_PortfolioId,
CMFA_FolioNum,
PAG_AssetGroupCode,
PA_AMCCode,
CMFA_IsJointlyHeld,
XMOH_ModeOfHoldingCode,
CMFA_CreatedBy,
CMFA_CreatedOn,
CMFA_ModifiedOn,
CMFA_ModifiedBy
)
VALUES
(
@CP_PortfolioId,
@CMFA_FolioNum,
@PAG_AssetGroupCode,
@PA_AMCCode,
@CMFA_IsJointlyHeld,
@XMOH_ModeOfHoldingCode,
@CMFA_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CMFA_ModifiedBy
)
SELECT @CMFA_AccountId=SCOPE_IDENTITY()' 
END
GO
