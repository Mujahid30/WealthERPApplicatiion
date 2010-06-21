-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateFixedIncomeAccount]
@CFIA_AccountId	INT,
@CFIA_AccountNum	varchar(30),
@CFIA_AccountSource	varchar(30),
@XMOH_ModeOfHoldingCode	char(5),
@CFIA_ModifiedBy	int
	
	
AS

UPDATE dbo.CustomerFixedIncomeAccount SET


CFIA_AccountNum=@CFIA_AccountNum,
CFIA_AccountSource=@CFIA_AccountSource,
XMOH_ModeOfHoldingCode=@XMOH_ModeOfHoldingCode,
CFIA_ModifiedOn=CURRENT_TIMESTAMP,
CFIA_ModifiedBy=@CFIA_ModifiedBy

WHERE
CFIA_AccountId=@CFIA_AccountId 