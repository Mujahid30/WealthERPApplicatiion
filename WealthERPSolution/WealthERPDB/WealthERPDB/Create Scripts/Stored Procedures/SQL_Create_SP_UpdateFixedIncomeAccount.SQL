/****** Object:  StoredProcedure [dbo].[SP_UpdateFixedIncomeAccount]    Script Date: 06/24/2009 12:06:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateFixedIncomeAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateFixedIncomeAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFixedIncomeAccount]    Script Date: 06/24/2009 12:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateFixedIncomeAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateFixedIncomeAccount]
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
CFIA_AccountId=@CFIA_AccountId' 
END
GO
