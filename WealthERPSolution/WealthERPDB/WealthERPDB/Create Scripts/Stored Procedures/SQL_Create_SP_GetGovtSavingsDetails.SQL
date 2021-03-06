/****** Object:  StoredProcedure [dbo].[SP_GetGovtSavingsDetails]    Script Date: 06/24/2009 12:02:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetGovtSavingsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetGovtSavingsDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetGovtSavingsDetails]    Script Date: 06/24/2009 12:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetGovtSavingsDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetGovtSavingsDetails]

@CGSNP_GovtSavingNPId INT

AS

SELECT CGSNP.*,PAIC.PAIC_AssetInstrumentCategoryName FROM  dbo.CustomerGovtSavingNetPosition AS CGSNP
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
ON CGSNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CGSNP_GovtSavingNPId=@CGSNP_GovtSavingNPId
' 
END
GO
