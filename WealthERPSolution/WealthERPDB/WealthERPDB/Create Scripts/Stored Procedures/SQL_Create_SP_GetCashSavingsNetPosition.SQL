/****** Object:  StoredProcedure [dbo].[SP_GetCashSavingsNetPosition]    Script Date: 06/24/2009 11:58:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCashSavingsNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCashSavingsNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCashSavingsNetPosition]    Script Date: 06/24/2009 11:58:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCashSavingsNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create PROCEDURE [dbo].[SP_GetCashSavingsNetPosition]
@CCSNP_CashSavingsNPId INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		CustomerCashSavingsNetPosition 
	WHERE 
		CCSNP_CashSavingsNPId = @CCSNP_CashSavingsNPId
END


' 
END
GO
