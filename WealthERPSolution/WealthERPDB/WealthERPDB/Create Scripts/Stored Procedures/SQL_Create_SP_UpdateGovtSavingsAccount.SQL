/****** Object:  StoredProcedure [dbo].[SP_UpdateGovtSavingsAccount]    Script Date: 06/24/2009 12:06:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateGovtSavingsAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateGovtSavingsAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateGovtSavingsAccount]    Script Date: 06/24/2009 12:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateGovtSavingsAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateGovtSavingsAccount]

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
' 
END
GO
