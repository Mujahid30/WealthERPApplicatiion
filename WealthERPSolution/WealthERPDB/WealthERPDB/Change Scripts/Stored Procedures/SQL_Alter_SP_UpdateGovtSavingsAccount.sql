
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateGovtSavingsAccount]

@CGSA_AccountId	int,
@CGSA_AccountNum	varchar(30),
@CGSA_AccountSource	varchar(30),
@XMOH_ModeOfHoldingCode	char(5),
@CGSA_AccountOpeningDate	datetime,
@CGSA_ModifiedBy	int

as

update CustomerGovtSavingAccount set 
	CGSA_AccountNum=@CGSA_AccountNum,
	CGSA_AccountSource=@CGSA_AccountSource,
	XMOH_ModeOfHoldingCode=@XMOH_ModeOfHoldingCode,
	CGSA_AccountOpeningDate=@CGSA_AccountOpeningDate,
	CGSA_ModifiedBy=@CGSA_ModifiedBy
	
	where CGSA_AccountId=@CGSA_AccountId
 