/****** Object:  StoredProcedure [dbo].[SP_DeleteEQTransactions]    Script Date: 06/24/2009 11:14:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteEQTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteEQTransactions]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteEQTransactions]    Script Date: 06/24/2009 11:14:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteEQTransactions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DeleteEQTransactions]
@CET_EqTransId INT

AS

BEGIN
	
	DELETE FROM CustomerEquityTransaction
	WHERE 
		CET_EqTransId = @CET_EqTransId
		AND
		CET_IsSourceManual = 0
	
END' 
END
GO
