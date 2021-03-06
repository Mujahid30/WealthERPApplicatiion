/****** Object:  StoredProcedure [dbo].[SP_DeleteMFTransactions]    Script Date: 06/24/2009 11:14:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMFTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteMFTransactions]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteMFTransactions]    Script Date: 06/24/2009 11:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteMFTransactions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DeleteMFTransactions]
@CMFT_MFTransId INT

AS

BEGIN
	
	DELETE FROM CustomerMutualFundTransaction
	WHERE 
		CMFT_MFTransId = @CMFT_MFTransId
		AND
		CMFT_IsSourceManual = 1
	
END' 
END
GO
