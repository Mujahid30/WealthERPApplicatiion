/****** Object:  StoredProcedure [dbo].[SP_GetPropertyNetPosition]    Script Date: 06/24/2009 12:04:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPropertyNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetPropertyNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPropertyNetPosition]    Script Date: 06/24/2009 12:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPropertyNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetPropertyNetPosition]
@CPNP_PropertyNPId INT

AS

BEGIN
	
	SELECT * FROM
		dbo.CustomerPropertyNetPosition
	WHERE
		CPNP_PropertyNPId = @CPNP_PropertyNPId
	
END' 
END
GO
