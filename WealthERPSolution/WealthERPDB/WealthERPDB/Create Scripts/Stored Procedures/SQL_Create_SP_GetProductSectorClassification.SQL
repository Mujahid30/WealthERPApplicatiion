/****** Object:  StoredProcedure [dbo].[SP_GetProductSectorClassification]    Script Date: 06/24/2009 12:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductSectorClassification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductSectorClassification]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductSectorClassification]    Script Date: 06/24/2009 12:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductSectorClassification]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE SP_GetProductSectorClassification 
AS

BEGIN

 SET NOCOUNT ON;
	
 Select PSC_SectorId,PSC_SectorName
 FROM ProductSectorClassification 
 
END
' 
END
GO
