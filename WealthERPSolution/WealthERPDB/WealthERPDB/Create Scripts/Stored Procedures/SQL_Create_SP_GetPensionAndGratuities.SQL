/****** Object:  StoredProcedure [dbo].[SP_GetPensionAndGratuities]    Script Date: 06/24/2009 12:03:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPensionAndGratuities]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetPensionAndGratuities]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPensionAndGratuities]    Script Date: 06/24/2009 12:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPensionAndGratuities]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetPensionAndGratuities]
@CPGNP_PensionGratutiesNPId INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		CustomerPensionandGratuitiesNetPosition 
	WHERE 
		CPGNP_PensionGratutiesNPId = @CPGNP_PensionGratutiesNPId
END


' 
END
GO
