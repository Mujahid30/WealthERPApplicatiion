-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateCashSavingsAccount]

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

CCSA_AccountId=@CCSA_AccountId 