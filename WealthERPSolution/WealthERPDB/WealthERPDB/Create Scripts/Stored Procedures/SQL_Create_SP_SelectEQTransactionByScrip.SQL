/****** Object:  StoredProcedure [dbo].[SP_SelectEQTransactionByScrip]    Script Date: 06/24/2009 12:05:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SelectEQTransactionByScrip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_SelectEQTransactionByScrip]
GO
/****** Object:  StoredProcedure [dbo].[SP_SelectEQTransactionByScrip]    Script Date: 06/24/2009 12:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SelectEQTransactionByScrip]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_SelectEQTransactionByScrip

@PEM_ScripCode INT

AS

SELECT
	 * 
FROM 
	dbo.CustomerEquityTransaction 
		
WHERE PEM_ScripCode=@PEM_ScripCode
' 
END
GO
