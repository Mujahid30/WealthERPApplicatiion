/****** Object:  StoredProcedure [dbo].[SP_UpdateCashSavingsAccount]    Script Date: 06/24/2009 12:06:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCashSavingsAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCashSavingsAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCashSavingsAccount]    Script Date: 06/24/2009 12:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCashSavingsAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateCashSavingsAccount]

@CCSA_AccountId	INT,
@CCSA_AccountNum	varchar(30),
@CCSA_BankName	varchar(30),
@CCSA_AccountOpeningDate DATETIME,
@XMOH_ModeOfHoldingCode	char(5),
@CCSA_ModifiedBy	int
	
	
AS

UPDATE dbo.CustomerCashSavingsAccount SET


CCSA_AccountNum=@CCSA_AccountNum,
CCSA_BankName=@CCSA_BankName,
CCSA_AccountOpeningDate=@CCSA_AccountOpeningDate,
XMOH_ModeOfHoldingCode=@XMOH_ModeOfHoldingCode,
CCSA_ModifiedOn=CURRENT_TIMESTAMP,
CCSA_ModifiedBy=@CCSA_ModifiedBy

WHERE

CCSA_AccountId=@CCSA_AccountId' 
END
GO
