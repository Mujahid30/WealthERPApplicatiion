/****** Object:  StoredProcedure [dbo].[SP_GetScripName]    Script Date: 06/24/2009 12:05:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetScripName]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetScripName]    Script Date: 06/24/2009 12:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetScripName

@PEM_ScripCode INT

AS

SELECT 
	*
FROM 
	dbo.ProductEquityMaster
WHERE 
	PEM_ScripCode=@PEM_ScripCode
' 
END
GO
